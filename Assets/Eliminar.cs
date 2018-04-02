using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Eliminar : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void Borrar()
    {
        ComandosManager.instance.darPool().ReturnObject(this.gameObject);
    }
}
