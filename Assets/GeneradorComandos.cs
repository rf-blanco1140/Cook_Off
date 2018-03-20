using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeneradorComandos : MonoBehaviour {

	SimpleObjectPool pool;

	public Transform inicio;

	public Transform fin;

	public Comando[] comandos;

	public float delayComandos = 0.5f;
	// Use this for initialization
	void Start () {
		pool = GetComponent<SimpleObjectPool>();

		foreach(Comando com in comandos)
		{
			com.configurar(KeyCode.UpArrow);	
		}

		StartCoroutine("Reproducir");


	}
	
	// Update is called once per frame
	void Update () {
		
	}

	private IEnumerator Reproducir()
	{
		foreach(Comando com in comandos)
		{
			GameObject temp = pool.GetObject();
			temp.transform.SetParent(this.transform);
			Comando actual = temp.GetComponent<Comando>();
			actual.cuadrarPool(pool);
			actual.configurarPuntos(inicio.position, fin.position);
			actual.configurar(com.darTecla());	
			actual.empezar();
			yield return new WaitForSeconds(delayComandos);		
		}
	}
}
