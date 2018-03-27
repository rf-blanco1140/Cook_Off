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

            // decirle cual es su elemento de arriba
            // Si es el primero el elemento de arriba es el ultimo
            //Decirle cual es su elemento de abajo
            //Si es el ultimo el elemento de abajo es el primero
            Button esteBoton = newButton.GetComponent<Button>();
            Navigation btnNav = esteBoton.navigation;
            if(i == (listaIngredientes.Count-1) && i>0)
            {
                //Debug.Log("esta en el final");

                // definir arriba del primero
                Navigation newNav = new Navigation();
                newNav.mode = Navigation.Mode.Explicit;
                newNav.selectOnDown = contentPanel.GetChild(1).GetComponent<Button>();
                newNav.selectOnUp = contentPanel.GetChild(i).GetComponent<Button>();
                
                contentPanel.GetChild(0).gameObject.GetComponent<Button>().navigation = newNav;


                // definir abajo del ultimo
                newNav.selectOnDown = contentPanel.GetChild(0).GetComponent<Button>();
                newNav.selectOnUp = contentPanel.GetChild(i - 1).GetComponent<Button>();

                contentPanel.GetChild(i).gameObject.GetComponent<Button>().navigation = newNav;
            }
            else if(i == (listaIngredientes.Count - 1) && i == 0)
            {

            }
            else if(i>0)
            {
                // navegacion del elementoa actual
                Navigation newNav = new Navigation();
                newNav.mode = Navigation.Mode.Explicit;
                newNav.selectOnUp = contentPanel.GetChild(i-1).GetComponent<Button>();

                contentPanel.GetChild(i).gameObject.GetComponent<Button>().navigation = newNav;


                newNav.mode = Navigation.Mode.Explicit;
                newNav.selectOnDown = contentPanel.GetChild(i).GetComponent<Button>();

                contentPanel.GetChild(i-1).gameObject.GetComponent<Button>().navigation = newNav;
            }

        }

        // Selecciona el primer boton
        primerBotonEnLista = contentPanel.GetChild(0).gameObject;
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

    public void definirNavegacionBotones()
    {

    }

    //------------------------------------------------------------------------------------------
    // Corrutina para que se pueda seleccionar el primer boton apenas se abre el menu
    public IEnumerator waitToSelect()
    {
        yield return new WaitForEndOfFrame();
        EventSystem.current.SetSelectedGameObject(primerBotonEnLista);
    }
}
