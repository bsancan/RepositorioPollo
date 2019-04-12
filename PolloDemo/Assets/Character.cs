using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    public string Name;
    public GameObject Model;

    [Tooltip("Rango minimo del movimiento del player en X Y")]
    [SerializeField]
    private Vector2 MinRange;                              //Rango minimo en XY del player sobre el mundo/local
    [Tooltip("Rango maximo del movimiento del player en X Y")]
    [SerializeField]
    private Vector2 MaxRange;                              //Rango máximo en XY del player sobre el mundo/local
    [Tooltip("Velodidad de movimiento del player")]
    [SerializeField]
    private float XYSpeed = 20f;                           // Velocidad de movimiento en XY del player
    [Tooltip("Velodidad de rotacion del player")]
    [SerializeField]
    private float RotationSpeed = 10f;                     //Velocidad de rotación del player


    //======Animaciones del player
    static int s_DeadHash = Animator.StringToHash("Dead");
    static int s_ShotHash = Animator.StringToHash("Shot");
    static int s_MovingHash = Animator.StringToHash("MoveX");
    static int s_HitHash = Animator.StringToHash("Hit");

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
