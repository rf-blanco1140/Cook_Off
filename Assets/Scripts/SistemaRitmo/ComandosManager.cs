using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComandosManager : MonoBehaviour
{

    public static ComandosManager instance = null;

    private int comandosActualesTotales;




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

    public void revisarSiTerminarSistemaRitmo(int comandosDetectados)
    {
        if(comandosDetectados == comandosActualesTotales)
        {
            BattleMenuManager.instance.terminarSistemaRitmo();
        }
    }
}
