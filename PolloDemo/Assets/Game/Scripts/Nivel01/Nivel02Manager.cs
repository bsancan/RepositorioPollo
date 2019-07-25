using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Nivel02Manager : MonoBehaviour
{
    public static Nivel02Manager Nivel02ManagerInstance;
    public Transform TunelRecorrido;
    //public Transform EnemyManagerRecorrido;
    public NaveEnemigaManager NaveEnemigaManager;
    
    public Material MatWorldBender;
    public Material MatEnemy;
    public Material MatEnemyLaser;
    public Material MatPlayerLaser;
    public Material MatItems;
    

    [Range(-0.01f, 0.01f)]
    public float CurvaturaMundoY;
    [Range(-0.01f, 0.01f)]
    public float CurvaturaMundoX;
    [Range(-1f, 1f)]
    public float DirCurvaturaMundoY;
    [Range(-1f, 1f)]
    public float DirCurvaturaMundoX;
    [Range(0f, 2f)]
    public float PoderCurvaturaMundo;

    private const string _CurvaturaX = "_CurvaturaX";
    private const string _DirCurvaturaX = "_DirCurvaturaX";
    private const string _CurvaturaY = "_CurvaturaY";
    private const string _DirCurvaturaY = "_DirCurvaturaY";

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
        StartCoroutine(IenIniciaCurvaturaTunel());
        //NaveEnemigaManager.EstablecerValoresIniciales();
        NaveEnemigaManager.GenerarOlasNavesEnemigas02();
       //IniciarAnimacionDeRecorrido();
    }

    // Update is called once per frame
    void Update()
    {
        //MatWorldBender.SetFloat(_DirCurvaturaX, DirCurvaturaMundoX);
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

    IEnumerator IenIniciaCurvaturaTunel()
    {
        MatWorldBender.SetFloat(_DirCurvaturaX, 0f);
        MatWorldBender.SetFloat(_DirCurvaturaY, 0f);
        MatEnemy.SetFloat(_DirCurvaturaX, 0f);
        MatEnemy.SetFloat(_DirCurvaturaY, 0f);
        yield return new WaitForSeconds(1f);
   
        Vector2 dirF = Vector2.zero;
        Vector2 dirI = Vector2.zero;
        while (true)
        {
 
            dirF = new Vector2(Random.Range(-0.3f, 0.3f), Random.Range(-0.3f, 0.3f));

            float tiempo = 0f;
            while (tiempo < 3f)
            {
                MatWorldBender.SetFloat(_DirCurvaturaX, dirI.x);
                MatWorldBender.SetFloat(_DirCurvaturaY, dirI.y);
                MatEnemy.SetFloat(_DirCurvaturaX, dirI.x);
                MatEnemy.SetFloat(_DirCurvaturaY, dirI.y);

                dirI = Vector2.Lerp(dirI, dirF, 2f* Time.deltaTime);
                
                yield return new WaitForSeconds(0.01f);
                tiempo += Time.deltaTime;
                //print(tiempo);
            }

            dirI = dirF;
        }
    }

    //public Quaternion ObtenerNuevaRotacionDelEnemyManagerRecorrido()
    //{
    //    return Quaternion.Euler(EnemyManagerRecorrido.rotation.eulerAngles - rotacionIniciarlTunel.eulerAngles);

    //}

}
