using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Nivel01Manager : MonoBehaviour
{
    public static Nivel01Manager NivelManagerInstance;
    public int SiguientePatron;
    public GameObject[] PatronesAsteroides;

   

    private void Awake()
    {
        if (NivelManagerInstance == null)
        {
            NivelManagerInstance = this;
        }
        else
        {
            Destroy(gameObject);
        }

       
    }

    void Start()
    {
        
    }


    void Update()
    {
        
    }

    public void InstanciarPatronAsteroide()
    {
        if(SiguientePatron < PatronesAsteroides.Length)
        {
            GameObject go = Instantiate(PatronesAsteroides[SiguientePatron]);
            Transform objTrans = go.transform;
            objTrans.parent = transform;
            objTrans.position = PatronesAsteroides[SiguientePatron].transform.position;
            print("Patron instanciado - " + SiguientePatron);
            SiguientePatron++;
        }
     
    }
}
