using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MensajesHistoriaEvent : MonoBehaviour
{
    private Animator AniCnvHistoria;
    public Text TxtMsgBox;
    public float EsperaSgtLinea;
    public string[] Mensajes;

    public void Awake()
    {
        AniCnvHistoria = GetComponent<Animator>();
    }

    private void Start()
    {
        TxtMsgBox.text = "";
    }

    public void IniciarAnimacionMensajes()
    {
        AniCnvHistoria.SetTrigger("Inicio");
    }

    public void MostartMensajes()
    {
        StartCoroutine(MostarMensajesPorLinea());
    }

    IEnumerator MostarMensajesPorLinea()
    {
        for (int i = 0; i < Mensajes.Length; i++)
        {
            TxtMsgBox.text = Mensajes[i];
            yield return new WaitForSeconds(EsperaSgtLinea);
            TxtMsgBox.text = "";
            yield return new WaitForSeconds(0.5f);
        }
    }

    //public void IrAlSiguienteNivel()
    //{
    //    GameManager.GameManagerInstance.IrAlNivel("1");
    //}
}
