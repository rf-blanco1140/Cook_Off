﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class POIngrediente : MonoBehaviour {

	// Enumerador para los sabores que existen
	public enum Sabor {Amargo, Dulce, Salado, Acido, Umami};

	// Enumerador para las texturas que existen
	public enum Textura {Crujiente, Suave, Humedo, Seco};

	// Valores numéricos que posee el ingrediente para cada uno de los sabores

	float dulce;

	float salado;

	float amargo;

	float acido;

	float umami;

	// Indican las texturas que posee o no el ingrediente

	bool crujiente;

	bool humedo;

	bool seco;

	bool suave;

	bool cortado;

	// Estado en el que se encuentra el ingrediente
	POEstado estado;

	// En caso de tratarse de una mezcla de ingredientes,
	// o del plato terminado, lista de ingredientes que lo componen
	List<POIngrediente> componentes;
	
	void Start () {
		estado = new POEstado();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	// Retorna el valor numérico del sabor especificado. En caso de tratarse de una mezcla,
	// retorna la suma del sabor de todos los ingredientes que componen la mezcla.
	public float darSabor(Sabor pSabor)
	{
		float cantidadSabor = 0;
		
		foreach(POIngrediente ingrediente in componentes)
		{
			cantidadSabor += ingrediente.darSabor(pSabor);
		}			
		
		switch(pSabor)
		{
			case Sabor.Acido:
			cantidadSabor += acido; 
			break;
			case Sabor.Dulce:
			cantidadSabor += dulce;
			break;
			case Sabor.Amargo:
			cantidadSabor += amargo;
			break;
			case Sabor.Salado:
			cantidadSabor += salado;
			break;
			case Sabor.Umami:
			cantidadSabor += umami;
			break;         		
			default:
			cantidadSabor += 0;
			break;
		}	

		return cantidadSabor;
	}

	// Retorna si el ingrediente especificado posee la textura especificada.
	// Si es una mezcla, indica si entre todos los ingredientes alguno posee la textura,
	// si uno de los ingredientes posee la textura se infiere que la mezcla o plato la tiene
	public bool darTextura(Textura pTextura)
	{
		bool laTextura = false;
		
		switch(pTextura)
		{
			case Textura.Suave:
				laTextura = suave; 
				break;  
			case Textura.Crujiente:
				laTextura = crujiente; 
				break; 
			case Textura.Seco:
				laTextura = seco; 
				break; 
			case Textura.Humedo:
				laTextura = humedo; 
				break; 			      		
			default:
				break;
		}	

		foreach(POIngrediente ingrediente in componentes)
		{
			laTextura = laTextura || ingrediente.darTextura(pTextura);
		}			
		return laTextura;
	}

	// Permite configurar los valores numéricos de los sabores que aporta el ingrediente
	public void configurarSabor(float pDulce, float pSalado, float pAmargo, float pAcido, float pUmami)
	{
		dulce = pDulce;
		amargo = pAmargo;
		salado = pSalado;
		acido = pAcido;
		umami = pUmami;
	}

	// Permite configurar las texturas que aporta el ingrediente
	public void configurarTextura(bool pSuave, bool pCrujiente, bool pHumedo, bool pSeco)
	{
		suave = pSuave;
		seco = pSeco;
		crujiente = pCrujiente;
		humedo = pHumedo;
	}

	// Adiciona los ingredientes especificados a la lista de componentes
	public void mezclar(List<POIngrediente> pIngredientes)
	{
		foreach(POIngrediente ingrediente in pIngredientes)
		{
			componentes.Add(ingrediente);
		}
	}

	// Corta el ingrediente, o si es el caso, todos los componentes de la mezcla
	public void cortar()
	{
		estado.cortar();
		foreach(POIngrediente ingrediente in componentes)
		{
			ingrediente.cortar();
		}
	}

	// Cocina el ingrediente, o si es el caso, todos los componentes de la mezcla
	public void cocinar()
	{
		estado.cocinar();
		foreach(POIngrediente ingrediente in componentes)
		{
			ingrediente.cocinar();
		}
	}

    // Cambia el estado a servido
	public void servir()
	{
		estado.servir();
	}

	// Indica si el ingrediente o sus componentes fueron cortados.
	// En caso de que el ingrediente o sus componentes en su totalidad no requieran
	// corte o han sido cortados retorna true, false en caso contrario
	public bool fueCortado()
	{
		bool cortarse = estado.fueCortado();
		foreach(POIngrediente ingrediente in componentes)
		{
			cortarse = cortarse && ingrediente.fueCortado();
		}
		return cortarse;
	}

	// Indica si el ingrediente o sus componentes fueron cocinados.
	// En caso de que el ingrediente o sus componentes en su totalidad no requieran
	// cocción o han sido cocinados retorna true, false en caso contrario
	public bool fueCocinado()
	{
		bool cocinarse = estado.fueCocinado();
		foreach(POIngrediente ingrediente in componentes)
		{
			cocinarse = cocinarse && ingrediente.fueCocinado();
		}
		return cocinarse;
	}

	// Permite configurar los requerimientos de corte y cocción del ingrediente
	public void configurar(bool pCorte, bool pCoccion)
	{
		estado.configurar(pCorte, pCoccion);
	}
}