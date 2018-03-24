using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextManager : MonoBehaviour {

	public delegate void Responder();
    public static event Responder ResponderAldeano;

	public Text texto;

	bool respuesta;
	// Use this for initialization
	void OnAwake () {
		texto = GetComponentInChildren<Text>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void setTexto(string nuevo)
	{
		texto.text = nuevo;
	}

	public void respuestaAfirmativa()
	{
		respuesta = true;
		ResponderAldeano();
	}

	public void respuestaNegativa()
	{
		respuesta = false;
		ResponderAldeano();
	}

	public bool darRespuesta()
	{
		return respuesta;
	}
}
