using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EscudoEnergia : MonoBehaviour
{
    
    //private IEnumerator ienMostrarEscudo;

    //public bool EscudoVisible;
    private void Awake()
    {
        //render = GetComponent<Renderer>();    
    }
    void Start()
    {
        //ienMostrarEscudo = CorMuestraEscudo();
        //render.enabled = false;
    }

    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Asteroid"))
        {
            CharacterManager.CharacterManagerInstance._Character.DañoRecibido(other.GetComponent<AsteroidCollider>().asteroid.ValorDaño);
            GetComponent<EscudoEnergiaEventos>().IniciaAnimacion();
            //IniciaEscudo(other.gameObject);
        }
    }

    //void IniciaEscudo(GameObject other)
    //{
    //    if (!EscudoVisible)
    //    {
    //        ienMostrarEscudo = CorMuestraEscudo();
    //        StartCoroutine(ienMostrarEscudo);
    //    }
    //}

    //IEnumerator CorMuestraEscudo()
    //{
    //    EscudoVisible = true;
    //    render.enabled = true;
    //    yield return new WaitForSeconds(1f);
    //    EscudoVisible = false;
    //    render.enabled = false;
    //}
}
