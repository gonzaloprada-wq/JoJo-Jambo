using System;
using System.Collections;
using System.Threading.Tasks;
using UnityEngine;

public class GruntMovement : MonoBehaviour
{
    public GameObject balaPrefab;
    private GameObject John;
    private JhonMovement1 johnScript;
    private float distancia;
    private float ultimoDisparo;
    private int salud = 2;
    public Boolean alive = true;

    
    private Animator animator;

    void Start()
    {
        John = GameObject.FindWithTag("Player");
        if (John != null)
            johnScript = John.GetComponent<JhonMovement1>();
        // Inicializa el animator para poder disparar triggers de animación
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        if (johnScript.alive && alive)
        {
            distancia = Mathf.Abs(John.transform.position.x - transform.position.x);

            Vector3 direccion = John.transform.position - transform.position;

            if (direccion.x > 0.0f)
                transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
            else
                transform.localScale = new Vector3(-1.0f, 1.0f, 1.0f);

            if (distancia < 1.0f && Time.time > ultimoDisparo + 0.5f)
            {
                Shoot();
                ultimoDisparo = Time.time;
            }
        }
    }

    private void Shoot()
    {
    
        if (johnScript.alive && alive)
        {
            
            Vector3 direccion = Vector3.right;
            if (transform.localScale.x == -1.0f)
                direccion = Vector3.left;

            GameObject bala = Instantiate(
                balaPrefab,
                transform.position + direccion * 0.1f,
                Quaternion.identity
            );

            bala.GetComponent<balaScript>().setDireccion(direccion);
        }
    }

    public void Hit()
    {

        salud--;

        if (salud <= 0)
        {
            execute_death();
      


        }

    }
    public void execute_death()
    {

        if (animator != null)
            animator.SetTrigger("Dead_Enemy");
            StartCoroutine(eliminar_objeto());

        alive = false;

    }
    private IEnumerator eliminar_objeto()
    {
        yield return new WaitForSeconds(0.7f);
        Destroy(gameObject);
    }
}
