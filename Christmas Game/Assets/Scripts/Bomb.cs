using System.Collections;
using UnityEngine;

public class Bomb : MonoBehaviour
{
    public GameObject PREFAB_BOMB;
    public GameObject PREFAB_TEXTMESH;
    public Vector3 textOffset;
    private TextMesh textMesh;
    private Rigidbody2D rb2b;

    public float timer = 1.0f;
    public float speed = 1.0f;

    // Use this for initialization
    void Start()
    {
        rb2b = this.gameObject.GetComponent<Rigidbody2D>();
        rb2b.velocity = new Vector2(speed * transform.localScale.x, speed * transform.localScale.y);

        GameObject obj = Instantiate(PREFAB_TEXTMESH, transform.position, Quaternion.identity);
        textMesh = obj.GetComponent<TextMesh>();
        StartCoroutine(UpdateText());
    }

    // Update is called once per frame
    void Update()
    {
        if (timer <= 0.0f)
        {
            timer -= Time.deltaTime;
            Explode();
        }

        UpdateTextPos();

        timer -= Time.deltaTime;
    }

    void UpdateTextPos()
    {
        textMesh.transform.position = transform.position + textOffset;
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.collider.CompareTag("Explosion"))
        {
            StartCoroutine(Explode(0.1f));
        }
    }

    public void Explode()
    {
        StartCoroutine(Explode(0.0f));
    }

    public IEnumerator Explode(float time)
    {
        yield return new WaitForSeconds(time);
        Instantiate(PREFAB_BOMB, transform.position, Quaternion.identity);
        Destroy(textMesh.gameObject);
        Destroy(this.gameObject);
    }

    private IEnumerator UpdateText()
    {
        while (true)
        {
            textMesh.text = Mathf.Floor(timer).ToString();
            yield return new WaitForSeconds(1.0f);
        }
    }
}
