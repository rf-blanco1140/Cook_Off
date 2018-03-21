using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextManager : MonoBehaviour {

	Text texto;
	// Use this for initialization
	void Start () {
		texto = GetComponentInChildren<Text>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void setTexto(string nuevo)
	{
		texto.text = nuevo;
	}
}
