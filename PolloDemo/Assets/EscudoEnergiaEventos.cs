using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EscudoEnergiaEventos : MonoBehaviour
{
    private Animator animator;
    private Renderer render;

    private void Awake()
    {
        render = GetComponent<Renderer>();
        animator = GetComponent<Animator>();
    }
    void Start()
    {
        //ienMostrarEscudo = CorMuestraEscudo();
        render.enabled = false;
    }

    public void IniciaAnimacion()
    {
        render.enabled = true;
        animator.SetTrigger("Damage");
    }

    public void TerminaAnimacion()
    {
        render.enabled = false;
    }
}
