using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Character : MonoBehaviour
{

    public string Name;
    public GameObject Model;
    public Transform PosicionLaserCentral;
    public Transform LugarExplosion;
    public Material MaterialModeloNormal;
    public Material MaterialModeloParpadeo;
    public List<Renderer> listarender;

    [Header("Estados player")]
    public bool Invencible;
    public bool Golpeado;
    public bool Muerto;

    [Header("Movilidad player")]
    [Tooltip("Rango minimo del movimiento del player en X Y")]
    public Vector2 MinRange;                              //Rango minimo en XY del player sobre el mundo/local
    [Tooltip("Rango maximo del movimiento del player en X Y")]
    public Vector2 MaxRange;                              //Rango máximo en XY del player sobre el mundo/local
    [Tooltip("Velodidad de movimiento del player")]
    public float VelocidadXY = 20f;                           // Velocidad de movimiento en XY del player
    public float VelocidadZ = 2f;
    [Tooltip("Velodidad de rotacion del player")]
    public float VelocidadRotacion = 10f;                     //Velocidad de rotación del player
    [Header("CrossHair")]
    [Tooltip("Velodidad de movimiento del crossHair")]
    public float VelocidadCrossHair = 3f;               //Velocidad del CrossHair para desplazarse sobre la pantalla
    [Tooltip("Distancia del Raycast")]
    public float DistanciaRaycast = 100f;
    [Header("Energia Escudo")]
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


    //====Parpadeo
    private IEnumerator rutinaDaño;
    Color32 c;

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


    public void DañoRecibido(int daño)
    {
        GameManager.GameManagerInstance._ExplotionManager.ObtenerExplosionPlayer(LugarExplosion);
        if (!Golpeado)
        {
            if (escudoActual - daño >= 0)
            {
                //AnimacionDeDaño();
                escudoActual -= daño;
                GameManager.GameManagerInstance._UiManager.TxtShieldPlayer.text = ((int)escudoActual * 100 / EscudoInicial).ToString();
            }
            else
            {
                AnimacionDeMuerte();

            }
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

    public void AnimacionDeDaño()
    {
        PlayerAnimator.SetTrigger(s_HitHash);
    }

    public void AnimacionDeMuerte()
    {
        PlayerAnimator.SetTrigger(s_DeadHash);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Asteroid"))
        {
            //    DañoRecibido(other.GetComponent<Asteroid>().ValorDaño);
            //    GameManager.GameManagerInstance._ExplotionManager.ObtenerExplosionPlayer(LugarExplosion);
        }
    }

    public void IniciaParpadeoMaterialDaño()
    {
        if (!Golpeado)
        {
            print("inicio parpadeo");
            rutinaDaño = CorParpadeoMaterialDaño();
            StartCoroutine(rutinaDaño);
        }
    }

    public void FinalizaParpadeoMaterialDaño()
    {
        StopCoroutine(rutinaDaño);
        listarender.ToList().ForEach(c => c.material = MaterialModeloNormal);
        Golpeado = false;
        print("fin parpadeo");
            
    }

    IEnumerator CorParpadeoMaterialDaño()
    {
        Golpeado = true;
        while (Golpeado)
        {
            listarender.ToList().ForEach(c => c.material = MaterialModeloParpadeo);

            yield return new WaitForSeconds(0.1f);

            listarender.ToList().ForEach(c => c.material = MaterialModeloNormal);

            yield return new WaitForSeconds(0.1f);
        }
       
    }
}
