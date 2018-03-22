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


    //---------------------------------------------------------------------------
    // Methods
    //---------------------------------------------------------------------------

    // Use this for initialization
    void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
		
	}

    private void OnEnable()
    {
        GameObject[] listado = GameManager.instance.getEnciclopedia();

        for(int i=0; i < listado.Length; i++)
        {
            GameObject nuevoBoton = buttonObjectPool.GetObject();

        }
    }
}
