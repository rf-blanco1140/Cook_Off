using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

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
        enciclopedia = new GameObject[3];
    }
	
    public GameObject[] getInventario()
    {
        return inventario;
    }

    public GameObject[] getEnciclopedia()
    {
        return enciclopedia;
    }

    public void poblarEnciclopedia()
    {
        GameObject nuevoIngrediente = new GameObject();
        nuevoIngrediente.name = "CarneAsada";
        enciclopedia[0] = nuevoIngrediente;

        nuevoIngrediente = new GameObject();
        nuevoIngrediente.name = "Sal";
        enciclopedia[1] = nuevoIngrediente;

        nuevoIngrediente = new GameObject();
        nuevoIngrediente.name = "Aceite";
        enciclopedia[2] = nuevoIngrediente;
    }
    
}
