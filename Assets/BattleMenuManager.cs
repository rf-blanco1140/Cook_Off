using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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

    private GameObject accionSeleccionada;

    private GameObject subAccionSeleccionada;


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
        objetoPadreRecursosDisponibles = GameObject.Find("Recursos");
        objetoPadreRecursosDisponibles.SetActive(false);
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

    public GameObject getAccionSeleccionada()
    {
        return accionSeleccionada;
    }

    public GameObject getSubAccionSeleccionada()
    {
        return subAccionSeleccionada;
    }

    public void limpiarAccionSeleccionada()
    {
        accionSeleccionada = null;
    }

    public void limpiarSubAccionSeleccionada()
    {
        subAccionSeleccionada = null;
    }

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

}
