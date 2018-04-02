using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class POIngrediente : MonoBehaviour
{

    // Nombre del ingrediente
    private string nombre;

	// Enumerador para los sabores que existen
	public enum Sabor {Amargo, Dulce, Salado, Acido, Umami};

	// Enumerador para las texturas que existen
	public enum Textura {Crujiente, Suave, Humedo, Seco};

	// Valores numéricos que posee el ingrediente para cada uno de los sabores

	float dulce;

	float salado;

	float amargo;

	float acido;

	float umami;

	// Indican las texturas que posee o no el ingrediente

	bool crujiente;

	bool humedo;

	bool seco;

	bool suave;

	bool cortado;

	// Estado en el que se encuentra el ingrediente
	POEstado estado;

	// En caso de tratarse de una mezcla de ingredientes,
	// o del plato terminado, lista de ingredientes que lo componen
	List<POIngrediente> componentes;

    private void Awake()
    {
        if (estado == null) { estado = new POEstado(); }
        componentes = new List<POIngrediente>();
    }
    void Start ()
    {

    }

    // Define el nombre del ingrediente
    public void definirNombre(string pNombre)
    {
        nombre = pNombre;
    }

    // Define el nombre del ingrediente
    public void definirComponentes(List<POIngrediente> pIngredientes)
    {
        foreach(POIngrediente ingredientePos in pIngredientes)
        {
            componentes.Add(ingredientePos);
        }
    }

    public List<POIngrediente> darComponentes()
    {
        return componentes;
    }

    // Retorna el nombre del ingrediente
    public string getNombre()
    {
        string cadena = "";
        if(componentes == null || componentes.Count == 0)
        {
            cadena = nombre;
        }
        else
        {
            cadena = nombre + " (";
            foreach(POIngrediente ingrediente in componentes)
            {
                // LA LINEA DEL DEMONIO
                cadena = cadena + ingrediente.getNombre() + ", ";
            }
            cadena = cadena.Substring(0, cadena.Length - 2);
            cadena += ")";
        }

        //Debug.Log("nombre retornado es "+nombre);
        return cadena;
    }

	// Retorna el valor numérico del sabor especificado. En caso de tratarse de una mezcla,
	// retorna la suma del sabor de todos los ingredientes que componen la mezcla.
	public float darSabor(Sabor pSabor)
	{
		float cantidadSabor = 0;
		
        if(componentes != null)
        {
            foreach (POIngrediente ingredientePos in componentes)
            {
                cantidadSabor += ingredientePos.darSabor(pSabor);
            }
        }
		
		switch(pSabor)
		{
			case Sabor.Acido:
			cantidadSabor += acido; 
			break;
			case Sabor.Dulce:
			cantidadSabor += dulce;
			break;
			case Sabor.Amargo:
			cantidadSabor += amargo;
			break;
			case Sabor.Salado:
			cantidadSabor += salado;
			break;
			case Sabor.Umami:
			cantidadSabor += umami;
			break;         		
			default:
			cantidadSabor += 0;
			break;
		}	

		return cantidadSabor;
	}

	// Retorna si el ingrediente especificado posee la textura especificada.
	// Si es una mezcla, indica si entre todos los ingredientes alguno posee la textura,
	// si uno de los ingredientes posee la textura se infiere que la mezcla o plato la tiene
	public bool darTextura(Textura pTextura)
	{
		bool laTextura = false;
		
		switch(pTextura)
		{
			case Textura.Suave:
				laTextura = suave; 
				break;  
			case Textura.Crujiente:
				laTextura = crujiente; 
				break; 
			case Textura.Seco:
				laTextura = seco; 
				break; 
			case Textura.Humedo:
				laTextura = humedo; 
				break; 			      		
			default:
				break;
		}	

        if(componentes != null)
        {
            foreach (POIngrediente ingredientePos in componentes)
            {
                ingredientePos.darTextura(pTextura);
            }
        }
					
		return laTextura;
	}

	// Permite configurar los valores numéricos de los sabores que aporta el ingrediente
	public void configurarSabor(float pDulce, float pSalado, float pAmargo, float pAcido, float pUmami)
	{
		dulce = pDulce;
		amargo = pAmargo;
		salado = pSalado;
		acido = pAcido;
		umami = pUmami;
	}

	// Permite configurar las texturas que aporta el ingrediente
	public void configurarTextura(bool pSuave, bool pCrujiente, bool pHumedo, bool pSeco)
	{
		suave = pSuave;
		seco = pSeco;
		crujiente = pCrujiente;
		humedo = pHumedo;
	}

	// Adiciona los ingredientes especificados a la lista de componentes
	public void mezclar(POIngrediente pIngredientesPos)
	{
        componentes.Add(pIngredientesPos);
	}

	// Corta el ingrediente, o si es el caso, todos los componentes de la mezcla
	public void cortar()
	{
		estado.cortar();
		foreach(POIngrediente ingredientePos in componentes)
		{
            ingredientePos.cortar();
        }
	}

	// Cocina el ingrediente, o si es el caso, todos los componentes de la mezcla
	public void cocinar()
	{
		estado.cocinar();
		foreach(POIngrediente ingredientePos in componentes)
		{
            ingredientePos.cocinar();
        }
	}

    // Cambia el estado a servido
	public void servir()
	{
		estado.servir();
	}

    // Indica si ha sido servido
    public bool estaServido()
    {
        return estado.fueServido();
    }

	// Indica si el ingrediente o sus componentes fueron cortados. Retorna la cantidad de ingredientes
    // que requieren corte y no fueron cortados, incluyéndolo si es el caso.
	public int fueCortado()
	{
        int cortados = 0;
        if(!estado.fueCortado())
        {
            cortados++;
        }
        foreach (POIngrediente ingredientePos in componentes)
        {
            ingredientePos.fueCortado();
        }
        return cortados;
	}

    // Indica si el ingrediente o sus componentes fueron cocinados. Retorna la cantidad de ingredientes
    // que requieren cocción y no fueron cocinados, incluyéndolo si es el caso.
	public int fueCocinado()
	{
        int cocinados = 0;
        if (!estado.fueCocinado())
        {
            cocinados++;
        }
        foreach (POIngrediente ingredientePos in componentes)
        {
            ingredientePos.fueCocinado();
        }
        return cocinados;
	}

    public int contarIngredientes()
    {
        int cantidad = 0;
        cantidad++;
        foreach (POIngrediente ingredientePos in componentes)
        {
            cantidad += ingredientePos.contarIngredientes();
        }
        return cantidad;
    }

	// Permite configurar los requerimientos de corte y cocción del ingrediente
	public void configurar(bool pCorte, bool pCoccion)
	{
        if (estado==null)
        {
            estado = new POEstado();
        }
		estado.configurar(pCorte, pCoccion);
	}

    public POEstado darEstado()
    {
        return estado;
    }

}
