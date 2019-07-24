using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
public class AsteroidTrigger : MonoBehaviour
{

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("PlayerShield"))
        {
            Nivel01Manager.Nivel01ManagerInstance.InstanciarPatronAsteroide();
        }
    }
}
