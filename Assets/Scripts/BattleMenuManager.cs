using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class BattleMenuManager : MonoBehaviour
{

    //---------------------------------------------------------------------------
    // Variables
    //---------------------------------------------------------------------------

    public static BattleMenuManager instance = null;

    // Por el momento se van a entender como recursos disponibles los ingredientes que puede usar el jugador
    private string[] recursosDisponibles;

    // Es el objeto que contiene los recursos disponibles en la escena del juego
    private GameObject objetoPadreRecursosDisponibles;

    // Accion que se esta seleccionando
    private GameObject accionSeleccionada;

    // Subaccion que se esta seleccionando
    private GameObject subAccionSeleccionada;

    // Lista con todos los ingredientes, en cualquier estado
    private GameObject[] listaTotalIngredientes;

    // Lista de los ingredientes que se van a usar en la subaccion seleccionada
    private GameObject[] ingredientesListosParaUsar;

    private GameObject peleaRitmoReference;

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
    void Start()
    {
        //Disapear mouse cousor
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        objetoPadreRecursosDisponibles = GameObject.Find("Recursos");
        peleaRitmoReference = GameObject.Find("PeleaRitmo");

        ingredientesListosParaUsar = new GameObject[objetoPadreRecursosDisponibles.transform.childCount];
        listaTotalIngredientes = new GameObject[objetoPadreRecursosDisponibles.transform.childCount];
        for (int i=0; i < objetoPadreRecursosDisponibles.transform.childCount; i++)
        {
            ingredientesListosParaUsar[i] = objetoPadreRecursosDisponibles.transform.GetChild(i).gameObject;
            listaTotalIngredientes[i] = ingredientesListosParaUsar[i];
        }

        objetoPadreRecursosDisponibles.SetActive(false);
        peleaRitmoReference.SetActive(false);

        //Accion.instance.inicializarListas();
    }

    // Metodo que guarda la ultima accion seleccionada
    public void seleccionarAccion(GameObject pAccionSeleccionada)
    {
        accionSeleccionada = pAccionSeleccionada;
    }

    // Metodo que guarda la ultima subaccion seleccionada
    public void seleccionarSubAccions(GameObject pSubAccionSeleccionada)
    {
        subAccionSeleccionada = pSubAccionSeleccionada;
    }

    /// <summary>
    /// Retorna la ultima accion seleccionada de no haber accion retorna null
    /// </summary>
    /// <returns> la ultima accion activamente seleccionada, null si no se esta seleccinoando una accion o subaccion </returns>
    public GameObject getAccionSeleccionada()
    {
        return accionSeleccionada;
    }

    /// <summary>
    /// Retorna la ultima subaccion seleccionada de no haber accion retorna null
    /// </summary>
    /// <returns> la ultima accion activamente seleccionada, null si no se esta seleccinoando una subaccion </returns>
    public GameObject getSubAccionSeleccionada()
    {
        return subAccionSeleccionada;
    }

    /// <summary>
    /// Deja la accion seleccionada como null
    /// esto indica que ene ste momento no se esta seleccionando una accion
    /// </summary>
    public void limpiarAccionSeleccionada()
    {
        accionSeleccionada = null;
    }

    /// <summary>
    /// Deja la subaccion seleccionada como null
    /// esto indica que ene ste momento no se esta seleccionando una subaccion
    /// </summary>
    public void limpiarSubAccionSeleccionada()
    {
        subAccionSeleccionada = null;
    }

    /// <summary>
    /// Devuelve el marcador del menu de accion a el elemento previo del que salio el menu actual
    /// </summary>
    /// <returns> el GameObject previo del que salio el menu actual </returns>
    public GameObject backToLastOpcionSelected()
    {
        GameObject rtaElement = null;

        if(subAccionSeleccionada != null)
        {
            rtaElement = subAccionSeleccionada;
            subAccionSeleccionada = null;
        }
        else
        {
            rtaElement = accionSeleccionada;
            accionSeleccionada = null;
        }

        return rtaElement;
    }

    /// <summary>
    /// Define la lista de ingredientes que se van a mostrar en base a la accion que se va a realizar
    /// Se llama cada vez que se selecciona la subaccion
    /// </summary>
    /// <param name="accionDefinida"> La accion que se va a realizar </param>
    public void definirIngredientesUsables(string accionDefinida)
    {
        for (int i = 0; i < listaTotalIngredientes.Length; i++)
        {
            GameObject esteIngrediente = listaTotalIngredientes[i];

            if (accionDefinida == "Cortar")
            {
                if (esteIngrediente.GetComponent<POEstado>().fueCortado() == false)
                {
                    ingredientesListosParaUsar[i] = esteIngrediente;
                }
            }
            else if (accionDefinida == "Mezclar")
            {
                // Por ahora se asume que se muestran todos
                ingredientesListosParaUsar[i] = esteIngrediente;
            }
            else if (accionDefinida == "cocinar")
            {
                if (esteIngrediente.GetComponent<POEstado>().fueCocinado() == false)
                {
                    ingredientesListosParaUsar[i] = esteIngrediente;
                }
            }
            else if (accionDefinida == "Servir")
            {
                // Por ahora se asume que se muestran todos
                ingredientesListosParaUsar[i] = esteIngrediente;
            }
        }
    }

    public GameObject[] getIngredeintesUsables()
    {
        return ingredientesListosParaUsar;
    }


    // METODO PARA TERMINAR LUEGO !!!!!!!!!!!!!!!!!
    // Llena el obnjeto recursos con los recursos disponibles
    public void crearRecursos()
    {
        GameObject nuevoRecurso = null;
        GameObject textoNuevoRecurso= null;

        for(int i=0; i<recursosDisponibles.Length; i++)
        {
            nuevoRecurso = new GameObject();
            textoNuevoRecurso = new GameObject();

            nuevoRecurso.AddComponent<Image>();
            nuevoRecurso.AddComponent<Button>();

            textoNuevoRecurso.AddComponent<Text>();
        }
    }

    public void lanzarSistemaRitmo()
    {
        peleaRitmoReference.SetActive(true);
    }

    public void terminarSistemaRitmo()
    {
        peleaRitmoReference.SetActive(false);
        EventSystem.current.SetSelectedGameObject(backToLastOpcionSelected());
    }

    public GameObject getSubaccionSeleccionada()
    {
        return subAccionSeleccionada;
    }

    // Method that manages the mouse click events so it dosn't fuck up everything else
    private void onMouseClickReturnToOriginalPosition()
    {
        if (Input.GetMouseButtonDown(0))
        {
            EventSystem.current.SetSelectedGameObject(null);
            EventSystem.current.SetSelectedGameObject(EventSystem.current.firstSelectedGameObject);
            //Debug.Log("Pressed left click.");
        }

        if (Input.GetMouseButtonDown(1))
        {
            EventSystem.current.SetSelectedGameObject(null);
            EventSystem.current.SetSelectedGameObject(EventSystem.current.firstSelectedGameObject);
            //Debug.Log("Pressed right click.");
        }


        if (Input.GetMouseButtonDown(2))
        {
            EventSystem.current.SetSelectedGameObject(null);
            EventSystem.current.SetSelectedGameObject(EventSystem.current.firstSelectedGameObject);
            Debug.Log("Pressed middle click.");
        }
    }

}
