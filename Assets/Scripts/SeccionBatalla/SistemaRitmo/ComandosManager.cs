using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ComandosManager : MonoBehaviour
{

    public static ComandosManager instance = null;

    private int comandosActualesTotales;

    private GameObject comandoObjetc;

    SimpleObjectPool poolComandos;


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

        comandosActualesTotales = 0;

        comandoObjetc = new GameObject();
        comandoObjetc.AddComponent<Comando>();
        poolComandos = GetComponent<SimpleObjectPool>();
    }

    // Use this for initialization
    void Start ()
    {
        //nuevoComando = new Comando();
    }


    public SimpleObjectPool darPool()
    {
        return poolComandos;
    }
    public void marcarIngredientesComoCortados()
    {
        List<int> posIngredientesAccion = Accion.instance.getPosicionIngredeintesEnAccion();
        for(int i=0; i<posIngredientesAccion.Count;i++)
        {
            Debug.Log("cantidad de ing en acc " + posIngredientesAccion.Count);
            Debug.Log("la pos cambio es " + posIngredientesAccion[i]);
            IngredientLoader.instance.darIngredienteIndice(posIngredientesAccion[i]).cortar();
        }
    }

    public void marcarIngredientesComoCocinado()
    {
        List<int> posIngredientesAccion = Accion.instance.getPosicionIngredeintesEnAccion();
        for (int i = 0; i < posIngredientesAccion.Count; i++)
        {
            Debug.Log("cantidad de ing en acc " + posIngredientesAccion.Count);
            Debug.Log("la pos cambio es " + posIngredientesAccion[i]);
            IngredientLoader.instance.darIngredienteIndice(posIngredientesAccion[i]).cocinar();
        }
    }

    public void marcarIngredientesComoServido()
    {
        List<int> posIngredientesAccion = Accion.instance.getPosicionIngredeintesEnAccion();
        for (int i = 0; i < posIngredientesAccion.Count; i++)
        {
            Debug.Log("cantidad de ing en acc " + posIngredientesAccion.Count);
            Debug.Log("la pos cambio es " + posIngredientesAccion[i]);
            IngredientLoader.instance.darIngredienteIndice(posIngredientesAccion[i]).servir();
        }
    }

    // Ejecuta el minijuego de ritmo de la subaccion correspondiente
    public void ejecutarSubaccion()
    {
        //Debug.Log("ejecuto accion");
        int numIngredeintes = Accion.instance.getNumeroDeIngredientes();
        if (numIngredeintes>0)
        {
            // Pregunta comparando el nombre de la subaccion seleccionada con los IDs de las subacciones registradas
            // dependiendo del match se ejecuta la subaccion particular
            string nombreSubAccion = BattleMenuManager.instance.getSubaccionSeleccionada().name;

            switch (nombreSubAccion)
            {
                case "Juliana":
                    comandosCorteJuliana();
                    marcarIngredientesComoCortados();
                    break;
                case "Chips":
                    comandosCorteChips();
                    marcarIngredientesComoCortados();
                    break;
                case "Cuadros":
                    comandosCorteCuadros();
                    marcarIngredientesComoCortados();
                    break;
                case "A_Mano":
                    comandosMezclaAMano();
                    break;
                case "Asar":
                    BattleMenuManager.instance.limpiarSubAccionSeleccionada();
                    BattleMenuManager.instance.cerrarSubPaneles();
                    marcarIngredientesComoCocinado();
                    //BattleMenuManager.instance.limpiarAccionSeleccionada();
                    comandosCorteJuliana();
                    break;
                case "Hervir":
                    BattleMenuManager.instance.limpiarSubAccionSeleccionada();
                    BattleMenuManager.instance.cerrarSubPaneles();
                    marcarIngredientesComoCocinado();
                    comandosCorteJuliana();
                    break;
                case "Hornear":
                    BattleMenuManager.instance.limpiarSubAccionSeleccionada();
                    BattleMenuManager.instance.cerrarSubPaneles();
                    marcarIngredientesComoCocinado();
                    comandosCorteJuliana();
                    break;
                case "Elegante":
                    comandosCorteJuliana();
                    marcarIngredientesComoServido();
                    break;
                case "Divertido":
                    comandosCorteJuliana();
                    marcarIngredientesComoServido();
                    break;
                case "Sencillo":
                    comandosCorteJuliana();
                    marcarIngredientesComoServido();
                    break;
            }
            BattleMenuManager.instance.lanzarSistemaRitmo();
            
        }
        else
        {
            EventSystem.current.SetSelectedGameObject(BattleMenuManager.instance.backToLastOpcionSelected());
        }

    }

    // ----------- Subacciones -----------------------------------

    // Agrega los comandos a la accion cortar en julianas
    public void comandosCorteJuliana()
    {
        int numIngredeintes = Accion.instance.getNumeroDeIngredientes();
        int limiteFor = numIngredeintes;
        Comando[] newComandos = new Comando[limiteFor*2];

        //for (int j=0; j < numIngredeintes; j++)
        //{
            for (int i = 0; i < limiteFor; i++)
            {
            Comando temp = poolComandos.GetObject().GetComponent<Comando>();
                temp.configurar(KeyCode.UpArrow);
                newComandos[i*2] = temp;

            Comando temp2 = poolComandos.GetObject().GetComponent<Comando>();
            temp2.configurar(KeyCode.DownArrow);
            newComandos[2*i+1] = temp2;
            }
        //}
        
        comandosActualesTotales = newComandos.Length;
        GeneradorComandos.instance.configurarComandos(newComandos);
    }

    // Agrega los comandos a la accion cortar en julianas
    public void comandosCorteChips()
    {
        Comando[] newComandos = new Comando[6];

        for (int i = 0; i < 6; i++)
        {
            Comando temp = poolComandos.GetObject().GetComponent<Comando>();
            temp.configurar(KeyCode.UpArrow);
            newComandos[i] = temp;
            i++;

            Comando temp1 = poolComandos.GetObject().GetComponent<Comando>();
            temp1.configurar(KeyCode.RightArrow);
            newComandos[i] = temp1;
            i++;

            Comando temp2 = poolComandos.GetObject().GetComponent<Comando>();
            temp2.configurar(KeyCode.DownArrow);
            newComandos[i] = temp2;
        }

        comandosActualesTotales = newComandos.Length;
        GeneradorComandos.instance.configurarComandos(newComandos);
    }

    // Agrega los comandos a la accion cortar en julianas
    public void comandosCorteCuadros()
    {
        Comando[] newComandos = new Comando[4];

        Comando temp = poolComandos.GetObject().GetComponent<Comando>();
        temp.configurar(KeyCode.LeftArrow);
            newComandos[0] = temp;

        Comando temp1 = poolComandos.GetObject().GetComponent<Comando>();
        temp1.configurar(KeyCode.UpArrow);
            newComandos[1] = temp1;

        Comando temp2 = poolComandos.GetObject().GetComponent<Comando>();
        temp2.configurar(KeyCode.DownArrow);
            newComandos[2] = temp2;

        Comando temp3 = poolComandos.GetObject().GetComponent<Comando>();
        temp3.configurar(KeyCode.RightArrow);
            newComandos[3] = temp3;


        comandosActualesTotales = newComandos.Length;
        GeneradorComandos.instance.configurarComandos(newComandos);
    }

    // Agrega los comandos a la accion para mezclar a mano
    // Mezcla los ingredientes
    public void comandosMezclaAMano()
    {
        //Debug.Log("pase como sapo");
        int numIngredeintes = Accion.instance.getNumeroDeIngredientes();
        int limiteFor = numIngredeintes;
        Comando[] newComandos = new Comando[limiteFor*4];
        //Debug.Log(limiteFor);

        for (int i = 0; i < limiteFor; i++)
        {
            Comando temp = poolComandos.GetObject().GetComponent<Comando>();
            temp.configurar(KeyCode.LeftArrow);
            newComandos[4*i+0] = temp;
            
            Comando temp1 = poolComandos.GetObject().GetComponent<Comando>();
            temp1.configurar(KeyCode.UpArrow);
            newComandos[4*i+1] = temp1;

            Comando temp2 = poolComandos.GetObject().GetComponent<Comando>();
            temp2.configurar(KeyCode.RightArrow);
            newComandos[4*i+2] = temp2;

            Comando temp3 = poolComandos.GetObject().GetComponent<Comando>();
            temp3.configurar(KeyCode.DownArrow);
            newComandos[4*i+3] = temp3;
        }
        //if (newComandos[5]) { Debug.Log("el comando 6 es nulo"); }

        comandosActualesTotales = newComandos.Length;
        GeneradorComandos.instance.configurarComandos(newComandos);
        BattleMenuManager.instance.mezclarIngredientesListos();
    }


    public void revisarSiTerminarSistemaRitmo()
    {
        if (BattleMenuManager.instance == null) { Debug.Log("la instancia es nulo"); }
        if (BattleMenuManager.instance.getAccionSeleccionada() == null) { Debug.Log("la accion es nulo"); }
        //if (BattleMenuManager.instance != null && BattleMenuManager.instance.getAccionSeleccionada() != null)
        //{
            string nombreAccion = BattleMenuManager.instance.getAccionSeleccionada().name;
            if (nombreAccion.Equals("Acc3"))
            {
                int numIngredeintes = Accion.instance.getNumeroDeIngredientes();
                ManejoTiempoCocina.instance.gameObject.SetActive(true);
                ManejoTiempoCocina.instance.empezar(numIngredeintes * 3f);
                cambiarEstadoCocina(false);
            }
            BattleMenuManager.instance.terminarSistemaRitmo();
        //}
    }

    // Deshabilita la accion de cocinar
    public void cambiarEstadoCocina(bool estado)
    {
        GameObject.Find("Acc3").GetComponent<Button>().interactable = estado;
    }
}
