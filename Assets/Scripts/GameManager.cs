using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    //---------------------------------------------------------------------------
    // Variables
    //---------------------------------------------------------------------------

    // Instancia estatica que le permite ser accedido por cualquier Script
    public static GameManager instance = null;

    // Inventario donde se guardan todos los recursos que recoja el jugador
    private GameObject[] inventario;

    // Enciclopedia donde se muestran todos los items en existencia
    private GameObject[] enciclopedia;

    private GameObject menu;


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
        inventario = new GameObject[100];
        enciclopedia = new GameObject[100];
        menu = GameObject.Find("MenuPanel");
        menu.SetActive(false);
    }
	
    public GameObject[] getInventario()
    {
        return inventario;
    }

    public GameObject[] getEnciclopedia()
    {
        return enciclopedia;
    }

    public void openMenu()
    {
        menu.SetActive(true);
        menu.
    }
}
