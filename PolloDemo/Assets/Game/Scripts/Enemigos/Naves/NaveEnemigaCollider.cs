using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NaveEnemigaCollider : MonoBehaviour
{
    [SerializeField]
    private NaveEnemigaControl naveEnemigaControl;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("PlayerAmmo"))
        {
            naveEnemigaControl.DañoRecibido(other.gameObject.GetComponent<PlayerAmmo>().ValorDaño);
            other.gameObject.SetActive(false);
        }
        else if (other.gameObject.CompareTag("Player"))
        {
            Character chars = CharacterManager.CharacterManagerInstance._Character;
            naveEnemigaControl.DañoRecibido((int)(chars.EscudoActual / 2));
            chars.DañoRecibido((int)(chars.EscudoActual / 2));



        }
    }

    //private void OnCollisionEnter(Collision other)
    //{
    //    if (other.gameObject.CompareTag("PlayerAmmo"))
    //    {
    //        print(other.gameObject.name);
    //        naveEnemigaControl.DañoRecibido(other.gameObject.GetComponent<PlayerAmmo>().ValorDaño);
    //        other.gameObject.SetActive(false);

    //    }
    //    else if (other.gameObject.CompareTag("Player"))
    //    {
    //        Character chars = CharacterManager.CharacterManagerInstance._Character;
    //        naveEnemigaControl.DañoRecibido((int)(chars.EscudoActual / 2));
    //        chars.DañoRecibido((int)(chars.EscudoActual / 2));
    //    }
    //}
}
