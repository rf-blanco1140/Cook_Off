﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Accion : MonoBehaviour
{

    //---------------------------------------------------------------------------
    // Variables
    //---------------------------------------------------------------------------

    public static Accion instance = null;

    private List<int> posIngredeintesEnAccion;

    private List<GameObject> ingredeintesQueUsara;

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
    }

    // Use this for initialization
    void Start ()
    {
        posEnListaIngredeintesQueUsara = 0;
        ingredeintesQueUsara = new List<GameObject>();
        posIngredeintesEnAccion = new List<int>();
	}

    public List<GameObject> getListaIngredeintesEnAccion()
    {
        return ingredeintesQueUsara;
    }

    public List<int> getPosicionIngredeintesEnAccion()
    {
        return posIngredeintesEnAccion;
    }

    // Devuelve el numero de ingredientes que se van a ausar en la subaccion
    public int getNumeroDeIngredientes()
    {
        return ingredeintesQueUsara.Count; //posEnListaIngredeintesQueUsara;
    }

    // Selecciona los ingredientes que se van a usar en cada accion
    public void agregarIngredeintesParaAccion(GameObject objetoIngredeinte, int posicion)
    {
        posIngredeintesEnAccion.Add(posicion);
        ingredeintesQueUsara.Add(objetoIngredeinte);
        posEnListaIngredeintesQueUsara++;

        //imprimirObjetosEnLista();
    }

    // Retira el elemento de la lista pedido y se reorganiza la lisat de ingredientes a usar
    public void retirarDeIngredientesParaAccion(GameObject objetoRetirar)
    {
        bool loEncontro = false;
        string nombreObjeto = objetoRetirar.name;

        for(int i=0; !loEncontro && (i < (posEnListaIngredeintesQueUsara)); i++)
        {
            if (nombreObjeto == ingredeintesQueUsara[i].name)
            {
                ingredeintesQueUsara.RemoveAt(i);
                posEnListaIngredeintesQueUsara--;
            }
        }

        //imprimirObjetosEnLista();
    }

    // Vacia la lista y retorna el indice de la posisicon actual a 0
    public void vaciarListaRecursosActuales()
    {
        Debug.Log("tamaño lista es: "+ingredeintesQueUsara.Count);
        for (int i = 0; i < ingredeintesQueUsara.Count ;i++)//posEnListaIngredeintesQueUsara; i++)
        {
            GameObject objetoBorrar = ingredeintesQueUsara[i];
            ElementoRecursos elem = objetoBorrar.GetComponent<ElementoRecursos>();
            elem.deseleccionarEsteElemento();

            ingredeintesQueUsara.RemoveAt(i); //ingredeintesQueUsara[i] = null;
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
