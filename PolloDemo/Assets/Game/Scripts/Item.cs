using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    public ParticleSystem SisParticulas;
    public Transform Modelo;
    public GameObject Plano;
    public int PuntosEnergia;
    public float VelocidadDeFrente = 1f;
    public float VelocidadDeRotacion = 10f;
    public float VelocidadHaciaPlayer = 4f;
    public float DistanciaMinDesaparecer = 1.5f;

    private Vector3 direccionRotacion;
    private bool playerAlcanzoItem;
    private Vector3 posicionAntesDelContacto;

    //private ParticleSystem.LimitVelocityOverLifetimeModule limitVelocityModule;
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

            //transform.position = CharacterManager.CharacterManagerInstance._Character.transform.position + posicionAntesDelContacto;
            transform.position = Vector3.Lerp(transform.position,
            CharacterManager.CharacterManagerInstance._Character.transform.position,
            VelocidadHaciaPlayer * Time.deltaTime);

            //if (Vector3.Distance(
            //   CharacterManager.CharacterManagerInstance._Character.transform.position,
            //   transform.position) > DistanciaMinDesaparecer)
            //{
            //    transform.position = Vector3.Lerp(transform.position,
            //   CharacterManager.CharacterManagerInstance._Character.transform.position,
            //   VelocidadHaciaPlayer * Time.deltaTime);


            //}
            //else
            //{
            //    GameManager.GameManagerInstance._UiManager.MostrarPuntosPositivos(PuntosEnergia, transform.position);
            //    CharacterManager.CharacterManagerInstance._Character.EnergiaRecibida(PuntosEnergia);
            //    Destroy(gameObject);

            //}
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("PlayerShield"))
        {
            //limitVelocityModule = SisParticulas.limitVelocityOverLifetime;
            //limitVelocityModule.limit = 10f;
            posicionAntesDelContacto = transform.position - CharacterManager.CharacterManagerInstance._Character.transform.position;
            playerAlcanzoItem = true;
            Plano.SetActive(false);
            Modelo.gameObject.SetActive(false);
            StartCoroutine(EsperarEmisionTermine());
            //gameObject.SetActive(false);
        }
    }

    IEnumerator EsperarEmisionTermine()
    {
        SisParticulas.Stop();

        while (SisParticulas.isEmitting)
        {
            yield return null;
        }
        GameManager.GameManagerInstance._UiManager.MostrarPuntosPositivos(PuntosEnergia, transform.position);
        CharacterManager.CharacterManagerInstance._Character.EnergiaRecibida(PuntosEnergia);
        //Destroy(gameObject);
    }
}
