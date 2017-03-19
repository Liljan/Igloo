using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : Attack
{
    public GameObject PREFAB_PARTICLES;

    public float speed = 15;
    private Rigidbody2D rb2b;

    // Use this for initialization
    void Awake()
    {
        rb2b = this.gameObject.GetComponent<Rigidbody2D>();

        float angle = transform.rotation.eulerAngles.z * Mathf.Deg2Rad;

        Debug.Log("Global rotation angle: " + transform.rotation.eulerAngles.z);
        //Debug.Log()

        rb2b.velocity = new Vector2(speed * Mathf.Cos(angle), speed * Mathf.Sin(angle));
    }

    public override void HitPlayerEffect()
    {
        GameObject obj = Instantiate(PREFAB_PARTICLES, transform.position, transform.rotation);
        //obj.transform.localScale = transform.localScale;
        Destroy(this.gameObject);
    }
}
