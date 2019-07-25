using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NaveEnemigaManager : MonoBehaviour
{
    public GameObject EnemyShip01;
    public GameObject EnemyShip02;
    public GameObject EnemyShip03;

    public GameObject Portal;

    [Header("Naves 02")]
    [SerializeField]
    private float ne02TiempoSigNav;
    [SerializeField]
    private float ne02TiempoSigOlaNav;
    [SerializeField]
    private int ne02NumNavesEnemiga;
    [SerializeField]
    private int ne02ValorDaño = 20;
    [SerializeField]
    private int ne02ValorEscudo = 20;
    [SerializeField]
    private float ne02VelocidadDeFrente = 0f;
    [SerializeField]
    private float ne02VelocidadRotation = 10f;
    [SerializeField]
    private float ne02TiempoActPosObjetivo;
    [SerializeField]
    private int ne02NumMaxLasers = 3;
    [SerializeField]
    private float ne02TiempoDisSigLaser = 1f;

    private Queue<Transform> enemyShip02Queue = new Queue<Transform>();
    public List<Vector3> posNavesEnemigasPatron02;

    private Vector3 posInicial;


    void Start()
    {
        posInicial = transform.position;
    }

    void Update()
    {
        //transform.position = Nivel02Manager.Nivel02ManagerInstance.TunelRecorrido.position + ( posInicial;
        //transform.rotation = Nivel02Manager.Nivel02ManagerInstance.ObtenerNuevaRotacionDelRecorrido();
    }

    public void EstablecerValoresIniciales()
    {
        //posNavesEnemigasPatron02 = new List<Vector3>();
        //posNavesEnemigasPatron02.Add(new Vector3(4f, 3f, 0f));
        //posNavesEnemigasPatron02.Add(new Vector3(-4f, 3f, 0f));
        //posNavesEnemigasPatron02.Add(new Vector3(6f, 0f, 0f));
        //posNavesEnemigasPatron02.Add(new Vector3(-6f, 0f, 0f));
        //posNavesEnemigasPatron02.Add(new Vector3(4f, -3f, 0f));
        //posNavesEnemigasPatron02.Add(new Vector3(-4f, -3f, 0f));
    }

    public void GenerarOlasNavesEnemigas02()
    {
        StartCoroutine(IenGenerarOlasNavesEnemigas02());
    }

    private void CrearNavesEnemigas02()
    {

        for (int i = 0; i < ne02NumNavesEnemiga; i++)
        {
            GameObject go = Instantiate(EnemyShip02, Vector3.zero, Quaternion.identity) as GameObject;
            Transform objTrans = go.transform;
            objTrans.parent = transform;
            objTrans.gameObject.name = objTrans.gameObject.name + "_" + i;
            enemyShip02Queue.Enqueue(objTrans);
            go.SetActive(false);
        }
    }

    private void GenerarNaveEnemiga02()
    {

        GameObject ship02 = Instantiate(EnemyShip02, Vector3.zero, Quaternion.identity) as GameObject;
        Transform ship02Trans = ship02.transform ;
        ship02Trans.parent = transform;

        GameObject por = Instantiate(Portal, Vector3.zero, Quaternion.identity) as GameObject;
        Transform porTrans = por.transform;
        porTrans.parent = transform;

        //Transform ship02 = enemyShip02Queue.Dequeue();
        NaveEnemigaControl nec = ship02.GetComponent<NaveEnemigaControl>();

        ship02.SetActive(true);
        //ship02Trans.position = transform.position + posNavesEnemigasPatron02[Random.Range(0, 5)] + CharacterManager.CharacterManagerInstance._Character.transform.localPosition;
        ship02Trans.localPosition = posNavesEnemigasPatron02[Random.Range(0, 5)];

        por.SetActive(true);
        //porTrans.position = ship02Trans.localPosition;
        porTrans.localPosition = ship02Trans.localPosition;
        porTrans.GetComponent<EnemyPortalTiempoVida>().IniciarTiempoVida(3f);

        ship02Trans.LookAt(CharacterManager.CharacterManagerInstance._Character.transform);
        nec.ValorEscudo = ne02ValorEscudo;
        nec.VelocidadDeFrente = ne02VelocidadDeFrente;
        nec.TiempoActPosObjetivo = ne02TiempoActPosObjetivo;
        nec.NumMaxLasers = ne02NumMaxLasers;
        nec.TiempoDisSigLaser = ne02TiempoDisSigLaser;
        nec.IniciarMovimiento();

        //ship02Trans.SetParent(null);
        //porTrans.SetParent(null);
        //enemyShip02Queue.Enqueue(ship02);

        //return ship02.transform;
    }

    IEnumerator IenGenerarOlasNavesEnemigas02()
    {
        //yield return new WaitForSeconds(8f);

        while (true)
        {
            GenerarNaveEnemiga02();
            yield return new WaitForSeconds(ne02TiempoSigNav);
        }
    }
}
