using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedWeapon : MonoBehaviour
{
    public Sprite sprite;
    public SpriteRenderer WEAPON_SPRITE_RENDERER;
    public Transform FIRE_POINT;

    public GameObject BULLET;

    public float AIM_THRESHOLD = 0.2f;

    float timer = 0.0f;

    private float recoil = 0.0f;
    public float recoilFactor = 10.0f;

    // Use this for initialization
    void Start()
    {
        WEAPON_SPRITE_RENDERER.sprite = sprite;
    }

    // Update is called once per frame
    void Update()
    {
        // Compensate the input for player turning, i.e. flipping in the x-direction.
        float x = Input.GetAxis("RIGHT_STICK_HORIZONTAL") * transform.parent.localScale.x;
        float y = Input.GetAxis("RIGHT_STICK_VERTICAL") * transform.parent.localScale.x;

        float aimAngle = 0.0f;

        if (Mathf.Abs(x) < AIM_THRESHOLD)
            x = 0.0f;
        if (Mathf.Abs(y) < AIM_THRESHOLD)
            y = 0.0f;

        if (x != 0.0f || y != 0.0f)
        {
            aimAngle = Mathf.Atan2(y, x) * Mathf.Rad2Deg;
            this.transform.rotation = Quaternion.Euler(0.0f, 0.0f, aimAngle);
        }

        if (Input.GetAxis("RIGHT_TRIGGER") > 0.0f && timer <= 0.0f)
        {
            Shoot();
        }

        timer -= Time.deltaTime;
        recoil -= Time.deltaTime;
        recoil = Mathf.Max(0.0f, recoil);

       // Debug.Log(recoil);
    }

    private void Shoot()
    {
        Vector3 parentLocalScale = transform.parent.localScale;

        Vector3 localRot = transform.localEulerAngles;

        // If flipped to the left - flip x-wise
        if (parentLocalScale.x < 0.0f)
        {
            localRot.z *= parentLocalScale.x;
            localRot.z += 180.0f;
        }

        recoil += recoilFactor;

        float recoilDisplacement = Random.Range(-recoil, recoil);
        Debug.Log(recoilDisplacement);
        localRot.z += recoilDisplacement;

        Quaternion spawnRot = Quaternion.Euler(localRot);

        GameObject obj = Instantiate(BULLET, FIRE_POINT.position, spawnRot);
        obj.GetComponent<Attack>().Initiate(0);

        timer = 0.2f;
    }
}
