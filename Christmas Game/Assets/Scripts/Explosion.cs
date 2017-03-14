using UnityEngine;

public class Explosion : Attack
{
    private Animator animator;
    private SpriteRenderer spriteRenderer;

    public override void HitPlayerEffect()
    {

    }

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

}
