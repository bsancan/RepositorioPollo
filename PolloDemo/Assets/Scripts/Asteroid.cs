using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroid : MonoBehaviour
{

    public enum DireccionRotacionAst
    {
        Ninguna, Arriba, Abajo, Derecha, Izquierda, DeFrente, Atras
    };

    public Transform Modelo;
    public int ValorDaño = 20;
    public int ValorEscudo = 20;
    public float VelocidadDeFrente = 0f;
    public float VelocidadRotation = 10f;
    public DireccionRotacionAst DireccionRotacion;
    public bool HabilitarAutoRotacion = true;

    private Vector3 resultadoRotacion;
    private int escudoActual;

    void Start()
    {
        
    }

    private void OnEnable()
    {
        escudoActual = ValorEscudo;
        if (HabilitarAutoRotacion)
        {
            int r = Random.Range(0, 6);

            if (r == 1) //derecha
                resultadoRotacion = Vector3.right;
            else if (r == 2) // izquier
                resultadoRotacion = -Vector3.up;
            else if (r == 3)
                resultadoRotacion = Vector3.forward;
            else if (r == 4)
                resultadoRotacion = -Vector3.forward;
            else if (r == 5) // arriba
                resultadoRotacion = Vector3.up;
            else if (r == 6) // abajo
                resultadoRotacion = -Vector3.up;
        }
        else
        {
            if (DireccionRotacion == DireccionRotacionAst.Arriba) //derecha
                resultadoRotacion = Vector3.right;
            else if (DireccionRotacion == DireccionRotacionAst.Abajo) // izquier
                resultadoRotacion = -Vector3.up;
            else if (DireccionRotacion == DireccionRotacionAst.DeFrente)
                resultadoRotacion = Vector3.forward;
            else if (DireccionRotacion == DireccionRotacionAst.Atras)
                resultadoRotacion = -Vector3.forward;
            else if (DireccionRotacion == DireccionRotacionAst.Derecha) // arriba
                resultadoRotacion = Vector3.up;
            else if (DireccionRotacion == DireccionRotacionAst.Izquierda) // abajo
                resultadoRotacion = -Vector3.up;
        }
    }


    void Update()
    {
        Modelo.rotation *= Quaternion.AngleAxis(VelocidadRotation * Time.deltaTime, resultadoRotacion);


        if (VelocidadDeFrente != 0f)
            transform.position += transform.forward * VelocidadDeFrente * Time.deltaTime;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("PlayerAmmo"))
        {
            TakeDamage(other.GetComponent<PlayerAmmo>().ValorDaño);
        }
        //else if (other.gameObject.CompareTag("Player"))
        //{
        //    gameObject.SetActive(false);
        //    ExplosionManager.explosionManagerInstance.SpawnExplosion(transform.position);
        //}
    }

    private void TakeDamage(int dañoRecibido)
    {
        int sum = escudoActual - dañoRecibido;
        if (sum > 0)
        {
            escudoActual = sum;
            GameManager.GameManagerInstance._UiManager.MostrarPuntosDaño(dañoRecibido, transform.position);

            //print(currentStamina);
        }
        else
        {
            if (gameObject.activeInHierarchy)
            {
                gameObject.SetActive(false);
                GameManager.GameManagerInstance._UiManager.MostrarPuntosDaño(dañoRecibido, transform.position);
                //ExplosionManager.explosionManagerInstance.SpawnExplosion(transform.position, AsteroidModelScale);
                //cuento la destruccion como puntaje
                //UIManager.uiManagerInstance.scoreManager.currentPlayerScore += valueDamage;
            }
            escudoActual = 0;
        }

    }
}
