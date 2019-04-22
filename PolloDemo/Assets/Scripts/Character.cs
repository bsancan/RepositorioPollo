using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    public string Name;
    public GameObject Model;
    public Transform PosicionLaserCentral;

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
        
    }

    // Update is called once per frame
    void Update()
    {
       
    }

    public void PlayerAnimationInX(float value)
    {
        PlayerAnimator.SetFloat(s_MovingHash, value);
    }

    public void PlayerAnimationShot(bool b)
    {
        PlayerAnimator.SetBool(s_ShotHash, b);
    }
}
