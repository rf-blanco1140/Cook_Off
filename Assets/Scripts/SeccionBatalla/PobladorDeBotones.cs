using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PobladorDeBotones : MonoBehaviour
{

    public SimpleObjectPool buttonObjectPool;

    public Transform contentPanel;


    // Use this for initialization
    void Start ()
    {
		
	}

    public void addButtons(List<POIngrediente> listaIngredientes)
    {
        for (int i = 0; i < listaIngredientes.Count; i++)
        {
            POIngrediente esteIngredeinte = listaIngredientes[i];       // Saca el PO de la lista
            GameObject newButton = buttonObjectPool.GetObject();        // Saca un boton del object pool
            newButton.transform.SetParent(contentPanel);                // Agrega el panel como padre del boton

            POIngrediente ingreStats = newButton.GetComponent<POIngrediente>();     // Saca la referencia del ingrediente para incializarle los stats
            ingreStats.configurarSabor(esteIngredeinte.darSabor(POIngrediente.Sabor.Dulce), esteIngredeinte.darSabor(POIngrediente.Sabor.Salado), esteIngredeinte.darSabor(POIngrediente.Sabor.Amargo), esteIngredeinte.darSabor(POIngrediente.Sabor.Acido), esteIngredeinte.darSabor(POIngrediente.Sabor.Umami));
            ingreStats.configurarTextura(esteIngredeinte.darTextura(POIngrediente.Textura.Suave), esteIngredeinte.darTextura(POIngrediente.Textura.Crujiente), esteIngredeinte.darTextura(POIngrediente.Textura.Humedo), esteIngredeinte.darTextura(POIngrediente.Textura.Seco));
            ingreStats.definirNombre(esteIngredeinte.getNombre());
        }
    }
}
