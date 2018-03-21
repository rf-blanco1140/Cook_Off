using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectInteraction : MonoBehaviour {

	public string theItem;
	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnTriggerEnter2D(Collider2D other) {
        if(other.gameObject.tag == "Player")
        {
        	other.GetComponent<PlayerInteraction>().entraObjeto(this);
        }
    }

    void OnTriggerExit2D(Collider2D other) {
        if(other.gameObject.tag == "Player")
        {
        	other.GetComponent<PlayerInteraction>().entraObjeto(null);
        }
    }

    public string interactuar()
    {
    	return theItem;
    }
}
