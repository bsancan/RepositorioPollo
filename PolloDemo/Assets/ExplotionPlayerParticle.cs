using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplotionPlayerParticle : MonoBehaviour
{
    public float TiempoVida = 1f;
    public Vector3 TamañoInicial = Vector3.one;
    private void OnEnable()
    {
        transform.localScale = TamañoInicial;
        CancelInvoke("DesactivarExplosionPlayerParticle");
        Invoke("DesactivarExplosionPlayerParticle", TiempoVida);
    }

    void DesactivarExplosionPlayerParticle()
    {
        transform.parent = GameManager.GameManagerInstance._ExplotionManager.GetComponent<Transform>();
        
        gameObject.SetActive(false);
    }

    public void ParametrosIniciales(Vector3 escala, float vida)
    {
        TiempoVida = vida;
        TamañoInicial = escala;
    }
}
