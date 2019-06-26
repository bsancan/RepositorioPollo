using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplotionParticle : MonoBehaviour
{
    public float TiempoVida = 1f;
    public Vector3 TamañoInicial = Vector3.one;

    private void Awake()
    {
        
    }

    private void OnEnable()
    {
        //print("activacion - " + gameObject.name + " - " + TamañoInicial.x.ToString());
        transform.localScale = TamañoInicial;
        CancelInvoke("DesactivarExplosionParticle");
        Invoke("DesactivarExplosionParticle", TiempoVida);
    }

    void DesactivarExplosionParticle()
    {
        gameObject.SetActive(false);
    }

    public void ParametrosIniciales(Vector3 escala, float vida)
    {
        TiempoVida = vida;
        TamañoInicial = escala;
    }
}
