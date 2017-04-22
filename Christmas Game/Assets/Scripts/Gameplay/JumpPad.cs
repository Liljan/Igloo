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
        animator.SetTrigger("Jump");

        if (col.tag.Equals("Player"))
        {
            Player p = col.GetComponent<Player>();

            if (p)
            {
                p.ApplyVelocity(magnitude * Vector3.up);
                Debug.Log("pushed");
            }
        }
    }
}
