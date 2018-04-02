using UnityEngine;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using System;

public class IngredientLoader : MonoBehaviour
{
    public List<POIngrediente> ingredientes;

    SimpleObjectPool poolIngredientes;

    public static IngredientLoader instance = null;

    //Awake is always called before any Start functions
    void Awake()
    {
        //Check if instance already exists
        if (instance == null)

            //if not, set instance to this
            instance = this;

        //If instance already exists and it's not this:
        else if (instance != this)

            //Then destroy this. This enforces our singleton pattern, meaning there can only ever be one instance of a GameManager.
            Destroy(gameObject);

        

    }

    void Start()
	{
        setUpAndRun();
    }

    private void setUpAndRun()
    {
        poolIngredientes = GetComponent<SimpleObjectPool>();
        LeerData();
    }

    private void LeerData()
    {
        //string path = "Assets/Resources/ingredients.txt";

        //StreamReader reader = new StreamReader(path); 
        string[] ing = new string[10];
        ing[0] = "salmón; dulce: 0; salado: 5; amargo: 2; acido: 0; umami: 3; crujiente: false; humedo: true; seco: false; suave: true; requiereCorte: true; requiereCoccion: false";
        ing[1] = "limón; dulce: 0; salado: 0; amargo: 3; acido: 7; umami: 1; crujiente: false; humedo: true; seco: false; suave: false; requiereCorte: true; requiereCoccion: false";
        ing[2] = "ajo; dulce: 0; salado: 0; amargo: 7; acido: 3; umami: 0; crujiente: true; humedo: true; seco: false; suave: false; requiereCorte: true; requiereCoccion: true";
        ing[3] = "tomate; dulce: 3; salado: 0; amargo: 0; acido: 1; umami: 1; crujiente: false; humedo: true; seco: false; suave: true; requiereCorte: true; requiereCoccion: true";
        ing[4] = "cebolla; dulce: 0; salado: 0; amargo: 3; acido: 7; umami: 1; crujiente: true; humedo: false; seco: false; suave: false; requiereCorte: true; requiereCoccion: true";
        ing[5] = "sal; dulce: 0; salado: 6; amargo: 0; acido: 0; umami: 1; crujiente: false; humedo: false; seco: true; suave: false; requiereCorte: false; requiereCoccion: false";
        ing[6] = "pimienta; dulce: 0; salado: 3; amargo: 2; acido: 2; umami: 1; crujiente: false; humedo: false; seco: true; suave: false; requiereCorte: false; requiereCoccion: false";
        ing[7] = "huevo; dulce: 0; salado: 0; amargo: 0; acido: 0; umami: 3; crujiente: false; humedo: true; seco: false; suave: true; requiereCorte: false; requiereCoccion: true";
        ing[8] = "leche; dulce: 3; salado: 0; amargo: 0; acido: 0; umami: 1; crujiente: false; humedo: true; seco: false; suave: false; requiereCorte: false; requiereCoccion: true";
        ing[9] = "queso; dulce: 0; salado: 3; amargo: 0; acido: 0; umami: 3; crujiente: false; humedo: true; seco: false; suave: true; requiereCorte: true; requiereCoccion: true";

        //El formato es:
        //nombre;dulce:0;salado:0;amargo:0;acido:0;umami:0;crujiente:true;humedo:true;seco:true;suave:true
        int i = 0;

        while(i < 10)
        {
            string lineaActual = ing[i];
            lineaActual = lineaActual.Replace(" ", "");

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
            bool corte = atributos[20].Equals("true");
            bool coccion = atributos[22].Equals("true");

            POIngrediente temp = poolIngredientes.GetObject().GetComponent<POIngrediente>();
            temp.transform.parent = gameObject.transform;
            temp.definirNombre(nombre);
            temp.configurarSabor(dulce, salado, amargo, acido, umami);
            temp.configurarTextura(suave, crujiente, humedo, seco);
            temp.configurar(corte, coccion);
            ingredientes.Add(temp);
            //lineaActual = reader.ReadLine();
            i++;
        }
        //reader.Close();
    }

    public List<POIngrediente> darIngredientes()
    {
        return ingredientes;
    }

    public POIngrediente darIngredienteIndice(int indice)
    {
        return ingredientes[indice];
    }

    public POIngrediente darIngredienteNombre(string nombre)
    {
        foreach (POIngrediente ingrediente in ingredientes)
        {
            if (ingrediente.getNombre().StartsWith(nombre))
            {
                return ingrediente;
            }
        }
        return null;
    }

    public void mezclarIngrediente(int posPrimario, int posSecundario)
    {
        ingredientes[posPrimario].mezclar(ingredientes[posSecundario]);
        ingredientes.RemoveAt(posSecundario);
    }

}