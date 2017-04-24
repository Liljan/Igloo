using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpPad : MonoBehaviour
{
    public float magnitude = 10.0f;

    private Animator animator;

    public void Awake()
    {
        animator = GetComponent<Animator>();
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        

        if (col.tag.Equals("Player"))
        {
            Player p = col.GetComponent<Player>();

            if (p)
            {
                animator.SetTrigger("Jump");
                p.ApplyVelocity(magnitude * Vector3.up);
            }
        }
    }
}
