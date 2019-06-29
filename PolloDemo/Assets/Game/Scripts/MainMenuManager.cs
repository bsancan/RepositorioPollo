using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuManager : MonoBehaviour
{
    public static MainMenuManager MainMenuManagerInstance;

    private void Awake()
    {
        if (MainMenuManagerInstance == null)
        {
            MainMenuManagerInstance = this;
        }
        else
        {
            Destroy(gameObject);
        }


    }

    void Start()
    {
       

    }


    void Update()
    {
        
    }

    public void IrAlNivel(string nvl)
    {
        GameManager.GameManagerInstance.IrAlNivel(nvl);
    }
}
