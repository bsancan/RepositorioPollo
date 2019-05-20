using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager GameManagerInstance;
    public AmmoManager _AmmoManager;
    public UiManager _UiManager;
    public ExplotionManager _ExplotionManager;

    public int NivelActual;

    private void Awake()
    {
        if (GameManagerInstance == null)
        {
            GameManagerInstance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        IniciarNivel();
    }

    void Update()
    {
        
    }

    public void IniciarNivel()
    {
        if(NivelActual == 1)
        {
            _UiManager.IngresarPuntaje(0);
            CharacterManager.CharacterManagerInstance._Character.IniciarConsumoEnergia();
        }
    }
}
