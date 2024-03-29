﻿using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour
{
    private UI_Handler UI_HANDLER;
    private int ID;
    private GameHandler gameHandler;
    public WeaponHandler weaponSystem;

    // Prefabs
    [Header("Prefabs")]
    public GameObject PREFAB_BOMB;
    public GameObject PREFAB_TEXT_POPUP;
    public GameObject PREFAB_DEATH_PARTICLES;

    [Header("Transform Points")]
    public Transform groundCheckPoint;
    public Transform throwPoint;
    private float groundCheckRadius = 0.1f;

    [Header("Collision")]
    public LayerMask groundLayer;

    // physics
    private Rigidbody2D rb2d;

    // RENDERING
    [Header("Collision")]
    private Animator animator;
    private SpriteRenderer spriteRenderer;

    // movement variables
    [Header("Gameplay Variables")]
    public float runningSpeed = 1.0f;
    public int MAX_JUMPS = 2;
    public float jumpForce;
    private int jumps = 0;
    private bool isGrounded = false;

    // health variables
    public int MAX_HEALTH = 100;
    private int health;

    [Header("Ammo")]
    private int bombs = 10;

    // Temporary
    private Vector4 oldColor;

    // SOUND EFFECTS
    private AudioSource audioSource;
    public AudioClip SFX_JUMP;
    public AudioClip SFX_DOUBLE_JUMP;
    public AudioClip SFX_TAUNT;
    public AudioClip SFX_HURT;
    public AudioClip SFX_DEATH;

    public void Awake()
    {
        rb2d = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        oldColor = spriteRenderer.color;

        audioSource = GetComponent<AudioSource>();
        UI_HANDLER = GameObject.FindObjectOfType<UI_Handler>();

        isGrounded = true;
        health = MAX_HEALTH;
    }

    public void Initialize(GameHandler gameHandler, int ID)
    {
        this.gameHandler = gameHandler;
        this.ID = ID;
        this.bombs = 5;

        weaponSystem.Initiate(this.ID);
        UI_HANDLER.SetUIBombs(this.ID, bombs.ToString());
    }

    private IEnumerator DamageFlash(float dt)
    {
        spriteRenderer.color = Color.red;
        yield return new WaitForSeconds(dt);
        spriteRenderer.color = oldColor;
    }

    // Update is called once per frame
    public void FixedUpdate()
    {
        CheckGrounded();
    }

    public void Update()
    {
        Move();

        if (Input.GetButtonDown(JoystickControlls.START[ID]))
        {
            gameHandler.Pause();
        }
    }

    private void CheckGrounded()
    {
        isGrounded = Physics2D.OverlapCircle(groundCheckPoint.position, groundCheckRadius, groundLayer);
        if (isGrounded)
            jumps = 0;

        animator.SetBool("Grounded", isGrounded);
    }

    public void TakeDamage(int dmg, int attackerID)
    {
        health -= dmg;

        StartCoroutine(DamageFlash(0.05f));

        Debug.Log("Player " + ID + " took " + dmg + " dmg from Player " + attackerID + " at time " + Time.realtimeSinceStartup);
        GameObject g = Instantiate(PREFAB_TEXT_POPUP, transform.position + 0.5f * new Vector3(0.0f, 0.0f, -0.1f), Quaternion.identity);
        g.GetComponent<TextMesh>().text = "-" + dmg;

        if (health <= 0)
        {
            Instantiate(PREFAB_DEATH_PARTICLES, transform.position, Quaternion.identity);
            gameHandler.RemovePlayer(ID, attackerID);

            audioSource.PlayOneShot(SFX_DEATH);
            Destroy(gameObject);
        }
    }

    public void AddHealth(int h)
    {
        health += h;
    }

    private void Move()
    {
        float x = Input.GetAxis(JoystickControlls.LEFT_HORIZONTAL[ID]);

        rb2d.velocity = new Vector2(runningSpeed * x, rb2d.velocity.y);

        SetFacingDirection(x);

        animator.SetFloat("Speed", Mathf.Abs(x));

        if (Input.GetButtonDown(JoystickControlls.RIGHT_BUMPER[ID]) && jumps < MAX_JUMPS - 1)
        {
            Jump();
        }

        if (Input.GetButtonDown(JoystickControlls.LEFT_BUMPER[ID]) && bombs > 0)
        {
            GameObject obj = Instantiate(PREFAB_BOMB, throwPoint.position, throwPoint.rotation);
            obj.transform.localScale = transform.localScale;

            obj.GetComponent<Bomb>().Initiate(ID, 10);

            bombs--;
            UI_HANDLER.SetUIBombs(ID, bombs.ToString());
        }

        if (Input.GetButtonDown(JoystickControlls.Y[ID]))
        {
            animator.SetTrigger("Taunt");
            audioSource.PlayOneShot(SFX_DEATH);
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
        rb2d.velocity = new Vector2(rb2d.velocity.x, jumpForce);
        jumps++;

        if (jumps == 1)
        {
            audioSource.PlayOneShot(SFX_JUMP);
        }
        else if (jumps == 2)
        {
            audioSource.PlayOneShot(SFX_DOUBLE_JUMP);
        }
    }

    public bool GetIsGrounded()
    {
        return isGrounded;
    }

    public int GetID()
    {
        return ID;
    }

    public void ApplyVelocity(Vector3 vel)
    {
        //rb2d.AddForce(force);
        rb2d.velocity = vel;
    }


}