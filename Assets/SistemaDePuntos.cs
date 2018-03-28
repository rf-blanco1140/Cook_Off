﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SistemaDePuntos : MonoBehaviour {

    float puntaje;

    float timer = 60;

    public static SistemaDePuntos instance = null;

    bool terminado = false;

    Text textoPuntaje;

    Text textoTimer;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

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

        textoPuntaje = GameObject.Find("Puntaje").GetComponent<Text>();
        textoTimer = GameObject.Find("Timer").GetComponent<Text>();
        StartCoroutine("CorrerTiempo");
    }

    public void aumentarPuntaje(float valor)
    {
        puntaje += valor;
        actualizarPuntaje();
    }

    public void evaluarPlatoFinal(List<POIngrediente> ingredientesServidos)
    {
        terminado = true;
        foreach(POIngrediente ingrediente in ingredientesServidos)
        {
            
        }
    }

    public void actualizarPuntaje()
    {
        textoPuntaje.text = puntaje + "";
    }

    public void actualizarTimer()
    {
        int minutos = (int)timer / 60;
        int segundos = (int) timer % 60;

        textoTimer.text = minutos + ":" + segundos;
    }

    IEnumerator CorrerTiempo()
    {
        while(!terminado && timer > 0)
        {
            timer = timer - 1;
            actualizarTimer();
            yield return new WaitForSeconds(1f);
        }
        if(!terminado)
        {
            evaluarPlatoFinal(BattleMenuManager.instance.darIngredientesServidos());
        }

    }
}
