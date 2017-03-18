using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedWeapon : MonoBehaviour
{

    public float aimThreshold = 0.2f;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        float x = Input.GetAxis("RIGHT_STICK_HORIZONTAL");
        float y = -Input.GetAxis("RIGHT_STICK_VERTICAL");

         float aimAngle = 0.0f;

            if (Mathf.Abs(x) < aimThreshold)
                x = 0.0f;
            if (Mathf.Abs(y) < aimThreshold)
                y = 0.0f;

            if (x != 0.0f || y != 0.0f)
                aimAngle = Mathf.Atan2(y, x) * Mathf.Rad2Deg;

            this.transform.rotation = Quaternion.Euler(0.0f, 0.0f, aimAngle);

    }
}
