using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Nivel02Manager : MonoBehaviour
{
    public static Nivel02Manager Nivel02ManagerInstance;
    public Transform TunelRecorrido;
    //public Transform EnemyManagerRecorrido;
    public NaveEnemigaManager NaveEnemigaManager;


    private Animator tunelAnima;
    private int s_MoveHash = Animator.StringToHash("Move");
    //private Animator EnemyManagerAnima;
    //private int s_Move2Hash = Animator.StringToHash("Move2");
    private Quaternion rotacionIniciarlTunel;

    private void Awake()
    {

        if (Nivel02ManagerInstance == null)
        {
            Nivel02ManagerInstance = this;
        }
        else
        {
            Destroy(gameObject);
        }
        //EnemyManagerAnima = EnemyManagerRecorrido.GetComponent<Animator>();
        tunelAnima = TunelRecorrido.GetComponent<Animator>();
        rotacionIniciarlTunel = Quaternion.Euler(0, 90.05801f, 0);
    }

    // Start is called before the first frame update
    void Start()
    {
        GameManager.GameManagerInstance._AmmoManager.CrearEnemyAmmo();
        
        //NaveEnemigaManager.EstablecerValoresIniciales();
       // NaveEnemigaManager.GenerarOlasNavesEnemigas02();
       //IniciarAnimacionDeRecorrido();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void IniciarAnimacionDeRecorrido()
    {
        tunelAnima.SetBool(s_MoveHash, true);
        //EnemyManagerAnima.SetBool(s_Move2Hash, true);
    }

    public Quaternion ObtenerNuevaRotacionDelRecorrido()
    {
        return Quaternion.Euler(TunelRecorrido.rotation.eulerAngles - rotacionIniciarlTunel.eulerAngles);

    }

    //public Quaternion ObtenerNuevaRotacionDelEnemyManagerRecorrido()
    //{
    //    return Quaternion.Euler(EnemyManagerRecorrido.rotation.eulerAngles - rotacionIniciarlTunel.eulerAngles);

    //}

}
