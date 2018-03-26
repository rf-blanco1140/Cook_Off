using UnityEngine;
using UnityEditor;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using System;

public class IngredientLoader : MonoBehaviour
{
    public List<POIngrediente> ingredientes;

    SimpleObjectPool poolIngredientes;

	void Start()
	{
        //poolIngredientes = GetComponent<SimpleObjectPool>();
		//LeerData();
	}
    
    public List<POIngrediente> setUpAndRun()
    {
        poolIngredientes = GameObject.Find("ButtonObjectPool").GetComponent<SimpleObjectPool>();
        LeerData();

        return ingredientes;
    }

    public void LeerData()
    {
        string path = "Assets/Resources/ingredients.txt";

        StreamReader reader = new StreamReader(path); 

        //El formato es:
        //nombre;dulce:0;salado:0;amargo:0;acido:0;umami:0;crujiente:true;humedo:true;seco:true;suave:true
        string lineaActual = reader.ReadLine();

        while(lineaActual != null)
        {
            string[] atributos = lineaActual.Split(new string[] { ";", ":" }, StringSplitOptions.None);
            
            string nombre = atributos[0];
            float dulce = float.Parse(atributos[2]);
            float salado = float.Parse(atributos[4]);
            float amargo = float.Parse(atributos[6]);
            float acido = float.Parse(atributos[8]);
            float umami = float.Parse(atributos[10]);
            bool crujiente = atributos[12].Equals("true");
            bool humedo = atributos[14].Equals("true");
            bool seco = atributos[16].Equals("true");
            bool suave = atributos[18].Equals("true");

            POIngrediente temp = poolIngredientes.GetObject().GetComponent<POIngrediente>();
            temp.definirNombre(nombre);
            temp.configurarSabor(dulce, salado, amargo, acido, umami);
            temp.configurarTextura(suave, crujiente, humedo, seco);
            ingredientes.Add(temp);
            poolIngredientes.ReturnObject(temp.gameObject);
            lineaActual = reader.ReadLine();
        }
        reader.Close();
    }

}