using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAmmo : MonoBehaviour {

    private float velocidad = 2f;
    private float tiempoVida = 2f;
    private int valorDaño = 10;

    public int ValorDaño
    {
        get { return valorDaño; }
        set { valorDaño = value; }
    }
    //private bool IsActive = false;

    public void SetPlayerAmmo(float sd, float lt, int vd)
    {
        velocidad = sd;
        tiempoVida = lt;
        valorDaño = vd;
    }

	void Start () {

	}

    private void OnEnable()
    {
       
        StartCoroutine(RutinaTimepoVida());
    }

    // Update is called once per frame
    void Update () {
        transform.position += transform.forward * velocidad * Time.deltaTime;
    }

    IEnumerator RutinaTimepoVida()
    {
        yield return new WaitForSeconds(tiempoVida);
        gameObject.SetActive(false);

    }

    private void OnTriggerEnter(Collider other)
    {
        //if (other.gameObject.CompareTag("Enemy"))
        //{
        //    gameObject.SetActive(false);

        //}
        //else if (other.gameObject.CompareTag("EnemySpaceShip"))
        //{
        //    gameObject.SetActive(false);
        //}


    }
}
