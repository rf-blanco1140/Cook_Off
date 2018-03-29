using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ManejoTiempoCocina : MonoBehaviour {

    public static ManejoTiempoCocina instance = null;

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
        ComandosManager.instance.cambiarEstadoCocina(true);
        this.gameObject.SetActive(false);
	}

	public bool estaEnProgreso()
	{
		return enProgreso;
	}
}
