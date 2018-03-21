using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComandosManager : MonoBehaviour
{

    public static ComandosManager instance = null;

    private int comandosActualesTotales;

    enum Subacciones { Juliana=0, Chips=1, Cuadros, A_Mano, Asar, Hervir, Hornear, Elegante, Divertido, Sencillo };





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

        //Sets this to not be destroyed when reloading scene
        DontDestroyOnLoad(gameObject);

        comandosActualesTotales = 0;
        Debug.Log("el total es: " + comandosActualesTotales);
    }

    // Use this for initialization
    void Start ()
    {
        
	}

    // Ejecuta el minijuego de ritmo de la subaccion correspondiente
    public void ejecutarSubaccion()
    {
        // Pregunta comparando el nombre de la subaccion seleccionada con los IDs de las subacciones registradas
        // dependiendo del match se ejecuta la subaccion particular
        string nombreSubAccion = BattleMenuManager.instance.getSubaccionSeleccionada().name;

        switch (nombreSubAccion)
        {
            case "Juliana":
                comandosCorteJuliana();
                break;
            case "Chips":
                comandosCorteChips();
                break;
            case "Cuadros":
                comandosCorteCuadros();
                break;
            case "A_Mano":
                comandosCorteJuliana();
                break;
            case "Asar":
                comandosCorteJuliana();
                break;
            case "Hervir":
                comandosCorteJuliana();
                break;
            case "Hornear":
                comandosCorteJuliana();
                break;
            case "Elegante":
                comandosCorteJuliana();
                break;
            case "Divertido":
                comandosCorteJuliana();
                break;
            case "Sencillo":
                comandosCorteJuliana();
                break;
        }

    }

    // ----------- Subacciones -----------------------------------

    // Agrega los comandos a la accion cortar en julianas
    public void comandosCorteJuliana()
    {
        Comando[] newComandos = new Comando[5];

        for (int i=0; i<5; i++)
        {
            Comando temp = new Comando();
            temp.configurar(KeyCode.UpArrow);
            newComandos[i] = temp;
        }

        comandosActualesTotales = newComandos.Length;
        GeneradorComandos.instance.configurarComandos(newComandos);
    }

    // Agrega los comandos a la accion cortar en julianas
    public void comandosCorteChips()
    {
        Comando[] newComandos = new Comando[6];

        for (int i = 0; i < 6; i++)
        {
            Comando temp = new Comando();
            temp.configurar(KeyCode.UpArrow);
            newComandos[i] = temp;
            i++;

            temp = new Comando();
            temp.configurar(KeyCode.RightArrow);
            newComandos[i] = temp;
            i++;

            temp = new Comando();
            temp.configurar(KeyCode.DownArrow);
            newComandos[i] = temp;
        }

        comandosActualesTotales = newComandos.Length;
        GeneradorComandos.instance.configurarComandos(newComandos);
    }

    // Agrega los comandos a la accion cortar en julianas
    public void comandosCorteCuadros()
    {
        Comando[] newComandos = new Comando[4];


            Comando temp = new Comando();
            temp.configurar(KeyCode.LeftArrow);
            newComandos[0] = temp;

            temp = new Comando();
            temp.configurar(KeyCode.UpArrow);
            newComandos[1] = temp;

            temp = new Comando();
            temp.configurar(KeyCode.DownArrow);
            newComandos[2] = temp;

            temp = new Comando();
            temp.configurar(KeyCode.RightArrow);
            newComandos[3] = temp;


        comandosActualesTotales = newComandos.Length;
        GeneradorComandos.instance.configurarComandos(newComandos);
    }


    public void revisarSiTerminarSistemaRitmo(int comandosDetectados)
    {
        if(comandosDetectados == comandosActualesTotales)
        {
            BattleMenuManager.instance.terminarSistemaRitmo();
        }
    }
}
