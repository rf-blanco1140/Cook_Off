using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VillagerInteraction : MonoBehaviour {

	public bool hasItem;

	public bool hasQuest;

    public bool hasQuestion;

    public string theQuestion;

    public string noAnswer;

    public string yesAnswer;

	public string theQuest;

	public string theItem; 

	public string defaultText;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnTriggerEnter2D(Collider2D other) {
        if(other.gameObject.tag == "Player")
        {
        	other.GetComponent<PlayerInteraction>().entraPersonaje(this);
        }
    }

    void OnTriggerExit2D(Collider2D other) {
        if(other.gameObject.tag == "Player")
        {
        	other.GetComponent<PlayerInteraction>().entraPersonaje(null);
        }
    }

    public string interactuar()
    {
    	if(hasQuestion)
        {
            return theQuestion;
        }
        else if(hasItem)
    	{
    		hasItem = false;
    		return theItem;
    	}
    	else if(hasQuest)
    	{
    		hasQuest = false;
    		return theQuest;
    	}
    	else
    	{
    		return defaultText;
    	}
    }

    public string responder(bool respuesta)
    {
        if(respuesta)
        {
            hasQuestion = false;
            return yesAnswer;
        }
        else
        {
            return noAnswer;
        }
    }
}
