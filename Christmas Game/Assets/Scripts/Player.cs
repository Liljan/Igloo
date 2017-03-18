﻿using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour
{
    private int mID;
    private GameHandler mGameHandler;

    // Prefabs
    [Header("Prefabs")]
    public GameObject PREFAB_BOMB;
    public GameObject PREFAB_TEXT_POPUP;
    public GameObject PREFAB_DEATH_PARTICLES;

    public RangedWeapon weapon;

    [Header("Transform Points")]
    public Transform mGroundChecker;
    private float mGroundCheckRadius = 0.1f;

    [Header("Collision")]
    public LayerMask mWhatIsGround;

    // Controller Variables
    [Header("Controls")]
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
    public int MAX_HEALTH = 100;
    private int mHealth;

    [Header("Ammo")]
    private int mAmmo;
    private int mNumberOfBombs;

    // Temporary
    private Vector4 oldColor;

    public void Awake()
    {
        mRb2d = GetComponent<Rigidbody2D>();
        mAnimator = GetComponent<Animator>();
        mSpriteRenderer = GetComponent<SpriteRenderer>();
        oldColor = mSpriteRenderer.color;

        mIsGrounded = true;
        mHealth = MAX_HEALTH;
    }

    public void Init(GameHandler gh, int ID, int shots, int bombs)
    {
        mGameHandler = gh;
        mID = ID;
        mAmmo = shots;
        mNumberOfBombs = bombs;
    }

    private IEnumerator DamageFlash(float dt)
    {
        mSpriteRenderer.color = Color.red;
        yield return new WaitForSeconds(dt);
        mSpriteRenderer.color = oldColor;
    }

    // Update is called once per frame
    public void FixedUpdate()
    {
        CheckGrounded();
    }

    public void Update()
    {
        Move();
    }

    private void CheckGrounded()
    {
        mIsGrounded = Physics2D.OverlapCircle(mGroundChecker.position, mGroundCheckRadius, mWhatIsGround);
        if (mIsGrounded)
            mJumps = 0;

        mAnimator.SetBool("Grounded", mIsGrounded);
    }

    public void TakeDamage(int dmg, int attackerID)
    {
        mHealth -= dmg;

        StartCoroutine(DamageFlash(0.05f));

        Debug.Log("Player " + mID + " took " + dmg + " dmg from Player " + attackerID + " at time " + Time.realtimeSinceStartup);
        GameObject g = Instantiate(PREFAB_TEXT_POPUP, transform.position + 0.5f * new Vector3(0.0f, 0.0f, -0.1f), Quaternion.identity);
        g.GetComponent<TextMesh>().text = "-" + dmg;


        if (mHealth <= 0)
        {
            Instantiate(PREFAB_DEATH_PARTICLES, transform.position, Quaternion.identity);
            mGameHandler.RemovePlayer(mID, attackerID);
            Destroy(gameObject);
        }
    }

    public void AddHealth(int h)
    {
        mHealth += h;
    }

    private void Move()
    {
        float x = Input.GetAxis("LEFT_STICK_HORIZONTAL");

        mRb2d.velocity = new Vector2(mSpeed * x, mRb2d.velocity.y);

        SetFacingDirection(x);
        // update animation
        mAnimator.SetFloat("Speed", Mathf.Abs(x));

        if (Input.GetButtonDown("Jump") && mJumps < MAX_JUMPS - 1)
        {
            Jump();
        }

        /*
        if (Input.GetKeyDown(KEY_SHOOT) && mAmmo > 0)
        {
            --mAmmo;
            mGameHandler.SetAmountOfAmmo(mID, mAmmo);
        } */

        if (Input.GetKeyDown(KEY_BOMB) && mNumberOfBombs > 0)
        {
            /*GameObject obj = Instantiate(PREFAB_BOMB, mFirePoint.position, mFirePoint.rotation);
            obj.transform.localScale = transform.localScale;

            obj.GetComponent<Bomb>().Initiate(mID); */

            mNumberOfBombs--;
            mGameHandler.SetAmountOfBombs(mID, mNumberOfBombs);
        }
    }

    private void SetFacingDirection(float xAxis)
    {
        if (xAxis <= -0.1f)
        {
            transform.localScale = new Vector3(-1.0f, 1.0f, 1.0f);
        }
        else if (xAxis >= 0.1f)
        {
            transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
        }
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

    public int GetID()
    {
        return mID;
    }
}