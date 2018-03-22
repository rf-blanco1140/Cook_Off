using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuManager : MonoBehaviour
{

    //---------------------------------------------------------------------------
    // Variables
    //---------------------------------------------------------------------------

    // Instancia estatica que le permite ser accedido por cualquier Script
    public static MenuManager instance = null;

    // Menu que se puede sacar cuenado se esta en el mapa
    private GameObject inMapMenu;

    // Ultima accion seleccionada en el menu
    private GameObject ultimaOpcionSeleccionada;

    //---------------------------------------------------------------------------
    // Methods
    //---------------------------------------------------------------------------

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

        //Sets this to not be destroyed when reloading scene
        DontDestroyOnLoad(gameObject);
    }

    // Use this for initialization
    void Start ()
    {
        inMapMenu = GameObject.Find("MenuPanel");
        inMapMenu.SetActive(false);
    }
	
	// Update is called once per frame
	void Update ()
    {
		if(Input.GetKeyDown("return"))
        {
            inMapMenu.SetActive(true);
        }
	}

    public GameObject getUltimaOpcionSeleccionada()
    {
        return ultimaOpcionSeleccionada;
    }
}
