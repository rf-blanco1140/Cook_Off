using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EvaluarComando : MonoBehaviour
{

	// Comando que actualmente se encuentra en contacto con el indicador de tiempo
	Comando comandoActual;

    private int numComandosDetectados;

    float multiplicadorComando;

    Image barraIndicador;


	void Start ()
    {
        barraIndicador = GameObject.Find("Indicador").GetComponent<Image>();
        numComandosDetectados = 0;
	}
	
	// Evalúa si el usuario está oprimiendo un botón, y si este botón coincide
	// con el comando que actualmente está interactuando con el indicador
	void Update () {
		if(comandoActual != null && Input.GetKeyDown(comandoActual.darTecla()))
		{
            comandoActual.gameObject.GetComponent<Image>().color = Color.green;
            StartCoroutine("MostrarAcierto");
            comandoActual.terminar();
        }
	}

	// Si entra en contacto con un comando lo guarda para su evaluación en el Update
	void OnTriggerEnter(Collider collision)
    {
        if(collision.gameObject.tag == "comando")
        {
        	comandoActual = collision.gameObject.GetComponent<Comando>();
            multiplicadorComando++;
            numComandosDetectados++;
            SistemaDePuntos.instance.aumentarPuntaje(multiplicadorComando * 100);
        }
    }

	// Si un comando sale de contacto con el indicador se elimina para que no sea evaluado
	// después de tiempo
    void OnTriggerExit(Collider collision)
    {
        if(collision.gameObject.tag == "comando")
        {
            comandoActual.gameObject.GetComponent<Image>().color = Color.red;
        	comandoActual = null;
            multiplicadorComando = 0;
        }
    }

    private void OnEnable()
    {
        numComandosDetectados = 0;
    }

    private IEnumerator MostrarAcierto()
    {
        barraIndicador.color = Color.green;
        yield return new WaitForSeconds(0.2f);
        barraIndicador.color = Color.white;
    }
}
