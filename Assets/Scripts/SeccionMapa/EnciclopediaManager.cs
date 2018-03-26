using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;


public class EnciclopediaManager : MonoBehaviour
{
    //---------------------------------------------------------------------------
    // Variables
    //---------------------------------------------------------------------------

    public SimpleObjectPool buttonObjectPool;

    public List<POIngrediente> listaIngredientes;

    public Transform contentPanel;

    public Button botonEnciclopedia;

    private GameObject primerBotonEnLista;


    //---------------------------------------------------------------------------
    // Methods
    //---------------------------------------------------------------------------

    // Use this for initialization
    void Start ()
    {
        //addButtons();
	}

    private void OnEnable()
    {
        listaIngredientes = GameManager.instance.getEnciclopedia();
        RemoveButtons();
        addButtons();
    }

    private void RemoveButtons()
    {
        while (contentPanel.childCount > 0)
        {
            GameObject toRemove = transform.GetChild(0).gameObject;
            buttonObjectPool.ReturnObject(toRemove);
        }
    }

    /// <summary>
    /// Agrega los ingredientes posibles al panel del la enciclopedia en forma de botones
    /// </summary>
    public void addButtons()
    {
        for(int i=0; i<listaIngredientes.Count; i++)
        {

            Debug.Log("nuevo nombre es: " + listaIngredientes[i].getNombre());
            POIngrediente esteIngredeinte = listaIngredientes[i];
            GameObject newButton = buttonObjectPool.GetObject();
            newButton.transform.SetParent(contentPanel);

            POIngrediente ingreStats = newButton.GetComponent<POIngrediente>();
            ingreStats.configurarSabor(esteIngredeinte.darSabor(POIngrediente.Sabor.Dulce), esteIngredeinte.darSabor(POIngrediente.Sabor.Salado), esteIngredeinte.darSabor(POIngrediente.Sabor.Amargo), esteIngredeinte.darSabor(POIngrediente.Sabor.Acido), esteIngredeinte.darSabor(POIngrediente.Sabor.Umami));
            ingreStats.configurarTextura(esteIngredeinte.darTextura(POIngrediente.Textura.Suave), esteIngredeinte.darTextura(POIngrediente.Textura.Crujiente), esteIngredeinte.darTextura(POIngrediente.Textura.Humedo), esteIngredeinte.darTextura(POIngrediente.Textura.Seco));
            ingreStats.definirNombre(esteIngredeinte.getNombre());

            BotonIngrediente nuevoElementoMenu = newButton.GetComponent<BotonIngrediente>();
            nuevoElementoMenu.inicializarValoresBoton(newButton);

            setUpElementoMenu(newButton);
            if (primerBotonEnLista==null) { primerBotonEnLista = newButton; }
        }

        // Selecciona el primer boton
        EventSystem.current.SetSelectedGameObject(null);
        StartCoroutine(waitToSelect());
    }

    // iniciliza el componente ElementoMenuMapa de los botones ne la enciclopedia
    public void setUpElementoMenu(GameObject nuevoBoton)
    {
        ElementoMenuMapa esteElementomenu = nuevoBoton.GetComponent<ElementoMenuMapa>();
        esteElementomenu.padre = botonEnciclopedia;
        esteElementomenu.panelMio = GameObject.Find("ListaCompletaIngredientes");
    }



    //------------------------------------------------------------------------------------------
    // Corrutina para que se pueda seleccionar el primer boton apenas se abre el menu
    public IEnumerator waitToSelect()
    {
        yield return new WaitForEndOfFrame();
        EventSystem.current.SetSelectedGameObject(primerBotonEnLista);
    }
}
