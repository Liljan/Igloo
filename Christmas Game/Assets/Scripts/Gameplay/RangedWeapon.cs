﻿using System.Collections;
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

    // Use this for initialization
    void Start()
    {
        WEAPON_SPRITE_RENDERER.sprite = sprite;
    }

    // Update is called once per frame
    void Update()
    {
        float x = Input.GetAxis("RIGHT_STICK_HORIZONTAL");
        float y = Input.GetAxis("RIGHT_STICK_VERTICAL");

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
            Vector3 parentLocalScale = transform.parent.localScale;
            Debug.Log("Parent local scale: " + parentLocalScale);

            Vector3 localRot = transform.localEulerAngles;

            if (parentLocalScale.x < 0.0f)
            {
                Debug.Log("Local rotation: " + localRot);
                localRot.z *= parentLocalScale.x;
                localRot.z += 180.0f;
            }

            Quaternion spawnRot = Quaternion.Euler(localRot);

            GameObject obj = Instantiate(BULLET, FIRE_POINT.position, spawnRot);

            Projectile bullet = obj.GetComponent<Projectile>();
            bullet.Initiate(0);// instantiate with ID 0


            timer = 0.2f;
        }

        timer -= Time.deltaTime;


    }
}
