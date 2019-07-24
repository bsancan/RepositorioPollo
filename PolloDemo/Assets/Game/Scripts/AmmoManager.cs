using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoManager : MonoBehaviour
{
    [Header("Player")]
    public GameObject PlayerAmmoPrefab = null;
    public float VelocidadPlayerAmmo = 60f;
    public float VelocidadFuego = 0.1f;
    public float TiempoVidaPlayerAmmo = 1f;
    public int ValorDañoPlayerAmmo = 10;
    public int TamañoPiscinaPlayerAmmo = 20;
    public Queue<Transform> PlayerAmmoQueue = new Queue<Transform>();

    [Header("Enemy")]
    public GameObject EnemyAmmoPrefab = null;
    public float VelocidadEnemyAmmo = 60f;
    //public float VelocidadFuegoEnemy = 0.1f;
    public float TiempoVidaEnemyAmmo = 1f;
    public int ValorDañoEnemyAmmo = 10;
    public int TamañoPiscinaEnemyAmmo = 20;
    public Queue<Transform> EnemyAmmoQueue = new Queue<Transform>();


    void Start()
    {
        CrearPlayerAmmo();
    }

    void Update()
    {

    }

    private void CrearPlayerAmmo()
    {
        for (int i = 0; i < TamañoPiscinaPlayerAmmo; i++)
        {
            GameObject go = Instantiate(PlayerAmmoPrefab, Vector3.zero, Quaternion.identity) as GameObject;
            Transform objTrans = go.transform;
            objTrans.parent = transform;
            objTrans.gameObject.name = objTrans.gameObject.name + "_" + i;
            PlayerAmmoQueue.Enqueue(objTrans);
            go.SetActive(false);
        }
    }


    public void ObtenerPlayerAmmo(Transform playerAmmo)
    {
        Transform spawnedAmmo = PlayerAmmoQueue.Dequeue();
        PlayerAmmo pa = spawnedAmmo.GetComponent<PlayerAmmo>();
        pa.SetPlayerAmmo(VelocidadPlayerAmmo, TiempoVidaPlayerAmmo, ValorDañoPlayerAmmo);

        spawnedAmmo.gameObject.SetActive(true);
        spawnedAmmo.position = playerAmmo.position + (playerAmmo.forward * 2f);
        spawnedAmmo.rotation = playerAmmo.rotation;
        PlayerAmmoQueue.Enqueue(spawnedAmmo);

        //return spawnedAmmo;
    }

    public void CrearEnemyAmmo()
    {
        for (int i = 0; i < TamañoPiscinaEnemyAmmo; i++)
        {
            GameObject go = Instantiate(EnemyAmmoPrefab, Vector3.zero, Quaternion.identity) as GameObject;
            Transform objTrans = go.transform;
            objTrans.parent = transform;
            objTrans.gameObject.name = objTrans.gameObject.name + "_" + i;
            EnemyAmmoQueue.Enqueue(objTrans);
            go.SetActive(false);
        }
    }


    public void ObtenerEnemyAmmo(Transform enemyAmmo)
    {
        Transform spawnedAmmo = EnemyAmmoQueue.Dequeue();
        EnemyAmmo pa = spawnedAmmo.GetComponent<EnemyAmmo>();
        pa.SetEnemyAmmo(VelocidadEnemyAmmo, TiempoVidaEnemyAmmo, ValorDañoEnemyAmmo);

        spawnedAmmo.gameObject.SetActive(true);
        spawnedAmmo.position = enemyAmmo.position + (enemyAmmo.forward * 2f);
        spawnedAmmo.rotation = enemyAmmo.rotation;
        EnemyAmmoQueue.Enqueue(spawnedAmmo);

        //return spawnedAmmo;
    }

}


