using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BotonRecurso : MonoBehaviour
{
    public Text nombreBoton;

    //private POIngrediente ingredienteStats;

    // posiscion en al lista de ingredeientes del Game Manager
    private int posComponenteIngredeinte;



    public void inicializarValoresBoton(int posEnLista, string nombre)
    {
        nombreBoton.text = nombre;
        posComponenteIngredeinte = posEnLista;
    }

    public void agregarComponenteIngredeinte(int posEnLista)
    {
        posComponenteIngredeinte = posEnLista;
    }

    public int posicionDeRecurso()
    {
        return posComponenteIngredeinte;
    }

    
}
