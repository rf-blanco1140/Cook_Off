using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class EnciclopediaManager : MonoBehaviour
{
    //---------------------------------------------------------------------------
    // Variables
    //---------------------------------------------------------------------------

    public SimpleObjectPool buttonObjectPool;

    public List<string> listaIngredientes;

    public Transform contentPanel;


    //---------------------------------------------------------------------------
    // Methods
    //---------------------------------------------------------------------------

    // Use this for initialization
    void Start ()
    {
        addButtons();
	}

    private void OnEnable()
    {
        /*GameObject[] listado = GameManager.instance.getEnciclopedia();

        for(int i=0; i < listado.Length; i++)
        {
            GameObject nuevoBoton = buttonObjectPool.GetObject();
        }*/
    }

    public void addButtons()
    {
        for(int i=0; i<listaIngredientes.Count; i++)
        {
            string nombreIngrediente = listaIngredientes[i];
            GameObject newButton = buttonObjectPool.GetObject();
            newButton.transform.SetParent(contentPanel);

            ElementoMenuMapa nuevoElementoMenu = newButton.GetComponent<ElementoMenuMapa>();
            nuevoElementoMenu.inicializarValoresBoton(nombreIngrediente);
        }
    }

    public void getListaIngredientes()
    {
        //listaIngredientes
    }
}
