using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteraction : MonoBehaviour {

	VillagerInteraction personaje;

	ObjectInteraction objeto;

	PlayerController control;

	TextManager texto;

	GameObject panelTexto;
	// Use this for initialization
	void Start () {
		texto = FindObjectOfType<TextManager>();
		control = GetComponent<PlayerController>();
		panelTexto = GameObject.Find("PanelTexto");
		panelTexto.SetActive(false);
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetKeyDown("space"))
		{
			evaluarAccion();
		}
	}

	void evaluarAccion()
	{
		if(personaje != null)
		{
			panelTexto.SetActive(!panelTexto.activeInHierarchy);
			control.cambiarCaminar(control.caminar);
			texto.setTexto(personaje.interactuar());
		}
		else if(objeto != null)
		{
			panelTexto.SetActive(!panelTexto.activeInHierarchy);
			control.cambiarCaminar(false);
			texto.setTexto(objeto.interactuar());
			GameObject temp = objeto.gameObject;
			objeto = null;
			temp.SetActive(false);
		}
		else if(objeto == null)
		{
			panelTexto.SetActive(false);
			control.cambiarCaminar(true);
		}
	}

	public void entraPersonaje(VillagerInteraction villager)
	{
		personaje = villager;
	}

	public void entraObjeto(ObjectInteraction obj)
	{
		objeto = obj;
	}
}
