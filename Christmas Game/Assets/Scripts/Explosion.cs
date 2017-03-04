using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    private Animator animator;

    public int damage = 1;

    // Use this for initialization
    void Start()
    {
        animator = GetComponent<Animator>();

        Destroy(this.gameObject, animator.GetCurrentAnimatorStateInfo(0).length);
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.collider.CompareTag("Player"))
        {
            Debug.Log("Part time");

            Player player = col.gameObject.GetComponent<Player>();
            player.TakeDamage(damage);
        }
    }
}
