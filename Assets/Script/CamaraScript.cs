using UnityEngine;

public class CamaraScript : MonoBehaviour
{
    private GameObject John;

    void Start()
    {
        John = GameObject.FindWithTag("Player");
    }

    void Update()
    {
        Vector3 posicion = transform.position;
        posicion.x = John.transform.position.x;
        posicion.y = John.transform.position.y;
        transform.position = posicion;
    }
}