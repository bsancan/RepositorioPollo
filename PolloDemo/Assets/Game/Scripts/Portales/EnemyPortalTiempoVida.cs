using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPortalTiempoVida : MonoBehaviour
{
    //public float TiempoVida;
    void Start()
    {
        
    }

    
    void Update()
    {
        
    }

    public void IniciarTiempoVida(float tv)
    {
        StartCoroutine(IenTiempoVida(tv));
    }

    IEnumerator IenTiempoVida(float tv) {

        yield return new WaitForSeconds(tv);
        //gameObject.SetActive(false);
        Destroy(gameObject);
    }
}
