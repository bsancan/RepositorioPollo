using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FadeEvents : MonoBehaviour
{

    public void IrAlSiguienteNivel()
    {
        // Return the current Active Scene in order to get the current Scene name.
        //Scene scene = SceneManager.GetActiveScene();
        if (!GameManager.GameManagerInstance.CargarEscena)
        {
            GameManager.GameManagerInstance.CargarEscenaAsync();
        }
        else
        {

        }

    }

   

}
