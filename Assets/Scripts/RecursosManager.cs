using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class RecursosManager : MonoBehaviour
{
    public SimpleObjectPool buttonObjectPool;
    public List<POIngrediente> listaIngredientes;
    public Transform contentPanel;
    private GameObject primerBotonEnLista;
    public Button botonPadre;



    private void Start()
    {
        if (GameManager.instance.getInventario() == null) { Debug.Log("es nulo"); }
        if (GameManager.instance == null) { Debug.Log("NO es nulo"); }
        if (GameManager.instance.getInventario() == null || GameManager.instance.getInventario().Count == 0)
        {
            GameManager.instance.llenarInvenatrio();
        }
        poblar();
    }

    private void OnEnable()
    {
        if (GameManager.instance != null && GameManager.instance.getInventario() != null)
        {
            listaIngredientes = GameManager.instance.getInventario();
            RemoveButtons();
            addButtons();
        }
    }

    public void poblar()
    {
        listaIngredientes = GameManager.instance.getInventario();
        RemoveButtons();
        if(listaIngredientes[0].GetComponent<POEstado>() !=null && BattleMenuManager.instance.getAccionSeleccionada().GetComponentInChildren<Text>().text == "Cortar")
        {
            addBotonesParaCortar();
        }
        else if(listaIngredientes[0].GetComponent<POEstado>() != null && BattleMenuManager.instance.getAccionSeleccionada().GetComponentInChildren<Text>().text == "Cocinar")
        {
            addBotonesParaCocinar();
        }
        else
        {
            addButtons();
        }
        
    }

    private void RemoveButtons()
    {
        while (contentPanel.childCount > 0)
        {
            GameObject toRemove = transform.GetChild(0).gameObject;
            buttonObjectPool.ReturnObject(toRemove);
        }
    }

    public void definirNavegacion(int i, GameObject newButton)
    {
        // decirle cual es su elemento de arriba
        // Si es el primero el elemento de arriba es el ultimo
        //Decirle cual es su elemento de abajo
        //Si es el ultimo el elemento de abajo es el primero
        Button esteBoton = newButton.GetComponent<Button>();
        Navigation btnNav = esteBoton.navigation;
        if (i == (listaIngredientes.Count - 1) && i > 0)
        {
            //Debug.Log("esta en el final");

            // definir arriba del primero
            Navigation newNav = new Navigation();
            newNav.mode = Navigation.Mode.Explicit;
            newNav.selectOnDown = contentPanel.GetChild(1).GetComponent<Button>();
            newNav.selectOnUp = contentPanel.GetChild(i).GetComponent<Button>();

            contentPanel.GetChild(0).gameObject.GetComponent<Button>().navigation = newNav;


            // Define el segundo
            newNav = new Navigation();
            newNav.mode = Navigation.Mode.Explicit;
            newNav.selectOnUp = contentPanel.GetChild(0).GetComponent<Button>();
            if (contentPanel.GetChild(2) != null)
            { newNav.selectOnDown = contentPanel.GetChild(2).GetComponent<Button>(); }

            contentPanel.GetChild(1).gameObject.GetComponent<Button>().navigation = newNav;


            // definir abajo del ultimo
            newNav.selectOnDown = contentPanel.GetChild(0).GetComponent<Button>();
            newNav.selectOnUp = contentPanel.GetChild(i - 1).GetComponent<Button>();

            contentPanel.GetChild(i).gameObject.GetComponent<Button>().navigation = newNav;


            // Define el paso de abajo del penultimo
            newNav = new Navigation();
            newNav.mode = Navigation.Mode.Explicit;
            newNav.selectOnDown = contentPanel.GetChild(i).GetComponent<Button>();
            newNav.selectOnUp = contentPanel.GetChild(i - 2).GetComponent<Button>();

            contentPanel.GetChild(i - 1).gameObject.GetComponent<Button>().navigation = newNav;
        }
        else if (i == (listaIngredientes.Count - 1) && i == 0)
        {

        }
        else if (i > 1)
        {
            // navegacion del elementoa actual
            Navigation newNav = new Navigation();
            newNav.mode = Navigation.Mode.Explicit;
            newNav.selectOnUp = contentPanel.GetChild(i - 1).GetComponent<Button>();

            contentPanel.GetChild(i).gameObject.GetComponent<Button>().navigation = newNav;


            newNav = new Navigation();
            newNav.mode = Navigation.Mode.Explicit;
            newNav.selectOnDown = contentPanel.GetChild(i).GetComponent<Button>();
            newNav.selectOnUp = contentPanel.GetChild(i - 2).GetComponent<Button>();

            contentPanel.GetChild(i - 1).gameObject.GetComponent<Button>().navigation = newNav;
        }
    }

    public void addButtons()
    {
        for (int i = 0; i < listaIngredientes.Count; i++)
        {
            POIngrediente esteIngredeinte = listaIngredientes[i];
            GameObject newButton = buttonObjectPool.GetObject();
            newButton.transform.SetParent(contentPanel);

            POIngrediente ingreStats = newButton.GetComponent<POIngrediente>();
            ingreStats.configurarSabor(esteIngredeinte.darSabor(POIngrediente.Sabor.Dulce), esteIngredeinte.darSabor(POIngrediente.Sabor.Salado), esteIngredeinte.darSabor(POIngrediente.Sabor.Amargo), esteIngredeinte.darSabor(POIngrediente.Sabor.Acido), esteIngredeinte.darSabor(POIngrediente.Sabor.Umami));
            ingreStats.configurarTextura(esteIngredeinte.darTextura(POIngrediente.Textura.Suave), esteIngredeinte.darTextura(POIngrediente.Textura.Crujiente), esteIngredeinte.darTextura(POIngrediente.Textura.Humedo), esteIngredeinte.darTextura(POIngrediente.Textura.Seco));
            ingreStats.definirNombre(esteIngredeinte.getNombre());

            BotonRecurso nuevoElementoMenu = newButton.GetComponent<BotonRecurso>();
            nuevoElementoMenu.inicializarValoresBoton(newButton);

            setUpElementoMenu(newButton);

            definirNavegacion(i, newButton);

        }

        // Selecciona el primer boton
        primerBotonEnLista = contentPanel.GetChild(0).gameObject;
        EventSystem.current.SetSelectedGameObject(null);
        StartCoroutine(waitToSelect());
    }

    public void addBotonesParaCortar()
    {
        for (int i = 0; i < listaIngredientes.Count; i++)
        {
            //if (listaIngredientes[i].GetComponent<POEstado>()==null) { Debug.Log("el estado es null"); }
            if(!listaIngredientes[i].GetComponent<POEstado>().fueCortado())
            {
                POIngrediente esteIngredeinte = listaIngredientes[i];
                GameObject newButton = buttonObjectPool.GetObject();
                newButton.transform.SetParent(contentPanel);

                POIngrediente ingreStats = newButton.GetComponent<POIngrediente>();
                ingreStats.configurarSabor(esteIngredeinte.darSabor(POIngrediente.Sabor.Dulce), esteIngredeinte.darSabor(POIngrediente.Sabor.Salado), esteIngredeinte.darSabor(POIngrediente.Sabor.Amargo), esteIngredeinte.darSabor(POIngrediente.Sabor.Acido), esteIngredeinte.darSabor(POIngrediente.Sabor.Umami));
                ingreStats.configurarTextura(esteIngredeinte.darTextura(POIngrediente.Textura.Suave), esteIngredeinte.darTextura(POIngrediente.Textura.Crujiente), esteIngredeinte.darTextura(POIngrediente.Textura.Humedo), esteIngredeinte.darTextura(POIngrediente.Textura.Seco));
                ingreStats.definirNombre(esteIngredeinte.getNombre());

                BotonRecurso nuevoElementoMenu = newButton.GetComponent<BotonRecurso>();
                nuevoElementoMenu.inicializarValoresBoton(newButton);

                setUpElementoMenu(newButton);

                definirNavegacion(i, newButton);
            }
        }

        // Selecciona el primer boton
        primerBotonEnLista = contentPanel.GetChild(0).gameObject;
        EventSystem.current.SetSelectedGameObject(null);
        StartCoroutine(waitToSelect());
    }

    public void addBotonesParaCocinar()
    {
        for (int i = 0; i < listaIngredientes.Count; i++)
        {
            if (!listaIngredientes[i].GetComponent<POEstado>().fueCocinado())
            {
                POIngrediente esteIngredeinte = listaIngredientes[i];
                GameObject newButton = buttonObjectPool.GetObject();
                newButton.transform.SetParent(contentPanel);

                POIngrediente ingreStats = newButton.GetComponent<POIngrediente>();
                ingreStats.configurarSabor(esteIngredeinte.darSabor(POIngrediente.Sabor.Dulce), esteIngredeinte.darSabor(POIngrediente.Sabor.Salado), esteIngredeinte.darSabor(POIngrediente.Sabor.Amargo), esteIngredeinte.darSabor(POIngrediente.Sabor.Acido), esteIngredeinte.darSabor(POIngrediente.Sabor.Umami));
                ingreStats.configurarTextura(esteIngredeinte.darTextura(POIngrediente.Textura.Suave), esteIngredeinte.darTextura(POIngrediente.Textura.Crujiente), esteIngredeinte.darTextura(POIngrediente.Textura.Humedo), esteIngredeinte.darTextura(POIngrediente.Textura.Seco));
                ingreStats.definirNombre(esteIngredeinte.getNombre());

                BotonRecurso nuevoElementoMenu = newButton.GetComponent<BotonRecurso>();
                nuevoElementoMenu.inicializarValoresBoton(newButton);

                setUpElementoMenu(newButton);

                definirNavegacion(i, newButton);
            }
        }

        // Selecciona el primer boton
        primerBotonEnLista = contentPanel.GetChild(0).gameObject;
        EventSystem.current.SetSelectedGameObject(null);
        StartCoroutine(waitToSelect());
    }

    public void setUpElementoMenu(GameObject newButton)
    {
        ElementoMenu esteElementomenu = newButton.GetComponent<ElementoMenu>();
        if (esteElementomenu != null)
        {
            //esteElementomenu.padre = botonPadre;
            esteElementomenu.panelMio = GameObject.Find("ListaCompletaIngredientes");
        }
    }


    //------------------------------------------------------------------------------------------
    // Corrutina para que se pueda seleccionar el primer boton apenas se abre el menu
    public IEnumerator waitToSelect()
    {
        yield return new WaitForEndOfFrame();
        EventSystem.current.SetSelectedGameObject(primerBotonEnLista);
    }

}
