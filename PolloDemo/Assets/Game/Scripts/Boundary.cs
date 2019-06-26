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
        }
        if (other.CompareTag("Item"))
        {
            Destroy(other.gameObject);
        }
    }

}
