using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

[RequireComponent(typeof(DualJoystickTouchContoller))]
public class UiManager : MonoBehaviour
{
    public enum TipoPregunta
    {
        ninguna = 0,
        continuarJuego = 1,
        reiniciarJuego = 2,
        salirJuego = 3
    }

    [Header("Menu")]
    [SerializeField]
    private GameObject btnMenu;
    [SerializeField]
    private GameObject pnlMenu;
    [SerializeField]
    private GameObject pnlPregunta;
    [SerializeField]
    private Text txtPregunta;
    //[SerializeField]
    //private Text txtTiempo;

    private TipoPregunta preguntalActual;

    [Header("CrossHair")]
 
    public RectTransform RectCrossHairParent;
    public RectTransform RectCrossHairA;
    public RectTransform RectCrossHairB;
    public DualJoystickTouchContoller TouchController;

    [SerializeField]
    private Color ColorCrossHair01;
    [SerializeField]
    private Color ColorCrossHair02;
    [SerializeField]
    private Color ColorCrossHair03;

    [Header("Points")]
    [SerializeField]
    private RectTransform rectCanvasPuntos;
    [SerializeField]
    private Text puntaje;
    [SerializeField]
    private GameObject puntos;
    [SerializeField]
    private Color ColorNegativePoints;
    [SerializeField]
    private Color ColorPositivePoints;


    [Header("Energy & Shield Player")]
    public GameObject GoBars;
    public Text TxtEnergyPlayer;
    public Text TxtShieldPlayer;

    private Image imgCrossHair;
    private int puntajeAcumulado = 0;
    
    private float timer;
    void Start()
    {
        DefaultConfiguration();
    }

    // Update is called once per frame
    void Update()
    {
        //timer += Time.deltaTime;
        //txtTiempo.text = timer.ToString("F");
    }

    public void ResetUiManager()
    {
        TxtEnergyPlayer.text = "100";
        TxtShieldPlayer.text = "100";
        preguntalActual = TipoPregunta.ninguna;
        pnlPregunta.SetActive(false);
        pnlMenu.SetActive(false);
        btnMenu.SetActive(true);
    }

    private void DefaultConfiguration()
    {
        if (TouchController == null)
            TouchController = GetComponent<DualJoystickTouchContoller>();
        if (!TouchController.leftJoystickBackgroundImage.gameObject.activeInHierarchy)
            TouchController.leftJoystickBackgroundImage.gameObject.SetActive(true);
        if (!TouchController.rightJoystickBackgroundImage.gameObject.activeInHierarchy)
            TouchController.rightJoystickBackgroundImage.gameObject.SetActive(true);
        if (!GoBars.activeInHierarchy)
            GoBars.SetActive(true);
        imgCrossHair = RectCrossHairB.GetComponent<Image>();

        preguntalActual = TipoPregunta.ninguna;
        Time.timeScale = 1;

      //RectCrossHairB.anchorMin = new Vector2(1, 0);
      //RectCrossHairB.anchorMax = new Vector2(1, 0);
    }

    public void MostrarPuntosNegativos(int values, Vector3 pos)
    {
        GameObject go = Instantiate(puntos, Vector3.zero, Quaternion.identity);
        go.transform.SetParent(rectCanvasPuntos.transform);
        Vector2 viewPortPosA = CharacterManager.CharacterManagerInstance.PlayerCamera.WorldToViewportPoint(pos);

        Vector2 screenPos = new Vector2(
            ((viewPortPosA.x * rectCanvasPuntos.sizeDelta.x) - (rectCanvasPuntos.sizeDelta.x * 0.5f)),
            ((viewPortPosA.y * rectCanvasPuntos.sizeDelta.y) - (rectCanvasPuntos.sizeDelta.y * 0.5f)));

        go.GetComponent<RectTransform>().anchoredPosition = screenPos;
        go.transform.localScale = Vector3.one;
        go.SetActive(true);

        //asigno los valores al texto
        go.GetComponent<Points>().StartAnimation("-" + values, ColorNegativePoints);

    }

    public void MostrarPuntosPositivos(int values, Vector3 pos)
    {
        GameObject go = Instantiate(puntos, Vector3.zero, Quaternion.identity);
        go.transform.SetParent(rectCanvasPuntos.transform);
        Vector2 viewPortPosA = CharacterManager.CharacterManagerInstance.PlayerCamera.WorldToViewportPoint(pos);

        Vector2 screenPos = new Vector2(
            ((viewPortPosA.x * rectCanvasPuntos.sizeDelta.x) - (rectCanvasPuntos.sizeDelta.x * 0.5f)),
            ((viewPortPosA.y * rectCanvasPuntos.sizeDelta.y) - (rectCanvasPuntos.sizeDelta.y * 0.5f)));

        go.GetComponent<RectTransform>().anchoredPosition = screenPos;
        go.transform.localScale = Vector3.one;
        go.SetActive(true);

        //asigno los valores al texto
        go.GetComponent<Points>().StartAnimation("+" + values, ColorPositivePoints);
    }

    public void SetCrossHairColor1()
    {
        imgCrossHair.color = ColorCrossHair01;

    }

    public void SetCrossHairColor2()
    {
        imgCrossHair.color = ColorCrossHair02;

    }

    public void SetCrossHairColor3()
    {
        imgCrossHair.color = ColorCrossHair03;

    }

    public void IngresarPuntaje(int p)
    {
        puntajeAcumulado += p;
        puntaje.text = puntajeAcumulado.ToString();
    }

    #region Menu para niveles
    public void MostrarMenu()
    {
        Time.timeScale = 0;
        pnlMenu.SetActive(true);
        btnMenu.SetActive(false);
    }

    public void RegresarMenuPrincipal()
    {
        GameManager.GameManagerInstance.RegresarMenuPrincipal();
    }

    public void Continuar()
    {
        Time.timeScale = 1;
        pnlMenu.SetActive(false);
        btnMenu.SetActive(true);
    }

    public void ReiniciarNivel()
    {
        preguntalActual = TipoPregunta.reiniciarJuego;
        txtPregunta.text = "¿Deseas reiniciar el nivel?";
        pnlMenu.SetActive(false);
        pnlPregunta.SetActive(true);

    }

    public void Salir()
    {
        preguntalActual = TipoPregunta.salirJuego;
        txtPregunta.text = "¿Deseas salir del juego?";
        pnlMenu.SetActive(false);
        pnlPregunta.SetActive(true);
    }

    public void RespuestaSI()
    {
        if (preguntalActual == TipoPregunta.reiniciarJuego)
        {
            //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            GameManager.GameManagerInstance.ReinicialNivel();
        }
        else if (preguntalActual == TipoPregunta.salirJuego)
        {
            Application.Quit();
        }
    }

    public void RespuestaNO()
    {
        preguntalActual = TipoPregunta.ninguna;
        pnlPregunta.SetActive(false);
        pnlMenu.SetActive(true);
    }
    #endregion


}
