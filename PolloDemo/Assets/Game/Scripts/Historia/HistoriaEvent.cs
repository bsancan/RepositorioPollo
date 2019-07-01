using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HistoriaEvent : MonoBehaviour
{
    public MensajesHistoriaEvent msj;

    public void IniciarAnimacionMensajes()
    {
        msj.IniciarAnimacionMensajes();
    }

    public void IrAlNivel()
    {

        GameManager.GameManagerInstance.IrAlNivel(GameManager.GameManagerInstance.e_Nivel01);
    }
}
