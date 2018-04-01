using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BotonRecurso : MonoBehaviour
{
    public Text nombreBoton;

    private POIngrediente ingredienteStats;



    public void inicializarValoresBoton(GameObject objetoIngrediente)
    {
        //Debug.Log("inicializo los botones");
        ingredienteStats = objetoIngrediente.GetComponent<POIngrediente>();
        nombreBoton.text = ingredienteStats.getNombre();
    }
}
