using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boundary : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Asteroid") )
        {
            Destroy(other.GetComponentInParent<Asteroid>().gameObject);
        }else if (other.CompareTag("Item"))
        {
            Destroy(other.gameObject);
        }else if (other.gameObject.CompareTag("Enemy"))
        {
            Destroy(other.gameObject.GetComponentInParent<NaveEnemigaControl>().gameObject);
        }
        //print("TRI - " + other.gameObject.name);
    }

    

    private void OnCollisionEnter(Collision other)
    {
        //print("COL - " + other.gameObject.name);

        //if (other.gameObject.CompareTag("Enemy"))
        //{
        //    print(other.gameObject.name);
        //    Destroy(other.gameObject.GetComponentInParent<NaveEnemigaControl>().gameObject);
        //}
    }

}
