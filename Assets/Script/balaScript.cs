using UnityEngine;

public class balaScript : MonoBehaviour
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
        Destroy(gameObject, 3f);
    }

    void Update()
    {
        rb2D.linearVelocity = direccion * speed;
    }

    public void destroyBala()
    {
        Destroy(gameObject);
    }

    public void OnTriggerEnter2D(Collider2D collider)
    {

        JhonMovement1 jhon = collider.GetComponent<JhonMovement1>();
        GruntMovement grunt = collider.GetComponent<GruntMovement>();

        if (jhon != null) jhon.Hit();
        if (grunt != null) grunt.Hit();

        if (!collider.CompareTag("Coin") && !collider.CompareTag("bala"))
        {
            destroyBala();
       
        }
    }


  

}