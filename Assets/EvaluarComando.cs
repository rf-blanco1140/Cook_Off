using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EvaluarComando : MonoBehaviour {

	Comando comandoActual;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if(comandoActual != null && Input.GetKeyDown(comandoActual.darTecla()))
		{
			comandoActual.terminar();
		}
	}

	void OnTriggerEnter(Collider collision)
    {
        if(collision.gameObject.tag == "comando")
        {
        	comandoActual = collision.gameObject.GetComponent<Comando>();
        }
    }

    void OnTriggerExit(Collider collision)
    {
        if(collision.gameObject.tag == "comando")
        {
        	comandoActual = null;
        }
    }
}
