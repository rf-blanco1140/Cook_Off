using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class BotonIngrediente : MonoBehaviour, ISelectHandler
{

    public Text nombreBoton;

    public Image imagenIngrediente;

    private POIngrediente ingredienteStats;

    public Text dulce;
    public Text salado;
    public Text amargo;
    public Text acido;
    public Text umami;
    public Text crujiente;
    public Text humedo;
    public Text seco;
    public Text suave;




    // Use this for initialization
    void Start ()
    {
        GameObject dulceObject = GameObject.Find("Dulce");
        dulce = dulceObject.GetComponent<Text>();
        salado = GameObject.Find("Salado").GetComponent<Text>();
        amargo = GameObject.Find("Amargo").GetComponent<Text>();
        acido = GameObject.Find("Acido").GetComponent<Text>();
        umami = GameObject.Find("Umami").GetComponent<Text>();
        crujiente = GameObject.Find("Crujiente").GetComponent<Text>();
        humedo = GameObject.Find("Humedo").GetComponent<Text>();
        seco = GameObject.Find("Seco").GetComponent<Text>();
        suave = GameObject.Find("Suave").GetComponent<Text>();
    }

    // Agrega los valores pasados al boton
    public void inicializarValoresBoton(GameObject objetoIngrediente)
    {
        setUpRefenrenciasBoton();

        ingredienteStats = objetoIngrediente.GetComponent<POIngrediente>();

        nombreBoton.text = ingredienteStats.getNombre();
    }

    // Guarda las refencias del canvas 
    public void setUpRefenrenciasBoton()
    {
        GameObject dulceObject = GameObject.Find("Dulce");
        dulce = dulceObject.GetComponent<Text>();
    }

    // Agrega los stats del ingrediente en el panel del canvas de stats en base a la info
    // guardada en el ingrediente
    public void agregarStatsEnPanel()
    {
        //Agrega todos los valores de las caracteristicas al texto en inetrfaz
        //Sabores
        dulce.text += " " + ingredienteStats.darSabor(POIngrediente.Sabor.Dulce);
        salado.text += " " + ingredienteStats.darSabor(POIngrediente.Sabor.Salado);
        amargo.text += " " + ingredienteStats.darSabor(POIngrediente.Sabor.Amargo);
        acido.text += " " + ingredienteStats.darSabor(POIngrediente.Sabor.Acido);
        umami.text += " " + ingredienteStats.darSabor(POIngrediente.Sabor.Umami);
        // Texturas
        crujiente.text += " " + ingredienteStats.darTextura(POIngrediente.Textura.Crujiente);
        suave.text += " " + ingredienteStats.darTextura(POIngrediente.Textura.Suave);
        humedo.text += " " + ingredienteStats.darTextura(POIngrediente.Textura.Humedo);
        seco.text += " " + ingredienteStats.darTextura(POIngrediente.Textura.Seco);
    }

    // Limpia la info escrita en el panel de stats
    public void limpiarPanelStats()
    {
        dulce.text = "Dulce:";
        salado.text = "Salado:";
        amargo.text = "Amargo:";
        acido.text = "Acido:";
        umami.text = "Umami:";
        crujiente.text = "Crujiente:";
        suave.text = "Suave:";
        humedo.text = "Humedo:";
        seco.text = "Seco:";
    }

    public void OnSelect(BaseEventData eventData)
    {
        limpiarPanelStats();
        agregarStatsEnPanel();
    }
}
