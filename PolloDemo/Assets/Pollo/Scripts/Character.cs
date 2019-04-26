using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    public string Name;
    public GameObject Model;
    public Transform PosicionLaserCentral;
    public Transform LugarExplosion;

    [Tooltip("Rango minimo del movimiento del player en X Y")]
    public Vector2 MinRange;                              //Rango minimo en XY del player sobre el mundo/local
    [Tooltip("Rango maximo del movimiento del player en X Y")]
    public Vector2 MaxRange;                              //Rango máximo en XY del player sobre el mundo/local
    [Tooltip("Velodidad de movimiento del player")]
    public float VelocidadXY = 20f;                           // Velocidad de movimiento en XY del player
    public float VelocidadZ = 2f;
    [Tooltip("Velodidad de rotacion del player")]
    public float VelocidadRotacion = 10f;                     //Velocidad de rotación del player
    [Tooltip("Velodidad de movimiento del crossHair")]
    public float VelocidadCrossHair = 3f;               //Velocidad del CrossHair para desplazarse sobre la pantalla
    [Tooltip("Distancia del Raycast")]
    public float DistanciaRaycast = 100f;
    [Tooltip("Tiempo de espera entre disparos")]
    public float VelocidadFuego = 0.2f;
    public int EscudoInicial = 100;
    public float IntervaloParaConsumirEscudo = 1f;
    public int EnergiaInicial = 100;
    public float IntervaloParaConsumirEnergia = 1f;
    public int GastoEnergiaPorTiempo = 1;

    [SerializeField]
    private int energiaActual;
    [SerializeField]
    private int escudoActual;

    private Animator PlayerAnimator;
    //======Animaciones del player
    private int s_DeadHash = Animator.StringToHash("Dead");
    private int s_ShotHash = Animator.StringToHash("Shot");
    private int s_MovingHash = Animator.StringToHash("MoveX");
    private int s_HitHash = Animator.StringToHash("Hit");

    //======Rotacion


    private void Awake()
    {
        PlayerAnimator = Model.GetComponent<Animator>();
    }
    void Start()
    {
        escudoActual = EscudoInicial;
        energiaActual = EnergiaInicial;
    }

    // Update is called once per frame
    void Update()
    {
       
    }


    void DañoRecibido(int daño)
    {
        if(escudoActual - daño >= 0)
        {
            escudoActual -= daño;
            GameManager.GameManagerInstance._UiManager.TxtShieldPlayer.text = ((int)escudoActual * 100 / EscudoInicial).ToString();
        }
        else
        {

        }
    }

    public void AnimacionEnX(float value)
    {
        PlayerAnimator.SetFloat(s_MovingHash, value);
    }

    public void AnimacionDeDisparo(bool b)
    {
        PlayerAnimator.SetBool(s_ShotHash, b);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Asteroid"))
        {
            DañoRecibido(other.GetComponent<Asteroid>().ValorDaño);
            GameManager.GameManagerInstance._ExplotionManager.ObtenerExplosionPlayer(LugarExplosion);
        }
    }
}
