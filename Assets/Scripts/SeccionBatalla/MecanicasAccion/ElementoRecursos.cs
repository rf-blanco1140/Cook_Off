using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ElementoRecursos : MonoBehaviour
{

    //---------------------------------------------------------------------------
    // Variables
    //---------------------------------------------------------------------------

    private bool estaSeleccionado;

    private Color colorOriginal;

    //---------------------------------------------------------------------------
    // Methods
    //---------------------------------------------------------------------------

    // Use this for initialization
    void Start ()
    {
        colorOriginal = GetComponent<Button>().colors.normalColor;

    }
	
    // Maneja el evento de presionar un boton de recurso
    // si el recurso no ha sido adicionado se adiciona y si a sido adicionado lo saca
    public void manejarSeleccionBotonRecurso()
    {
        if(estaSeleccionado)
        {
            Accion.instance.retirarDeIngredientesParaAccion(this.gameObject);
            estaSeleccionado = false;
            unHiligtButton();
        }
        else
        {
            int pos = this.GetComponent<BotonRecurso>().posicionDeRecurso();  //GameManager.instance.getInventario().IndexOf(this.gameObject.GetComponent<POIngrediente>());
            //Debug.Log("la pos es "+ pos);
            Accion.instance.agregarIngredeintesParaAccion(this.gameObject, pos);
            estaSeleccionado = true;
            hiligtButton();
        }

        reaccionEnInterfaz();
    }

    public void deseleccionarEsteElemento()
    {
        estaSeleccionado = false;
    }

    public void reaccionEnInterfaz()
    {

    }
    public void hiligtButton()
    {
        Color newColor = new Color(0, 1, 0);
        ColorBlock colorBlck = GetComponent<Button>().colors;
        //colorBlck.highlightedColor = newColor;
        colorBlck.normalColor = newColor;
        GetComponent<Button>().colors = colorBlck;
    }

    public void unHiligtButton()
    {
        ColorBlock colorBlck = GetComponent<Button>().colors;
        colorBlck.normalColor = colorOriginal;
        GetComponent<Button>().colors = colorBlck;
    }

}
