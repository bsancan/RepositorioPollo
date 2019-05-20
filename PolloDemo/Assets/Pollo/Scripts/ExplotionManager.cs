using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplotionManager : MonoBehaviour
{
    [Header("ExplosionEnemigos")]
    public GameObject ExplosionPrefab;
    public Queue<Transform> ExplosionQueue = new Queue<Transform>();
    public int TamañoPiscinaExplosion = 5;

    [Header("ExplosionPlayer")]
    public GameObject ExplosionPlayerPrefab;
    public Queue<Transform> ExplosionPlayerQueue = new Queue<Transform>();
    public int TamañoPisicinaExplosionPlayer = 2;


    void Start()
    {
        CrearExplosion();    
    }

    void Update()
    {
        
    }

    void CrearExplosion()
    {
        for (int i = 0; i < TamañoPiscinaExplosion; i++)
        {
            GameObject go = Instantiate(ExplosionPrefab, Vector3.zero, Quaternion.identity) as GameObject;
            Transform objTrans = go.transform;
            objTrans.parent = transform;
            objTrans.gameObject.name = objTrans.gameObject.name + "_" + i;
            ExplosionQueue.Enqueue(objTrans);
            go.SetActive(false);
        }

        for (int i = 0; i < TamañoPisicinaExplosionPlayer; i++)
        {
            GameObject go = Instantiate(ExplosionPlayerPrefab, Vector3.zero, Quaternion.identity) as GameObject;
            Transform objTrans = go.transform;
            objTrans.parent = transform;
            objTrans.gameObject.name = objTrans.gameObject.name + "_" + i;
            ExplosionPlayerQueue.Enqueue(objTrans);
            go.SetActive(false);
        }
    }

    public void ObtenerExplosion(Transform asteroid, Vector3 ReEscalaTamaño, float TiempoVidaExplosion)
    {
        Transform spawnedExplosion = ExplosionQueue.Dequeue();
        spawnedExplosion.GetComponent<ExplotionParticle>().ParametrosIniciales (ReEscalaTamaño.x * asteroid.localScale, TiempoVidaExplosion);
        spawnedExplosion.position = asteroid.position;

        print(asteroid.name);
        print(ReEscalaTamaño + asteroid.localScale);

        spawnedExplosion.gameObject.SetActive(true);
        
        ExplosionQueue.Enqueue(spawnedExplosion);
    }

    public void ObtenerExplosionPlayer(Transform pos)
    {
        Transform spawnedExplosion = ExplosionPlayerQueue.Dequeue();
        spawnedExplosion.GetComponent<ExplotionPlayerParticle>().ParametrosIniciales(Vector3.one, 1f);
        spawnedExplosion.parent = pos;
        spawnedExplosion.localPosition = new Vector3(0f, 0.6f, 0f);
        //spawnedExplosion.position = pos.position;
        spawnedExplosion.gameObject.SetActive(true);
        ExplosionPlayerQueue.Enqueue(spawnedExplosion);
    }
}
