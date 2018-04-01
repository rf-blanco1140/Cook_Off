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



    public void inicializarValoresBoton(int posEnLista)
    {
        //ingredienteStats = GameManager.instance.getElementoInventario(posEnLista); //objetoIngrediente.GetComponent<POIngrediente>();
        nombreBoton.text = GameManager.instance.getElementoInventario(posEnLista).getNombre();
    }

    public void agregarComponenteIngredeinte(int posEnLista)
    {
        posComponenteIngredeinte = posEnLista;
    }
}
