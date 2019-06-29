using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HistoriaManager : MonoBehaviour
{
    public static HistoriaManager HistoriaManagerInstance;
    public Animator AniHistoria;

    private void Awake()
    {
        if (HistoriaManagerInstance == null)
        {
            HistoriaManagerInstance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        
    }

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
   

    
}
