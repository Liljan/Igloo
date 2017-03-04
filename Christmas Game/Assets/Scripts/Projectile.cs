using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public GameObject PREFAB_PARTICLES;

    public Vector2 speed;
    private Rigidbody2D rb2b;

    // Use this for initialization
    void Start()
    {
        rb2b = this.gameObject.GetComponent<Rigidbody2D>();
        rb2b.velocity = new Vector2(speed.x * transform.localScale.x, speed.y * transform.localScale.y);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Destroy(this.gameObject);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        GameObject obj = Instantiate(PREFAB_PARTICLES, transform.position, transform.rotation);
        obj.transform.localScale = transform.localScale;
        Destroy(this.gameObject);
    }
}
