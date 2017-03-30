using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedWeapon : MonoBehaviour
{
    public Transform FIRE_POINT;
	public SpriteRenderer muzzleFlashRenderer;

    public GameObject BULLET;

    public float AIM_THRESHOLD = 0.2f;

    float timer = 0.0f;

    private float recoil = 0.0f;
    public float recoilFactor = 10.0f;

    public int ammo = 20;
    public int clipSize = 8;
    private int ammoInClip;
    private bool isReloading = false;

    public float reloadTime = 0.5f;

	// Shell casings
	public bool shouldDropShells = true;
	public GameObject SHELL;

    // Bars
    public FillBar reloadBar;

    // SOUND EFFECTS
    private AudioSource audioSource;
    public AudioClip SFX_SHOOT;
    public AudioClip SFX_RELOAD;

    // Use this for initialization
    void Start()
    {
        ammoInClip = clipSize;

        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        /* // Compensate the input for player turning, i.e. flipping in the x-direction.
         float aimAngle = 0.0f;

         if (Mathf.Abs(x) < AIM_THRESHOLD)
             x = 0.0f;
         if (Mathf.Abs(y) < AIM_THRESHOLD)
             y = 0.0f;

         if (x != 0.0f || y != 0.0f)
         {
             aimAngle = Mathf.Atan2(y, x) * Mathf.Rad2Deg;
             this.transform.rotation = Quaternion.Euler(0.0f, 0.0f, aimAngle);
         } */

        if (Input.GetAxis("RIGHT_TRIGGER") > 0.0f && timer <= 0.0f && ammoInClip > 0)
        {
            Shoot();
            Debug.Log("Ammo in magazine: " + ammoInClip);
        }
        else if (Input.GetButton("RELOAD") && !isReloading)
        {
            StartCoroutine(Reload(reloadTime));
		}

        timer -= Time.deltaTime;
        recoil -= Time.deltaTime;
        recoil = Mathf.Max(0.0f, recoil);
    }

    private void Shoot()
    {
		Vector3 playerLocalScale = transform.parent.transform.parent.localScale;
        Vector3 localRot = transform.parent.localEulerAngles;

        // If flipped to the left - flip x-wise
        if (playerLocalScale.x < 0.0f)
        {
            localRot.z *= playerLocalScale.x;
            localRot.z += 180.0f;
        }

        recoil += recoilFactor;

        float recoilDisplacement = Random.Range(-recoil, recoil);
        localRot.z += recoilDisplacement;

        Quaternion spawnRot = Quaternion.Euler(localRot);

        GameObject obj = Instantiate(BULLET, FIRE_POINT.position, spawnRot);
        obj.GetComponent<Attack>().Initiate(0);

		StartCoroutine (ShowMuzzleFlash (0.05f));

		if(shouldDropShells)
			Instantiate (SHELL, transform.position, transform.rotation);

        audioSource.PlayOneShot(SFX_SHOOT);

        timer = 0.2f;
        ammoInClip--;
    }

    private IEnumerator Reload(float dt)
    {
        isReloading = true;
        audioSource.PlayOneShot(SFX_RELOAD);
        yield return new WaitForSeconds(dt);

        ammo += ammoInClip;

        if (ammo >= clipSize)
        {
            ammo -= clipSize;
            ammoInClip = clipSize;
        }
        else
        {
            ammoInClip = ammo;
            ammo = 0;
        }

        Debug.Log("Reload, ammo in clip: " + ammoInClip);
        Debug.Log("Reload, total ammo: " + ammo);

        isReloading = false;
    }

	private IEnumerator ShowMuzzleFlash(float dt)
	{
		muzzleFlashRenderer.gameObject.SetActive(true);

		if (Random.value > 0.5) {
			muzzleFlashRenderer.flipY = true;
		}
		else
		{
			muzzleFlashRenderer.flipY = false;
		}

		yield return new WaitForSeconds(dt);
		muzzleFlashRenderer.gameObject.SetActive(false);
	}
}
