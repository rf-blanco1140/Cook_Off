using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ManejoTiempoCocina : MonoBehaviour {

	Image imagenBarra;

	float maxTiempo = 1f;

	float duracion;

	float velocidad = 0.01f;

	bool enProgreso = false;
	// Use this for initialization
	void Start () {
		imagenBarra = GameObject.Find("Relleno").GetComponent<Image>();
		this.gameObject.SetActive(false);
	}
	
	public void empezar(float pDuracion)
	{
		imagenBarra.fillAmount = 0;
		duracion = pDuracion;
		StartCoroutine("CorrerTiempo");
	}

	IEnumerator CorrerTiempo()
	{
		enProgreso = true;
		while(imagenBarra.fillAmount < 1)
		{
			imagenBarra.fillAmount = imagenBarra.fillAmount + maxTiempo*0.05f;
			yield return new WaitForSeconds(duracion*0.05f);
		}
		enProgreso = false;
	}

	public bool estaEnProgreso()
	{
		return enProgreso;
	}
}
