using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class POIngrediente : MonoBehaviour {

	public enum Sabor {Amargo, Dulce, Salado, Acido, Umami};

	public enum Textura {Crujiente, Suave, Humedo, Seco};

	float dulce;

	float salado;

	float amargo;

	float acido;

	float umami;

	bool crujiente;

	bool humedo;

	bool seco;

	bool suave;

	bool cortado;

	List<POIngrediente> componentes;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

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

	public void configurarSabor(float pDulce, float pSalado, float pAmargo, float pAcido, float pUmami)
	{
		dulce = pDulce;
		amargo = pAmargo;
		salado = pSalado;
		acido = pAcido;
		umami = pUmami;
	}

	public void configurarTextura(bool pSuave, bool pCrujiente, bool pHumedo, bool pSeco)
	{
		suave = pSuave;
		seco = pSeco;
		crujiente = pCrujiente;
		humedo = pHumedo;
	}

	public void mezclar(List<POIngrediente> pIngredientes)
	{
		foreach(POIngrediente ingrediente in pIngredientes)
		{
			componentes.Add(ingrediente);
		}
	}
}
