using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Nivel01Manager : MonoBehaviour
{
    public static Nivel01Manager NivelManagerInstance;
    public Transform Portal;
    public int SiguientePatron;
    public GameObject[] PatronesAsteroides;

    private Vector3 portalOffset;

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
        portalOffset = Portal.position - CharacterManager.CharacterManagerInstance.transform.position;
    }


    void Update()
    {
        Portal.position = CharacterManager.CharacterManagerInstance.transform.position + portalOffset;
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
