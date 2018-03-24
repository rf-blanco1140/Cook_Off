using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class PlayerInteraction : MonoBehaviour {

	VillagerInteraction personaje;

	ObjectInteraction objeto;

	PlayerController control;

	TextManager texto;

	GameObject panelTexto;

	GameObject panelPregunta;

	bool respondiendoPregunta;

	// Sistema de eventos
    EventSystem evSys;

	// Use this for initialization
	void Start () {
		texto = FindObjectOfType<TextManager>();
		control = GetComponent<PlayerController>();
		panelTexto = GameObject.Find("PanelTexto");
		panelPregunta = GameObject.Find("PanelPregunta");
		panelTexto.SetActive(false);
		panelPregunta.SetActive(false);
		TextManager.ResponderAldeano += EvaluarAccion;
		evSys = EventSystem.current;
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetKeyDown("space"))
		{
			EvaluarAccion();
		}
	}

	void EvaluarAccion()
	{
		if(respondiendoPregunta)
		{
			respondiendoPregunta = false;
			bool respuestaUsuario = texto.darRespuesta();
			panelPregunta.SetActive(false);
			texto.setTexto(personaje.responder(respuestaUsuario));
			if(respuestaUsuario)
			{
				SceneManager.LoadScene("EscenaBatalla");
			}
		}
		else if(personaje != null)
		{
			panelTexto.SetActive(!panelTexto.activeInHierarchy);
			control.cambiarCaminar(!control.caminar);
			texto.setTexto(personaje.interactuar());
			if(personaje.hasQuestion)
			{
				EventSystem.current.SetSelectedGameObject(null);
                StartCoroutine(waitToSelect());
				respondiendoPregunta = true;
				panelPregunta.SetActive(true);
			}
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

	// Corrutina apra que se pueda seleccionar el primer elemento apenas se abre el menu
    public IEnumerator waitToSelect()
    {
        yield return new WaitForEndOfFrame();
        EventSystem.current.SetSelectedGameObject(GameObject.Find("YES"));
    }
}
