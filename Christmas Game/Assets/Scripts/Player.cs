using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour
{
    // components
    private Rigidbody2D mRb2d;
    // private Animator mAnimator;
    private SpriteRenderer mSpriteRenderer;
    public Transform mGroundChecker;
    private float mGroundCheckRadius = 0.1f;
    public LayerMask mWhatIsGround;

    // movement variables
    public float mSpeed = 1.0f;
    public int MAX_JUMPS = 2;
    public float mJumpForce;
    private int mJumps = 0;
    private bool mIsGrounded = false;

    // ladder variables
    // private bool mIsOnLadder = false;
    // public float mClimbSpeed;
    // private float mGravityStore;

    // health variables
    public int MAX_HEALTH = 3;
    private int mHealth;

    // knockback variables
    public float mKnockBackSpeed;
    private bool mIsKnockBackRight;
    public float MAX_KNOCK_BACK_TIME;
    public float mKnockBackTimer = 0.0f;

    // Controller Variables
    public KeyCode KEY_LEFT;
    public KeyCode KEY_RIGHT;
    public KeyCode KEY_JUMP;
    public KeyCode KEY_SHOOT;

    public void Awake()
    {
        mRb2d = GetComponent<Rigidbody2D>();
        //   mAnimator = GetComponent<Animator>();
        //   mSpriteRenderer = GetComponent<SpriteRenderer>();

        mIsGrounded = true;
        mHealth = MAX_HEALTH;

        //  mGravityStore = mRb2d.gravityScale;
    }

    private IEnumerator DamageFlash(float dt)
    {
        mSpriteRenderer.color = Color.red;
        yield return new WaitForSeconds(dt);
        mSpriteRenderer.color = Color.white;
    }

    // Update is called once per frame
    public void FixedUpdate()
    {
        CheckGrounded();
    }

    public void Update()
    {
        Move();
        UpdateAnimations();

        mKnockBackTimer -= Time.deltaTime;
    }

    private void KnockBack()
    {
        if (mIsKnockBackRight)
        {
            //mRb2d.velocity = new Vector2(-mKnockBackSpeed, mKnockBackSpeed);
            mRb2d.velocity = Vector2.left * mKnockBackSpeed;
        }
        else
        {
            // mRb2d.velocity = new Vector2(mKnockBackSpeed, mKnockBackSpeed);
            mRb2d.velocity = Vector2.right * mKnockBackSpeed;
        }
    }

    public void EnableKnockBack() { mKnockBackTimer = MAX_KNOCK_BACK_TIME; }

    private void CheckGrounded()
    {
        mIsGrounded = Physics2D.OverlapCircle(mGroundChecker.position, mGroundCheckRadius, mWhatIsGround);
        if (mIsGrounded)
            mJumps = 0;

        //mAnimator.SetBool("Grounded", mIsGrounded);
    }

    public void TakeDamage(int dmg)
    {
        mHealth -= dmg;

        if (mHealth <= 0)
        {
            Destroy(gameObject);
        }
    }

    public void AddHealth(int h)
    {
        mHealth += h;
    }

    private void UpdateAnimations()
    {
        /*  mAnimator.SetBool("Running", Mathf.Abs(Input.GetAxis("Horizontal")) > 0.1f);

          mAnimator.SetBool("OnLadder", mIsOnLadder);

          mAnimator.SetBool("Climbing", mIsOnLadder && Mathf.Abs(Input.GetAxis("Vertical")) > 0.1f); */
    }

    private void Move()
    {
        float x = 0.0f;
        // check keybord input
        //x = Input.GetAxis("Horizontal");

        if (Input.GetKey(KEY_LEFT))
        {
            x = -1.0f;
        }else if (Input.GetKey(KEY_RIGHT))
        {
            x = 1.0f;
        }

        mRb2d.velocity = new Vector2(mSpeed * x, mRb2d.velocity.y);

        SetFacingDirection(x);

        if (Input.GetKeyDown(KEY_JUMP) && mJumps < MAX_JUMPS - 1)
        {
            Jump();
        }

        if (Input.GetKeyDown(KEY_SHOOT))
        {
            Debug.Log("When you gotta shoot, shoot. Don't talk.");
        }
    }

    private void SetFacingDirection(float xAxis)
    {
        if (xAxis <= -0.1f)
            transform.localScale = new Vector3(-1.0f, 1.0f, 1.0f);
        else if (xAxis >= 0.1f)
            transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
    }

    public void Jump()
    {
        mRb2d.velocity = new Vector2(mRb2d.velocity.x, mJumpForce);
        ++mJumps;
    }

    public bool GetIsGrounded()
    {
        return mIsGrounded;
    }

    public void OnCollisionEnter2D(Collision2D other)
    {
        /*      if (other.gameObject.CompareTag("Enemy"))
              {
                  TakeDamage(1);
                  mIsKnockBackRight = transform.position.x < other.transform.position.x;
                  EnableKnockBack();
                  StartCoroutine(DamageFlash(0.5f * mKnockBackTimer));
              }*/
    }
}