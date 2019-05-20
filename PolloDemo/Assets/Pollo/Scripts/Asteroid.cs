using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroid : MonoBehaviour
{

    public enum DireccionRotacionAst
    {
        Arriba, Abajo, Derecha, Izquierda, DeFrente, Atras
    };

    public Transform Modelo;
    public Vector3 ReEscalaExplosion = Vector3.one;
    public float TiempoVidaExplosion = 1.2f;
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
            int r = Random.Range(0, 5);

            if (r == 0) 
                resultadoRotacion = Vector3.right;
            else if (r == 1) 
                resultadoRotacion = Vector3.left;
            else if (r == 2)
                resultadoRotacion = Vector3.down;
            else if (r == 3)
                resultadoRotacion = Vector3.up;
            else if (r == 4) // arriba
                resultadoRotacion = Vector3.back;
            else if (r == 5) // abajo
                resultadoRotacion = Vector3.forward;
        }
        else
        {
            if (DireccionRotacion == DireccionRotacionAst.Arriba)
                resultadoRotacion = Vector3.right;
            else if (DireccionRotacion == DireccionRotacionAst.Abajo)
                resultadoRotacion = Vector3.left;
            else if (DireccionRotacion == DireccionRotacionAst.Derecha)
                resultadoRotacion = Vector3.down;
            else if (DireccionRotacion == DireccionRotacionAst.Izquierda)
                resultadoRotacion = Vector3.up;
            else if (DireccionRotacion == DireccionRotacionAst.DeFrente)
                resultadoRotacion = Vector3.back;
            else if (DireccionRotacion == DireccionRotacionAst.Atras)
                resultadoRotacion = Vector3.forward;
            //if (DireccionRotacion == DireccionRotacionAst.Arriba) 
            //    resultadoRotacion = Vector3.right;
            //else if (DireccionRotacion == DireccionRotacionAst.Abajo) 
            //    resultadoRotacion = -Vector3.up;
            //else if (DireccionRotacion == DireccionRotacionAst.DeFrente)
            //    resultadoRotacion = Vector3.forward;
            //else if (DireccionRotacion == DireccionRotacionAst.Atras)
            //    resultadoRotacion = -Vector3.forward;
            //else if (DireccionRotacion == DireccionRotacionAst.Derecha) 
            //    resultadoRotacion = Vector3.up;
            //else if (DireccionRotacion == DireccionRotacionAst.Izquierda) 
            //    resultadoRotacion = -Vector3.up;
        }
    }


    void Update()
    {
        Modelo.rotation *= Quaternion.AngleAxis(VelocidadRotation * Time.deltaTime, resultadoRotacion);


        if (VelocidadDeFrente != 0f)
            transform.position += transform.forward * VelocidadDeFrente * Time.deltaTime;
    }

    //private void OnTriggerEnter(Collider other)
    //{
    //    if (other.gameObject.CompareTag("PlayerAmmo"))
    //    {
    //        //aplico daño al asteroide
    //        TakeDamage(other.GetComponent<PlayerAmmo>().ValorDaño);

    //    }else if (other.gameObject.CompareTag("PlayerShield") || other.gameObject.CompareTag("Player"))
    //    {
    //        gameObject.SetActive(false);
    //        GameManager.GameManagerInstance._ExplotionManager.ObtenerExplosion(transform, ReEscalaExplosion, TiempoVidaExplosion);
    //    }
    //}

    public void TakeDamage(int dañoRecibido)
    {
        int sum = escudoActual - dañoRecibido;
        if (sum > 0)
        {
            escudoActual = sum;
            GameManager.GameManagerInstance._UiManager.MostrarPuntosNegativos(dañoRecibido, transform.position);
            
            //print(currentStamina);
        }
        else
        {
            if (gameObject.activeInHierarchy)
            {
                gameObject.SetActive(false);
                GameManager.GameManagerInstance._UiManager.MostrarPuntosNegativos(dañoRecibido, transform.position);
                GameManager.GameManagerInstance._ExplotionManager.ObtenerExplosion(transform, ReEscalaExplosion, TiempoVidaExplosion);
                //cuento la destruccion como puntaje
                //UIManager.uiManagerInstance.scoreManager.currentPlayerScore += valueDamage;
            }
            escudoActual = 0;
        }

        GameManager.GameManagerInstance._UiManager.IngresarPuntaje(dañoRecibido);

    }
}
