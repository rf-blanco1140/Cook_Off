using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Accion : MonoBehaviour
{

    //---------------------------------------------------------------------------
    // Variables
    //---------------------------------------------------------------------------

    public static Accion instance = null;

    private bool estaSeleccionada;

    private BattleMenuManager instanciaBMManager;

    private GameObject[] ingredeintesQueUsara = new GameObject[100];

    private int posEnListaIngredeintesQueUsara;


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
        estaSeleccionada = false;
        instanciaBMManager = BattleMenuManager.instance;
        posEnListaIngredeintesQueUsara = 0;
	}

    public int getNumeroDeIngredientes()
    {
        return posEnListaIngredeintesQueUsara;
    }

    // Selecciona los ingredientes que se van a usar en cada accion
    public void agregarIngredeintesParaAccion(GameObject objetoIngredeinte)
    {
        ingredeintesQueUsara[posEnListaIngredeintesQueUsara] = objetoIngredeinte;
        posEnListaIngredeintesQueUsara++;

        imprimirObjetosEnLista();
    }

    // Retira el elemento de la lista pedido y se reorganiza la lisat de ingredientes a usar
    public void retirarDeIngredientesParaAccion(GameObject objetoRetirar)
    {
        bool loEncontro = false;
        string nombreObjeto = objetoRetirar.name;

        for(int i=0; !loEncontro && (i < (posEnListaIngredeintesQueUsara+1)); i++)
        {
            if (nombreObjeto == ingredeintesQueUsara[i].name)
            {
                if(i < posEnListaIngredeintesQueUsara)
                {
                    for (int j = i; j < posEnListaIngredeintesQueUsara; j++)
                    {
                        ingredeintesQueUsara[j] = ingredeintesQueUsara[j + 1];
                    }

                    ingredeintesQueUsara[posEnListaIngredeintesQueUsara] = null;
                    loEncontro = true;
                    posEnListaIngredeintesQueUsara--;
                }
            }
        }

        imprimirObjetosEnLista();
    }

    // Vacia la lista y retorna el indice de la posisicon actual a 0
    public void vaciarListaRecursosActuales()
    {
        for(int i=0; i < posEnListaIngredeintesQueUsara; i++)
        {
            GameObject objetoBorrar = ingredeintesQueUsara[i];
            ElementoRecursos elem = objetoBorrar.GetComponent<ElementoRecursos>();
            elem.deseleccionarEsteElemento();

            ingredeintesQueUsara[i] = null;
        }
        posEnListaIngredeintesQueUsara = 0;
    }

    // ------------------------------------------------------------------------------------------------------

    public void imprimirObjetosEnLista()
    {
        Debug.Log("--------- inicio---------");
        for(int i = 0; i < posEnListaIngredeintesQueUsara; i++)
        {
            Debug.Log(ingredeintesQueUsara[i]);
        }
        Debug.Log("---------Fin---------");
    }

    // Metodo usado para incicializar la lista de ingredientes a usar una vez se ha inicializado la lista de todos los ingredientes
    // EN el momento este metodo no srive pa nada !!!!!!!!!!!!!!!!!
    /*
    public void inicializarListas()
    {
        //ingredeintesQueUsara = new GameObject[instanciaBMManager.getIngredeintesUsables().Length];
    }*/
}
