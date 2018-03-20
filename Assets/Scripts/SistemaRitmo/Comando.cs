using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Comando : MonoBehaviour {

	// Tecla asociada al comando
	KeyCode tecla;

	// Pool al que pertenece. Utiliza este objeto al momento de haber servido su propósito
	SimpleObjectPool pool;

	// Posición en la cual inicia su translación
	Vector3 inicio;

	// Posición en la cual termina su translación
	Vector3 fin;

	// Velocidad a la cual ejecuta la translación
	float velocidad = 150f;

	// No se usa por ahora :P
	void Start () {
		
	}

	// Se le asigna la tecla asociada por parámetro
	public void configurar(KeyCode pTecla)
	{
		tecla = pTecla;
	}

	// Se configura el pool al que pertenece, para su momento de desaparecer
	public void cuadrarPool(SimpleObjectPool elPool)
	{
		pool = elPool;
	}

	// Configura los puntos de la translación
	public void configurarPuntos(Vector3 pinicio, Vector3 pfin)
	{
		inicio = pinicio;
		fin = pfin;
	}

	// Retorna el comando asignado
	public KeyCode darTecla()
	{
		return tecla;
	}

	// LLeva el objeto a la posición inicial e inicia la translación
	public void empezar()
	{
		transform.position = inicio;
		StartCoroutine("Mover");
	}

	// Translada el objeto. En caso de terminar la translación, es decir
	// que llega a la posición final, debe retornarse al pool pues no fue presionado
	// por el usuario.
	private IEnumerator Mover()
	{
		Vector3 mov = fin - inicio;
		mov.Normalize ();

		while(transform.position.x > fin.x)
		{
			transform.Translate (mov*Time.deltaTime*velocidad);
			yield return new WaitForSeconds(.01f);
		}
		yield return new WaitForSeconds(0.1f);
		pool.ReturnObject(gameObject);

	}

	// Comando para terminar la translación del objeto y devolverlo al pool.
	// Es responsabilidad del script evaluador determinar si debe terminarse antes
	// de terminar la translación.
	public void terminar()
	{
		StopCoroutine("Mover");
		pool.ReturnObject(gameObject);
	}
}
