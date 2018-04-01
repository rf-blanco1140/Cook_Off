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
    private List<POIngrediente> inventario;

    // Enciclopedia donde se muestran todos los items en existencia
    private List<POIngrediente> enciclopedia;




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


        IngredientLoader loader = GameObject.Find("Loader").GetComponent<IngredientLoader>();
        enciclopedia = loader.setUpAndRun();

        //GameObject.Find("ContentAdder").GetComponent<EnciclopediaManager>().poblar();
    }

    

    // Use this for initialization
    void Start ()
    {
        
            
        

       // inventario = new GameObject[100];
        //enciclopedia = new GameObject[3];
    }

    public void llenarInvenatrio()
    {
            inventario = enciclopedia;
    }
	
    public List<POIngrediente> getInventario()
    {
        return inventario;
    }

    public List<POIngrediente> getEnciclopedia()
    {
        return enciclopedia;
    }

    public void sacarDeInventario(string aSacar)
    {
        POIngrediente remover = null;
        foreach(POIngrediente ingrediente in inventario)
        {
            if(ingrediente.getNombre().Equals(aSacar))
            {
                remover = ingrediente;
            }
        }
        inventario.Remove(remover);
    }
    
    public void agregarEnInventario(POIngrediente aAgregar)
    {
        Debug.Log("dentro de inventario nombre es "+aAgregar.getNombre());
        inventario.Add(aAgregar);
        Debug.Log("hay "+(inventario.Count-1)+" posisione en ingredeintes");
        Debug.Log("nombre ultimo elemento lista es "+inventario[inventario.Count-1].getNombre());
    }
}
