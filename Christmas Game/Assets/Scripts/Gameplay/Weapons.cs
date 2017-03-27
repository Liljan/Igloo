using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapons : MonoBehaviour
{

    public float AIM_THRESHOLD = 0.2f;

    // Use this for initialization
    void Start()
    {

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
    }
}
