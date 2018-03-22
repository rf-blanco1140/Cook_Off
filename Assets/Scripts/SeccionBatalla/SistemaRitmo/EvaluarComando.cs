using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EvaluarComando : MonoBehaviour
{

	// Comando que actualmente se encuentra en contacto con el indicador de tiempo
	Comando comandoActual;

    private int numComandosDetectados;


	void Start ()
    {
        numComandosDetectados = 0;
	}
	
	// Evalúa si el usuario está oprimiendo un botón, y si este botón coincide
	// con el comando que actualmente está interactuando con el indicador
	void Update () {
		if(comandoActual != null && Input.GetKeyDown(comandoActual.darTecla()))
		{
			comandoActual.terminar();
            ComandosManager.instance.revisarSiTerminarSistemaRitmo(numComandosDetectados);
        }
	}

	// Si entra en contacto con un comando lo guarda para su evaluación en el Update
	void OnTriggerEnter(Collider collision)
    {
        if(collision.gameObject.tag == "comando")
        {
        	comandoActual = collision.gameObject.GetComponent<Comando>();
            numComandosDetectados++;
        }
    }

	// Si un comando sale de contacto con el indicador se elimina para que no sea evaluado
	// después de tiempo
    void OnTriggerExit(Collider collision)
    {
        if(collision.gameObject.tag == "comando")
        {
        	comandoActual = null;
            ComandosManager.instance.revisarSiTerminarSistemaRitmo(numComandosDetectados);
        }
    }

    private void OnEnable()
    {
        numComandosDetectados = 0;
    }
}
