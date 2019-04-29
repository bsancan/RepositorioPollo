using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(DualJoystickTouchContoller))]
public class UiManager : MonoBehaviour
{
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

    // Start is called before the first frame update
    void Start()
    {
        DefaultConfiguration();
    }

    // Update is called once per frame
    void Update()
    {
        
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
        imgCrossHair = RectCrossHairA.GetComponent<Image>();


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
        go.GetComponent<Points>().StartAnimation("-" + values, ColorPositivePoints);
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
}
