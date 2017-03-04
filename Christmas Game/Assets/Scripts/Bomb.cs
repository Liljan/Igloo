using UnityEngine;

public class Bomb : MonoBehaviour
{
    public GameObject PREFAB_BOMB;
    private Rigidbody2D rb2b;

    public float timer = 1.0f;
    public float speed = 1.0f;

    // Use this for initialization
    void Start()
    {
        rb2b = this.gameObject.GetComponent<Rigidbody2D>();
        rb2b.velocity = new Vector2(speed * transform.localScale.x, speed * transform.localScale.y);
    }

    // Update is called once per frame
    void Update()
    {
        if (timer <= 0.0f)
        {
            timer -= Time.deltaTime;
            Instantiate(PREFAB_BOMB, transform.position, Quaternion.identity);
            Destroy(this.gameObject);
        }

        timer -= Time.deltaTime;
    }
}
