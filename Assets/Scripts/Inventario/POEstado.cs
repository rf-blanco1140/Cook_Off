using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class POEstado : MonoBehaviour
{

	// Indica si el ingrediente requiere de corte, para la evaluación del plato
	bool requiereCorte;

	// Indica si el ingrediente requiere de cocción, para la evaluación del plato
	bool requiereCoccion;

	// Indica si el ingrediente fue cortado, para la evaluación del plato
	bool cortado;

	// Indica si el ingrediente fue cocinado, para la evaluación del plato
	bool cocinado;

	// Indica si el ingrediente ya fue servido, para la evaluación del plato
	bool servido;

	// En primera instancia, el ingrediente no ha sido cortado, cocinado o servido
	public POEstado ()
    {
		cortado = false;
		cocinado = false;
		servido = false;
	}

	// Indica si el ingrediente requiere de corte y ha sido cortado, o si no requiere
	// de corte en primera instancia.
	// True es el caso favorable, false el caso contrario.
	public bool fueCortado()
	{
		if(!requiereCorte)
		{
			return true;
		}
		else
		{
			return cortado;
		}
	}

	// Indica si el ingrediente requiere de cocción y ha sido cocinado, o si no requiere
	// de cocción en primera instancia.
	// True es el caso favorable, false el caso contrario.
	public bool fueCocinado()
	{
		if(!requiereCoccion)
		{
			return true;
		}
		else
		{
			return cocinado;
		}
	}

	// Permite configurar los requerimientos de corte y cocción del ingrediente
	public void configurar(bool pCorte, bool pCocion)
	{
		requiereCorte = pCorte;
		requiereCoccion = pCocion;
	}

	// Cambia el estado del ingrediente a cortado
	public void cortar()
	{
		cortado = true;
	}

	// Cambia el estado del ingrediente a cocinado
	public void cocinar()
	{
		cocinado = true;
	}

	// Cambia el estado del ingrediente a servido
	public void servir()
	{
		servido = true;
	}

    // Metodo que indica si fue o no servido
    public bool fueServido()
    {
        return servido;
    }
}
