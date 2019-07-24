using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalTrigger : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("PlayerShield"))
        {
            Nivel01Manager.Nivel01ManagerInstance.Portal.gameObject.SetActive(true);
        }
    }

}
