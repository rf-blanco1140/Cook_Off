using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SistemaDePuntos : MonoBehaviour {

    public GameObject panelPuntaje;

    float puntaje;

    float timer = 60;

    public static SistemaDePuntos instance = null;

    bool terminado = false;

    Text textoPuntaje;

    Text textoTimer;

    string resultado = "";

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

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

        textoPuntaje = GameObject.Find("Puntaje").GetComponent<Text>();
        textoTimer = GameObject.Find("Timer").GetComponent<Text>();
        StartCoroutine("CorrerTiempo");
    }

    public void aumentarPuntaje(float valor)
    {
        puntaje += valor;
        actualizarPuntaje();
    }

    public void evaluarPlatoFinal(List<POIngrediente> ingredientesServidos)
    {
        terminado = true;
        int cantidadIngredientes = 0;
        int sinCortar = 0;
        int sinCocinar = 0;
        float dulce = 0;
        float acido = 0;
        float amargo = 0;
        float salado = 0;
        float umami = 0;
        bool crujiente = false;
        bool suave = false;
        bool humedo = false;
        bool seco = false;

        for(int i=0;i<ingredientesServidos.Count;i++)//foreach(POIngrediente ingrediente in ingredientesServidos)
        {
            POIngrediente ingrediente = ingredientesServidos[i];

            cantidadIngredientes += ingrediente.contarIngredientes();

            sinCortar += ingrediente.fueCortado();
            sinCocinar += ingrediente.fueCocinado();

            dulce += ingrediente.darSabor(POIngrediente.Sabor.Dulce);
            acido += ingrediente.darSabor(POIngrediente.Sabor.Acido);
            amargo += ingrediente.darSabor(POIngrediente.Sabor.Amargo);
            salado += ingrediente.darSabor(POIngrediente.Sabor.Salado);
            umami += ingrediente.darSabor(POIngrediente.Sabor.Umami);

            crujiente = crujiente || ingrediente.darTextura(POIngrediente.Textura.Crujiente);
            suave = suave || ingrediente.darTextura(POIngrediente.Textura.Suave);
            humedo = humedo || ingrediente.darTextura(POIngrediente.Textura.Humedo);
            seco = seco || ingrediente.darTextura(POIngrediente.Textura.Seco);

        }
        dulce = dulce * 10;
        amargo = amargo * 10;
        salado = salado * 10;
        acido = acido * 10;
        umami = umami * 10;

        float balance = (dulce + acido + amargo + salado + umami);
        float bonusCrujiente = 0;
        float bonusSuave = 0;
        float bonusHumedo = 0;
        float bonusSeco = 0;

        if(suave)
        {
            bonusSuave = 200;
        }
        if(crujiente)
        {
            bonusCrujiente = 200;
        }
        if(humedo)
        {
            bonusHumedo = 200;
        }
        if(seco)
        {
            bonusSeco = 200;
        }

        float total = puntaje +
                      acido + amargo + dulce + salado + umami - balance +
                      bonusSeco + bonusSuave + bonusHumedo + bonusCrujiente
                      - sinCortar * 50 - sinCocinar * 50;

        resultado = "RESULTADO: \n" +
            "--------------------------- \n" +
            "Aciertos acciones: +" + puntaje + "pts \n" +
            "--------------------------- \n" +
            "Ingredientes sin cortar (" + sinCortar + "/" + cantidadIngredientes + "): -" + (sinCortar*50) + "pts \n" +
            "Ingredientes sin cocinar (" + sinCocinar + "/" + cantidadIngredientes + "): -" + (sinCocinar * 50) + "pts \n" +
            "--------------------------- \n" +
            "Sabor ácido: +" + acido + "pts \n" +
            "Sabor amargo: +" + amargo + "pts \n" +
            "Sabor dulce: +" + dulce + "pts \n" +
            "Sabor salado: +" + salado + "pts \n" +
            "Sabor umami: +" + umami + "pts \n" +
            "Desbalance entre sabores: -" + balance + "pts \n" +
            "--------------------------- \n" +
            "Bonus por textura crujiente: +" + bonusCrujiente + "pts \n" +
            "Bonus por textura suave: +" + bonusSuave + "pts \n" +
            "Bonus por textura humedo: +" + bonusHumedo + "pts \n" +
            "Bonus por textura seco: +" + bonusSeco + "pts \n" +
            "--------------------------- \n" +
            "TOTAL = " + total + " pts.";

        panelPuntaje.SetActive(true);

        GameObject.Find("TextEditable").GetComponent<Text>().text = puntaje+"";
        GameObject.Find("TextEditable (1)").GetComponent<Text>().text = sinCortar + "";
        GameObject.Find("TextEditable (17)").GetComponent<Text>().text = cantidadIngredientes + "";
        GameObject.Find("TextEditable (3)").GetComponent<Text>().text = (sinCortar * 50) + "";
        GameObject.Find("TextEditable (2)").GetComponent<Text>().text = sinCocinar + "";
        GameObject.Find("TextEditable (16)").GetComponent<Text>().text = cantidadIngredientes + "";
        GameObject.Find("TextEditable (4)").GetComponent<Text>().text = (sinCocinar * 50) + "";
        GameObject.Find("TextEditable (5)").GetComponent<Text>().text = acido + "";
        GameObject.Find("TextEditable (6)").GetComponent<Text>().text = amargo + "";
        GameObject.Find("TextEditable (7)").GetComponent<Text>().text = dulce + "";
        GameObject.Find("TextEditable (8)").GetComponent<Text>().text = salado + "";
        GameObject.Find("TextEditable (9)").GetComponent<Text>().text = umami + "";
        GameObject.Find("TextEditable (10)").GetComponent<Text>().text = balance + "";
        GameObject.Find("TextEditable (11)").GetComponent<Text>().text = bonusCrujiente + "";
        GameObject.Find("TextEditable (12)").GetComponent<Text>().text = bonusSuave + "";
        GameObject.Find("TextEditable (13)").GetComponent<Text>().text = bonusHumedo + "";
        GameObject.Find("TextEditable (14)").GetComponent<Text>().text = bonusSeco + "";
        GameObject.Find("TextEditable (15)").GetComponent<Text>().text = total + "";

        

        Debug.Log(resultado);

    }

    public void actualizarPuntaje()
    {
        textoPuntaje.text = puntaje + "";
    }

    public void actualizarTimer()
    {
        int minutos = (int)timer / 60;
        int segundos = (int) timer % 60;

        textoTimer.text = minutos + ":" + segundos;
    }

    IEnumerator CorrerTiempo()
    {
        while(!terminado && timer > 0)
        {
            timer = timer - 1;
            actualizarTimer();
            yield return new WaitForSeconds(1f);
        }
        if(!terminado)
        {
            evaluarPlatoFinal(BattleMenuManager.instance.darIngredientesServidos());
        }

    }
}
