using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    public Transform Modelo;
    public int PuntosEnergia;
    public float VelocidadDeFrente = 1f;
    public float VelocidadDeRotacion = 10f;
    public float VelocidadHaciaPlayer = 4f;
    public float DistanciaMinDesaparecer = 1.5f;

    private Vector3 direccionRotacion;
    private bool playerAlcanzoItem;

    void Start()
    {
        direccionRotacion = Vector3.up;    
    }

    // Update is called once per frame
    void Update()
    {
        Modelo.rotation *= Quaternion.AngleAxis(VelocidadDeRotacion * Time.deltaTime, direccionRotacion);

        if (!playerAlcanzoItem)
        {
            if(VelocidadDeFrente != 0f)
            {
                transform.position += transform.forward * VelocidadDeFrente * Time.deltaTime;
            }
        }
        else
        {
            if (Vector3.Distance(
               CharacterManager.CharacterManagerInstance._Character.transform.position,
               transform.position) > DistanciaMinDesaparecer)
            {
                transform.position = Vector3.Lerp(transform.position,
               CharacterManager.CharacterManagerInstance._Character.transform.position,
               VelocidadHaciaPlayer * Time.deltaTime);
            }
            else
            {
                GameManager.GameManagerInstance._UiManager.MostrarPuntosPositivos(PuntosEnergia, transform.position);
                CharacterManager.CharacterManagerInstance._Character.EnergiaRecibida(PuntosEnergia);
                Destroy(gameObject);
                //gameObject.SetActive(false);
            }
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            playerAlcanzoItem = true;
            //gameObject.SetActive(false);
        }
    }
}
