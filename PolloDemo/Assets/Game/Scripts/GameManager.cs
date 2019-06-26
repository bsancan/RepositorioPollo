using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager GameManagerInstance;
    public Animator AniFade;
    public AmmoManager _AmmoManager;
    public UiManager _UiManager;
    public ExplotionManager _ExplotionManager;

    
    public int NivelActual;
    private string siguienteNivel;

    //======Animaciones del player
    private int s_Inicio = Animator.StringToHash("Inicio");
    private int s_Estado = Animator.StringToHash("CambioEstado");

    //==========nombre de escenas
    public string e_Carga = "GameManager";
    public string e_Menu = "MainMenuScene";
    public string e_Historia = "Historia";
    public string e_PlayerManager = "Player";
    public string e_Nivel01 = "Nivel01";
    public string e_Nivel02 = "Nivel02";

    [Header("Modo Pruebas")]
    public bool CargarEscena;
    public string CodigoEscena;
   
    private void Awake()
    {
        if (GameManagerInstance == null)
        {
            GameManagerInstance = this;
            GameManagerInstance.NivelActual = 0;
            DontDestroyOnLoad(this);
            _UiManager.gameObject.SetActive(false);
        }
        else
        {
            Destroy(gameObject);
        }


    }

    // Start is called before the first frame update
    void Start()
    {
        Application.targetFrameRate = 30;

        if (!CargarEscena)
        {
            StartCoroutine(EsperaCargaDeEscenaAsyncInicio());
        }
        else
        {
            if (CodigoEscena == "H")
                siguienteNivel = e_Historia;
            else if (CodigoEscena == "1")
                siguienteNivel = e_Nivel01;
            else if (CodigoEscena == "2")
                siguienteNivel = e_Nivel02;
            //StartCoroutine(EsperaCargaDeEscenaAsync());
            CargarNivelManualmente();
        }
 
    }

    void Update()
    {
        
    }

    void IniciarNivel()
    {
        // Return the current Active Scene in order to get the current Scene name.
        Scene scene = SceneManager.GetActiveScene();

        if(scene.name == e_Historia)
        {
            HistoriaManager hm = GameObject.FindObjectOfType<HistoriaManager>();
            if(hm != null)
            {
                AniFade.SetBool(s_Estado, false);
                hm.AniHistoria.SetTrigger("Inicio");
            }

        }
        else if(scene.name == e_PlayerManager)
        {
            siguienteNivel = e_Nivel01;
            CargarEscenaAsync();
        }
        else if (scene.name == e_Nivel01)
        {
            NivelActual = 1;
            _UiManager.gameObject.SetActive(true);
            _UiManager.IngresarPuntaje(0);
            CharacterManager.CharacterManagerInstance._Character.IniciarConsumoEnergia();
            AniFade.SetBool(s_Estado, false);
        }

        
    }

    public void CargarEscenaAsync()
    {
        StartCoroutine(EsperaCargaDeEscenaAsync());
    }


    #region Botones del menu principal
    public void IrAlNivel(string nom)
    {
        AniFade.SetBool(s_Estado, true);

        if (nom == "H")
        {
            siguienteNivel = e_Historia;
        }
        else if(nom == "1")
        {
            siguienteNivel = e_PlayerManager;
        }
        else if(nom == "2")
        {
            siguienteNivel = e_Nivel02;
        }
        print(siguienteNivel);
    }
    #endregion

    IEnumerator EsperaCargaDeEscenaAsync()
    {
        // The Application loads the Scene in the background as the current Scene runs.
        // This is particularly good for creating loading screens.
        // You could also load the Scene by using sceneBuildIndex. In this case Scene2 has
        // a sceneBuildIndex of 1 as shown in Build Settings.

        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(siguienteNivel);

        // Wait until the asynchronous scene fully loads
        while (!asyncLoad.isDone)
        {
            yield return null;
        }

        if (!CargarEscena)
        {
            IniciarNivel();
        }
        else
        {
            AniFade.SetTrigger(s_Inicio);
            yield return new WaitForSeconds(2f);
            AniFade.SetBool(s_Estado,true);
            yield return new WaitForSeconds(2f);
            CargarEscena = false;
            IniciarNivel();
        }



    }

    IEnumerator EsperaCargaDeEscenaAsyncInicio()
    {
        // The Application loads the Scene in the background as the current Scene runs.
        // This is particularly good for creating loading screens.
        // You could also load the Scene by using sceneBuildIndex. In this case Scene2 has
        // a sceneBuildIndex of 1 as shown in Build Settings.
        
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(e_Menu);

        // Wait until the asynchronous scene fully loads
        while (!asyncLoad.isDone)
        {
            yield return null;
        }
        
        AniFade.SetTrigger(s_Inicio);

        
    }

    #region Pruebas

    void CargarNivelManualmente()
    {
        AniFade.gameObject.SetActive(false);
        if (siguienteNivel == e_Nivel01)
        {
            NivelActual = 1;
            _UiManager.gameObject.SetActive(true);
            _UiManager.IngresarPuntaje(0);
            //CharacterManager.CharacterManagerInstance._Character.IniciarConsumoEnergia();
            //AniFade.SetBool(s_Estado, false);
        }
    }

    #endregion
}
