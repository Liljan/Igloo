using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpPad : MonoBehaviour
{
    public float magnitude = 10.0f;

    private Vector3 direction;

    private Animator animator;

    public void Awake()
    {
        animator = GetComponent<Animator>();
        direction = transform.up;
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.tag.Equals("Player"))
        {
            Player p = col.GetComponent<Player>();

            if (p)
            {
                animator.SetTrigger("Jump");
                p.ApplyVelocity(magnitude * direction);
            }
        }
    }
}
