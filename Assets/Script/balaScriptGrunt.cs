using UnityEngine;

public class balaScriptGrunt : MonoBehaviour
{
    private float speed = 2.22f;

    private Rigidbody2D rb2D;
    private Vector2 direccion;

    public void setDireccion(Vector2 dir)
    {
        direccion = dir;
    }

    void Start()
    {
        rb2D = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        rb2D.linearVelocity = direccion * speed;
    }

    public void destroyBala()
    {
        Destroy(gameObject);
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag("Coin") && !collision.CompareTag("Player"))
        {
            destroyBala();
        }
    }
}