using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidCollider : MonoBehaviour
{
    public Asteroid asteroid;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("PlayerAmmo"))
        {
            //aplico daño al asteroide
            asteroid.TakeDamage(other.GetComponent<PlayerAmmo>().ValorDaño);

        }
        else if (other.gameObject.CompareTag("PlayerShield") || other.gameObject.CompareTag("Player"))
        {
            //gameObject.SetActive(false);
            GameManager.GameManagerInstance._ExplotionManager.ObtenerExplosion(asteroid.transform, asteroid.ReEscalaExplosion, asteroid.TiempoVidaExplosion);
            Destroy(asteroid.gameObject);
        }
    }
}
