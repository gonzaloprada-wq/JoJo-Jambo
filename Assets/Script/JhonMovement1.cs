using System;
using TMPro;
using UnityEditor.Build.Content;
using UnityEngine;
using UnityEngine.SceneManagement;
public class JhonMovement1 : MonoBehaviour
{
    public GameObject BalaPrefab;
    public bool grounded;
    public float speed;
    public float jumpforce;
    public int coins;
    private int salud = 5;

    public TextMeshProUGUI textMonedas;
    public TextMeshProUGUI textVidas;

    public Boolean alive = true;

    private float horizontal;
    private Rigidbody2D rb2D;
    private Animator animator;


    void Start()
    {
        rb2D = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        ActualizaHUD();


    }

    void Update()
    {
        if (alive)
        {


            Debug.DrawRay(transform.position, Vector3.down * 0.1f, Color.red);

            horizontal = Input.GetAxisRaw("Horizontal");
            animator.SetBool("Running", horizontal != 0.0f);


            grounded = Physics2D.Raycast(transform.position, Vector3.down, 0.12f);
            animator.SetBool("En suelo", grounded);

            if (Input.GetKeyDown(KeyCode.W) && grounded)
            {
                Jump();
           
            }

            if (Input.GetKeyDown(KeyCode.Space))
            {
               
                Shoot();
            }
        }
    }

    private void FixedUpdate()
    {
        if (alive)
        {
            rb2D.linearVelocity = new Vector2(horizontal * speed, rb2D.linearVelocity.y);

            if (horizontal < 0.0f)
                transform.localScale = new Vector3(-1.0f, 1.0f, 1.0f);
            else if (horizontal > 0.0f)
                transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
        } }

    private void Jump()
    {
        rb2D.AddForce(Vector2.up * jumpforce);
    }

    private void Shoot()
    {
        Vector3 direccion = Vector3.right;
        if (transform.localScale.x == -1.0f)
            direccion = Vector3.left;

        GameObject bala = Instantiate(
            BalaPrefab,
            transform.position + direccion * 0.1f,
            Quaternion.identity
        );

        bala.GetComponent<balaScript>().setDireccion(direccion);
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Coin"))
        {
            coins++;
            ActualizaHUD();

            Destroy(collision.gameObject);
        }
        if (collision.CompareTag("shelter")) {


            SceneManager.LoadScene("Fase dos");


        }


    }

    public void Hit()
    {
        
        if (alive)
        {
            salud--;
            ActualizaHUD();

            if (salud <= 0)
            {

                execute_death();



            }
            else
            {

                animator.SetTrigger("daño");
                
            }
        }

        
    }
    private void ActualizaHUD()
    {
        textVidas.text = salud.ToString();
        textMonedas.text = coins.ToString();
    }

    public void execute_death()
    {

        animator.SetTrigger("Dead");

        alive = false;

    }


}