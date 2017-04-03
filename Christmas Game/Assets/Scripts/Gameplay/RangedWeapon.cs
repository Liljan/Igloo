using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedWeapon : MonoBehaviour
{
	public WeaponID weaponID;

    public Transform FIRE_POINT;
	public SpriteRenderer muzzleFlashRenderer;

    public GameObject BULLET;
	public int damage;

    public float AIM_THRESHOLD = 0.2f;

	public float fireRate;
	private float fireTime;
    float timer = 0.0f;
	public bool isSingle;

    private float recoil = 0.0f;
    public float recoilFactor = 10.0f;

	[Header("Ammo")]
    public int ammo = 20;
    public int clipSize = 8;
    private int ammoInClip;
    private bool isReloading = false;

    public float reloadTime = 0.5f;

	// Shell casings
	[Header("Shell casings")]
	public bool shouldDropShells = true;
	public GameObject SHELL;

    // Bars
    public FillBar reloadBar;

    // SOUND EFFECTS
	[Header("Sound Effects")]
    private AudioSource audioSource;
    public AudioClip SFX_SHOOT;
    public AudioClip SFX_RELOAD;

    // Use this for initialization
    void Start()
    {
        ammoInClip = clipSize;

        audioSource = GetComponent<AudioSource>();

		fireTime = 1.0f / fireRate;
    }

    // Update is called once per frame
    void Update()
    {
		// Ugly hax

		if (Input.GetAxis("RIGHT_TRIGGER") > 0.0f && timer <= 0.0f && ammoInClip > 0)
		{
			Shoot();
			Debug.Log("Ammo in magazine: " + ammoInClip);
		}
		else if (Input.GetButton("RELOAD") && !isReloading)
		{
				StartCoroutine(Reload(reloadTime));
		}

		if (isSingle) {
			if (Input.GetAxis ("RIGHT_TRIGGER") == 0.0f) {
				timer = 0.0f;
			}
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
		obj.GetComponent<Attack>().Initiate(0,damage);

		StartCoroutine(ShowMuzzleFlash(0.05f));

		if(shouldDropShells)
			Instantiate(SHELL, transform.position, transform.rotation);

        audioSource.PlayOneShot(SFX_SHOOT);

		if (isSingle) {
			timer = Mathf.Infinity;
		} else {
			timer = fireTime;
		}
        
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

	public void AddAmmo(int a) {
		ammo += a;
	}

	public void OnDisable() {
		isReloading = false;
		muzzleFlashRenderer.gameObject.SetActive(false);
	}
}
