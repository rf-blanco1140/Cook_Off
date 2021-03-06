﻿using System.Collections;
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
    }

    // Use this for initialization
    void Start()
    {
        //Disapear mouse cousor
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        objetoPadreRecursosDisponibles = GameObject.Find("ListaCompletaIngredientes");
        peleaRitmoReference = GameObject.Find("PeleaRitmo");

        //listaTotalIngredientes = new List<GameObject>();
        if (objetoPadreRecursosDisponibles.transform.GetChild(0) == null) { Debug.Log("objetos padre es nulo"); }
        
        objetoPadreRecursosDisponibles.SetActive(false);
        peleaRitmoReference.SetActive(false);

        //Accion.instance.inicializarListas();
    }

    // Metodo que guarda la ultima accion seleccionada
    public void seleccionarAccion(GameObject pAccionSeleccionada)
    {
        accionSeleccionada = pAccionSeleccionada;
        string nombreAcc = accionSeleccionada.GetComponentInChildren<Text>().text;
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
        accionSeleccionada = GameObject.Find("Acciones").gameObject.transform.GetChild(0).gameObject;
    }

    /// <summary>
    /// Deja la subaccion seleccionada como null
    /// esto indica que ene ste momento no se esta seleccionando una subaccion
    /// </summary>
    public void limpiarSubAccionSeleccionada()
    {
        subAccionSeleccionada = null;
    }

    public void cerrarSubPaneles()
    {
        GameObject.Find("Cocinar").gameObject.SetActive(false);
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
    /*public void definirIngredientesUsables(string accionDefinida)
    {
        for (int i = 0; i < listaTotalIngredientes.Count; i++)
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
                // Siempre se muestran todos
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
    }*/

    public List<GameObject> getIngredeintesUsables()
    {
        return null;
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

    // Activa el sistema de ritmo en pantalla para que corra los comandos definidos previamente
    public void lanzarSistemaRitmo()
    {
        peleaRitmoReference.SetActive(true);
    }

    // Desactiva el sistema de ritmo y vuelve a la ultima accion seleccionada
    public void terminarSistemaRitmo()
    {
        peleaRitmoReference.SetActive(false);
        Accion.instance.vaciarListaRecursosActuales();
        EventSystem.current.SetSelectedGameObject(backToLastOpcionSelected());
    }

    // Devuelve la subaccion que se ha seleccioando
    public GameObject getSubaccionSeleccionada()
    {
        return subAccionSeleccionada;
    }

    // Mezcla los ingredientes que fueron seleccionados en la acción de mezclar
    public void mezclarIngredientesListos()
    {
        Debug.Log("A_Mezclar");
        //List<GameObject> listaIngreEnAccion = Accion.instance.getListaIngredeintesEnAccion();
        List<int> posIngredeintes = Accion.instance.getPosicionIngredeintesEnAccion();
        for (int i = 1; i < posIngredeintes.Count; i++)//ingredientesListosParaUsar.Count; i++)
        {
            IngredientLoader.instance.mezclarIngrediente(posIngredeintes[0], posIngredeintes[i]);
        }
    }

    //!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
    //  Este es el metodo que realiza las acciones cuando se presenta el plato a los jurados
    //!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
    public void presentarPlatoJurados()
    {
        SistemaDePuntos.instance.evaluarPlatoFinal(darIngredientesServidos());
    }

    public List<POIngrediente> darIngredientesServidos()
    {
        
        List<POIngrediente> servidos = new List<POIngrediente>();
        for(int i=0; i<IngredientLoader.instance.darIngredientes().Count;i++)//foreach(GameObject ingrediente in listaTotalIngredientes)
        {
            //POIngrediente elIngrediente = ingrediente.GetComponent<POIngrediente>();
            if(IngredientLoader.instance.darIngredienteIndice(i).estaServido())
            {
                servidos.Add(IngredientLoader.instance.darIngredienteIndice(i));
            }
        }
        return servidos;
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
            //.Log("Pressed middle click.");
        }
    }

}
