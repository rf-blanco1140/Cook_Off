﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GeneradorComandos : MonoBehaviour
{

    public static GeneradorComandos instance = null;

    //Sistema de pool para los prefabs de los comandos
    SimpleObjectPool pool;

	// Transform de referencia para el recorrido de los comandos
	public Transform inicio;

	// Transform de referencia para el recorrido de los comandos
	public Transform fin;

	// Arreglo de comandos a ser ejecutados, en teoría son configurados por la acción
	// e.g. Cortar ingrediente, mezclar ingredientes
	public Comando[] comandos;

	// Distancia temporal entre comandos en la secuencia
	public float delayComandos = 0.5f;

    //Awake is always called before any Start functions
    void Awake()
    {

        
        //Check if instance already exists
        if (instance == null)

            //if not, set instance to this
            instance = this;

        //If instance already exists and it's not this:
        else if (instance != this)

            //Then destroy this. This enforces our singleton pattern, meaning there can only ever be one instance of a GameManager.
            Destroy(gameObject);
    }

    // Configura el pool e inicia el proceso de autogenerar la secuencia de comandos especificada
    void Start () {
		pool = GetComponent<SimpleObjectPool>();
	}

	// Corutina para generar la secuencia de comandos a partir de 
	// la lista especificada por la acción.
	// Saca un prefab del pool, le configura el padre como este objeto,
	// le pasa el pool para que este comando pueda retornarse según su criterio,
	// configura los puntos de inicio y fin para la translación,
	// le asigna la tecla que debe presionarse a partir de la lista especificada,
	// le da la orden de que empiece su desplazamiento, y espera para producir el siguiente.
	private IEnumerator Reproducir()
	{
        //
        //int rondasDebug = 0;
        //Debug.Log("la cantidad de comandos es "+comandos.Length);
        //
		foreach(Comando com in comandos)
		{
            //Debug.Log("mira comandos a crear, esta en ronda "+rondasDebug);
            //if (com == null) { Debug.Log("el comando es null"); }
            //else { Debug.Log("el comando NO es null"); }
			GameObject temp = pool.GetObject();
			temp.transform.SetParent(this.transform);
			Comando actual = temp.GetComponent<Comando>();
            actual.gameObject.GetComponent<Image>().color = Color.white;
			actual.cuadrarPool(pool);
			actual.configurarPuntos(inicio.position, fin.position);
			actual.configurar(com.darTecla());
            com.GetComponent<Eliminar>().Borrar();
			actual.empezar();
            //rondasDebug++; //////////////////////////////////////////////////////////////////////////////////
			yield return new WaitForSeconds(delayComandos);		
		}
        //Debug.Log("termina");
		yield return new WaitForSeconds(5f);
        ComandosManager.instance.revisarSiTerminarSistemaRitmo();
	}

    void OnEnable()
    {
        StartCoroutine("Reproducir");
    }

    public void configurarComandos(Comando[] nuevos)
    {
        //Debug.Log("configuro comandos");
        comandos = nuevos;
    }
}
