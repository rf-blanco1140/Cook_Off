using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Comando : MonoBehaviour {

	KeyCode tecla;

	SimpleObjectPool pool;

	Vector3 inicio;

	Vector3 fin;

	float velocidad = 150f;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void configurar(KeyCode pTecla)
	{
		tecla = pTecla;
	}

	public void cuadrarPool(SimpleObjectPool elPool)
	{
		pool = elPool;
	}

	public void configurarPuntos(Vector3 pinicio, Vector3 pfin)
	{
		inicio = pinicio;
		fin = pfin;
	}

	public KeyCode darTecla()
	{
		return tecla;
	}

	public void empezar()
	{
		transform.position = inicio;
		StartCoroutine("Mover");
	}

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

	public void terminar()
	{
		StopCoroutine("Mover");
		pool.ReturnObject(gameObject);
	}
}
