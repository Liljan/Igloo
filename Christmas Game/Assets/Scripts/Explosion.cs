using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    private Animator animator;
    private SpriteRenderer spriteRenderer;

    public int damage = 1;

    // Use this for initialization
    void Start()
    {
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();

        if (Random.value > 0.5f)
            spriteRenderer.flipX = true;
        if (Random.value > 0.5f)
            spriteRenderer.flipY = true;

        Destroy(this.gameObject, animator.GetCurrentAnimatorStateInfo(0).length);
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.collider.CompareTag("Player"))
        {
            Player player = col.gameObject.GetComponent<Player>();
            player.TakeDamage(damage);
        }
    }
}
