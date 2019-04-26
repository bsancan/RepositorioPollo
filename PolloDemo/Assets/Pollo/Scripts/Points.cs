using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Points : MonoBehaviour
{
    [SerializeField]
    private Text txtPuntos;
    [SerializeField]
    private Animator animator;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartAnimation(string value, Color colortext)
    {
        txtPuntos.text = value;
        txtPuntos.color = colortext;
        animator.SetTrigger("Inicio");
        //CancelInvoke();
        //Invoke("Die", lifeTime);
    }

    //void Die()
    //{
    //    CancelInvoke();
    //    //gameObject.SetActive(false);
    //    Destroy(gameObject);
    //}
}
