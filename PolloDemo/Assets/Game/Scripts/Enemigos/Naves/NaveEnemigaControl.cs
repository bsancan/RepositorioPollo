using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class NaveEnemigaControl : MonoBehaviour
{

    public Transform Pivot;
    public Transform Modelo;
    public Transform PosLaserCentral;

    public int TipoNave;
    public Vector3 ReEscalaExplosion = Vector3.one;
    public float TiempoVidaExplosion = 1.2f;
    public int ValorDaño = 20;
    public int ValorEscudo = 20;
    public float VelocidadDeFrente = 0f;
    public float VelocidadRotation = 10f;
    public float TiempoActPosObjetivo;

    [Header("Lasers")]
    public int NumMaxLasers = 3;
    public float TiempoDisSigLaser = 1f;

    public bool moverse;

    private int escudoActual;
    private Transform objetivo;
    Vector3 direction;
    Quaternion toRotation;

    private void OnEnable()
    {
        escudoActual = ValorEscudo;
    }

    void Start()
    {
       
    }

    void Update()
    {
        if (moverse)
        {
            transform.position = transform.position + (transform.forward * VelocidadDeFrente * Time.deltaTime);
            Pivot.LookAt(objetivo);
            //direction = CharacterManager.CharacterManagerInstance._Character.transform.position - transform.position;
            //toRotation = Quaternion.LookRotation(direction);
            //transform.rotation = Quaternion.Lerp(transform.rotation, toRotation , VelocidadRotation * Time.deltaTime);
        }
        else
        {
            transform.position = transform.position + (transform.forward * VelocidadDeFrente * Time.deltaTime);
        }
    }

    public void IniciarMovimiento()
    {
        if (TipoNave == 1)
        {

        }
        else if (TipoNave == 2)
        {

            StartCoroutine(IenTiempoActualizarPosObjecitvo());
        }
        else if (TipoNave == 3)
        {
          
        }
    }

    public void DañoRecibido(int dañoRecibido)
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
                //gameObject.SetActive(false);
                GameManager.GameManagerInstance._UiManager.MostrarPuntosNegativos(dañoRecibido, transform.position);
                GameManager.GameManagerInstance._ExplotionManager.ObtenerExplosion(transform, ReEscalaExplosion, TiempoVidaExplosion);
                Destroy(gameObject);

            }
            escudoActual = 0;
        }

        GameManager.GameManagerInstance._UiManager.IngresarPuntaje(dañoRecibido);

    }

    IEnumerator IenTiempoActualizarPosObjecitvo() {
        objetivo = CharacterManager.CharacterManagerInstance._Character.transform;
        moverse = true;
        StartCoroutine(IenDispararLasers());
        yield return new WaitForSeconds(TiempoActPosObjetivo);
        moverse = false;

    }

    IEnumerator IenDispararLasers()
    {
        yield return new WaitForSeconds(1f);
        int claser = 0;
        while(claser < NumMaxLasers)
        {
            PosLaserCentral.LookAt(objetivo);
            GameManager.GameManagerInstance._AmmoManager.ObtenerEnemyAmmo(PosLaserCentral);
            yield return new WaitForSeconds(TiempoDisSigLaser);
            claser++;
        }
    }

    //IEnumerator IenActualizarPosObjetivo()
    //{
    //    while (true)
    //    {
    //        objetivo = CharacterManager.CharacterManagerInstance._Character.transform.position;
    //        yield return new WaitForSeconds(TiempoActPosObjetivo);
    //    }
    //}
}
