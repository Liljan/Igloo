using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour
{
    private char mID;
    private GameHandler mGameHandler;

    // Prefabs
    [Header("Prefabs")]
    public GameObject PREFAB_SNOWBALL;
    public GameObject PREFAB_BOMB;

    public GameObject PREFAB_DEATH_PARTICLES;

    [Header("Transform Points")]
    public Transform mFirePoint;
    public Transform mGroundChecker;
    private float mGroundCheckRadius = 0.1f;

    [Header("Collision")]
    public LayerMask mWhatIsGround;

    // Controller Variables
    [Header("Controls")]
    public KeyCode KEY_LEFT;
    public KeyCode KEY_RIGHT;
    public KeyCode KEY_JUMP;
    public KeyCode KEY_SHOOT;
    public KeyCode KEY_BOMB;

    // components
    private Rigidbody2D mRb2d;
    private Animator mAnimator;
    private SpriteRenderer mSpriteRenderer;

    // movement variables
    [Header("Gameplay Variables")]
    public float mSpeed = 1.0f;
    public int MAX_JUMPS = 2;
    public float mJumpForce;
    private int mJumps = 0;
    private bool mIsGrounded = false;

    // health variables
    public int MAX_HEALTH = 3;
    private int mHealth;

    [Header("Ammo")]
    private int m_amount_of_shots;
    private int m_amount_of_bombs;

    // knockback variables
    public float mKnockBackSpeed;
    private bool mIsKnockBackRight;
    public float MAX_KNOCK_BACK_TIME;
    public float mKnockBackTimer = 0.0f;

    public void Awake()
    {
        mRb2d = GetComponent<Rigidbody2D>();
        mAnimator = GetComponent<Animator>();
        mSpriteRenderer = GetComponent<SpriteRenderer>();

        mIsGrounded = true;
        mHealth = MAX_HEALTH;
    }

    private void OnDestroy()
    {
        mGameHandler.RemovePlayer(mID);
    }

    public void Init(GameHandler gh, char ID, int shots, int bombs)
    {
        mGameHandler = gh;
        mID = ID;
        m_amount_of_shots = shots;
        m_amount_of_bombs = bombs;
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

        mAnimator.SetBool("Grounded", mIsGrounded);
    }

    public void TakeDamage(int dmg)
    {
        mHealth -= dmg;

        if (mHealth <= 0)
        {
            Instantiate(PREFAB_DEATH_PARTICLES, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }

    public void AddHealth(int h)
    {
        mHealth += h;
    }

    private void Move()
    {
        float x = 0.0f;
        // check keybord input
        //x = Input.GetAxis("Horizontal");

        if (Input.GetKey(KEY_LEFT))
        {
            x = -1.0f;
        }
        else if (Input.GetKey(KEY_RIGHT))
        {
            x = 1.0f;
        }

        mRb2d.velocity = new Vector2(mSpeed * x, mRb2d.velocity.y);

        SetFacingDirection(x);
        // update animation
        mAnimator.SetFloat("Speed", Mathf.Abs(x));

        if (Input.GetKeyDown(KEY_JUMP) && mJumps < MAX_JUMPS - 1)
        {
            Jump();
        }

        if (Input.GetKeyDown(KEY_SHOOT))
        {
            GameObject obj = Instantiate(PREFAB_SNOWBALL, mFirePoint.position, mFirePoint.rotation);
            obj.transform.localScale = transform.localScale;
            mAnimator.SetTrigger("Throw");
        }

        if (Input.GetKeyDown(KEY_BOMB) && m_amount_of_bombs > 0)
        {
            GameObject obj = Instantiate(PREFAB_BOMB, mFirePoint.position, mFirePoint.rotation);
            obj.transform.localScale = transform.localScale;
            mAnimator.SetTrigger("Throw");

            m_amount_of_bombs--;
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