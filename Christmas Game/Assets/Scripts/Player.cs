using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour
{
	private int ID;
    private GameHandler gameHandler;
    public Weapons weaponSystem;

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
	private int ammo;
	private int bombs = 3;

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

        isGrounded = true;
        health = MAX_HEALTH;
    }

    public void Initialize(GameHandler gameHandler, int ID)
    {
		this.gameHandler = gameHandler;
        this.ID = ID;
        this.bombs = 5;

        weaponSystem.Initiate(this.ID);
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
        float x = Input.GetAxis("LEFT_STICK_HORIZONTAL");

        rb2d.velocity = new Vector2(runningSpeed * x, rb2d.velocity.y);

        SetFacingDirection(x);
        // update animation
        animator.SetFloat("Speed", Mathf.Abs(x));

        if (Input.GetButtonDown("JUMP") && jumps < MAX_JUMPS - 1)
        {
            Jump();
        }

		if (Input.GetButtonDown("THROW_GRENADE") && bombs > 0)
        {
			GameObject obj = Instantiate(PREFAB_BOMB, throwPoint.position, throwPoint.rotation);
            obj.transform.localScale = transform.localScale;

            obj.GetComponent<Bomb>().Initiate(ID,1);

            bombs--;
            //mGameHandler.SetAmountOfBombs(mID, mNumberOfBombs);
        }

        if (Input.GetButtonDown("TAUNT"))
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
}