using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinalNivelTrigger : MonoBehaviour
{

    private void OnTriggerEnter(Collider other)
    {
        GameManager gm = GameManager.GameManagerInstance;
        if (other.CompareTag("PlayerShield"))
        {

            if(GameManager.GameManagerInstance.ActualNivel == gm.e_Nivel01)
            {
                GameManager.GameManagerInstance.IrAlNivel(gm.e_Nivel02);
            }
           
        }
    }
}
