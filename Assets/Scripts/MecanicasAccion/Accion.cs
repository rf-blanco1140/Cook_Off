using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Accion : MonoBehaviour
{

    //---------------------------------------------------------------------------
    // Variables
    //---------------------------------------------------------------------------

    private bool estaSeleccionada;

    private BattleMenuManager instanciaBMManager;

    private GameObject[] ingredeintesQueUsara;

    private int posEnListaIngredeintesQueUsara;


    //---------------------------------------------------------------------------
    // Methods
    //---------------------------------------------------------------------------

    // Use this for initialization
    void Start ()
    {
        estaSeleccionada = false;
        instanciaBMManager = BattleMenuManager.instance;
        ingredeintesQueUsara = new GameObject[instanciaBMManager.getIngredeintesUsables().Length];
        posEnListaIngredeintesQueUsara = 0;
	}

    // Selecciona los ingredientes que se van a usar en cada accion
    public void agregarIngredeintesParaAccion(GameObject objetoIngredeinte)
    {
        ingredeintesQueUsara[posEnListaIngredeintesQueUsara] = objetoIngredeinte;
        posEnListaIngredeintesQueUsara++;
    }

    public void retirarIngredientesParaAccion()
    {
        for(int i=0; i < (posEnListaIngredeintesQueUsara+1); i++)
        {
            if()
            {

            }
        }
    }
}
