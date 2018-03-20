using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElementoRecursos : MonoBehaviour
{

    //---------------------------------------------------------------------------
    // Variables
    //---------------------------------------------------------------------------

    private bool estaSeleccionado;


    //---------------------------------------------------------------------------
    // Methods
    //---------------------------------------------------------------------------

    // Use this for initialization
    void Start ()
    {
		
	}
	
    // Maneja el evento de presionar un boton de recurso
    // si el recurso no ha sido adicionado se adiciona y si a sido adicionado lo saca
    public void manejarSeleccionBotonRecurso()
    {
        if(estaSeleccionado)
        {
            Accion.instance.retirarDeIngredientesParaAccion(this.gameObject);
            estaSeleccionado = false;
        }
        else
        {
            Accion.instance.agregarIngredeintesParaAccion(this.gameObject);
            estaSeleccionado = true;
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

}
