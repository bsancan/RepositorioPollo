using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventosAnimacionPlayer : MonoBehaviour
{
    public void AnimacionDañoIniciada()
    {
        CharacterManager.CharacterManagerInstance._Character.IniciaParpadeoMaterialDaño();
    }
    public void AnimacionDañoTerminada()
    {
        CharacterManager.CharacterManagerInstance._Character.FinalizaParpadeoMaterialDaño();
    }
}
