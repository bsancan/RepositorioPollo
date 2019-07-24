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
    public GameObject Portal;
    public Material MaterialModeloNormal;
    public Material MaterialModeloParpadeo;
    public List<Renderer> listarender;

    [Header("Estados player")]
    public bool Invencible;
    public bool Golpeado;
    public bool Muerto;
    public bool EnMovimiento;

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
    public int EscudoInicial = 100;
    public int EscudoActual = 0;
    public float IntervaloParaConsumirEscudo = 1f;
    public int EnergiaInicial = 100;
    public int EnergiaActual = 0;
    public float IntervaloParaConsumirEnergia = 1f;
    public int GastoEnergiaPorTiempo = 1;





    private IEnumerator ienEnergia;

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
        ResetCharacter();
    }

    // Update is called once per frame
    void Update()
    {
       
    }

    public void ResetCharacter()
    {
        transform.position = Vector3.zero;
        EscudoActual = EscudoInicial;
        EnergiaActual = EnergiaInicial;
        if (ienEnergia != null)
            StopCoroutine(ienEnergia);
        else
            ienEnergia = IenConsumoEnergia();
        
    }


    public void DañoRecibido(int daño)
    {
       
        GameManager.GameManagerInstance._ExplotionManager.ObtenerExplosionPlayer(LugarExplosion);
        if (Invencible)
            return;

        if (!Golpeado)
        {
            if (EscudoActual - daño >= 0)
            {
                //AnimacionDeDaño();
                EscudoActual -= daño;
                GameManager.GameManagerInstance._UiManager.TxtShieldPlayer.text = ((int)EscudoActual * 100 / EscudoInicial).ToString();
            }
            else
            {
                AnimacionDeMuerte();

            }
        }

    }

    public void EnergiaRecibida(int energia)
    {
        StopCoroutine(ienEnergia);
        if((EnergiaActual + energia) > EnergiaInicial)
        {
            EnergiaActual = EnergiaInicial;
        }
        else
        {
            EnergiaActual += energia;
        }

        GameManager.GameManagerInstance._UiManager.TxtEnergyPlayer.text =
            (EnergiaActual * 100 / EnergiaInicial).ToString();

        StartCoroutine(ienEnergia);
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

    //private void OnTriggerEnter(Collider other)
    //{
    //    if (other.gameObject.CompareTag("Asteroid"))
    //    {
    //        //    DañoRecibido(other.GetComponent<Asteroid>().ValorDaño);
    //        //    GameManager.GameManagerInstance._ExplotionManager.ObtenerExplosionPlayer(LugarExplosion);
    //    }
    //}

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

    public void IniciarConsumoEnergia()
    {
        StartCoroutine(ienEnergia);
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

    IEnumerator IenConsumoEnergia()
    {
        while (EnergiaActual > 0)
        {
            yield return new WaitForSeconds(IntervaloParaConsumirEnergia);
            EnergiaActual -= GastoEnergiaPorTiempo;

            GameManager.GameManagerInstance._UiManager.TxtEnergyPlayer.text =
                (EnergiaActual * 100 / EnergiaInicial).ToString();
        }

        EnergiaActual = 0;
        GameManager.GameManagerInstance._UiManager.TxtEnergyPlayer.text = "0";

        //probar estilo para detenar

    }
}
