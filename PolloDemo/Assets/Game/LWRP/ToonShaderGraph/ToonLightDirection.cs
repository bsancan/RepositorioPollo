using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToonLightDirection : MonoBehaviour
{

    //_ToonLightDirection
    // Start is called before the first frame update
    void Start()
    {
     
    }

    // Update is called once per frame
    void Update()
    {
        Shader.SetGlobalVector("_ToonLightDirection", -this.transform.forward);
    }
}
