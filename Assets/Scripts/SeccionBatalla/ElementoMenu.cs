﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ElementoMenu : MonoBehaviour, ISelectHandler, IDeselectHandler
{

    //---------------------------------------------------------------------------
    // Variables
    //---------------------------------------------------------------------------

    // Botón del cuál fue llamado este elemento.
    // Se necesita conocer para poder marcarlo como seleccionado en caso de
    // retroceder en la jerarquía.
    public Button padre;

    // Botón que marcaré como seleccionado al mostrar el panel
    // en el siguiente nivel de jerarquía. Sin esto no podré navegar al avanzar
    // al siguiente nivel de jerarquía.
	public Button hijo;

    // Panel del siguiente nivel de jerarquía.
    // Al conocerlo, puedo mostrarlo al avanzar al siguiente nivel de la jerarquía.
	public GameObject panelHijo;

    // Panel al que pertenezco.
    // Al conocerlo, puedo ocultarlo al avanzar niveles a lo largo de la jerarquía.
	public GameObject panelMio;

    // Indica si el elemento es el que está actualmente elegido, para filtrar sus acciones.
	bool activo = false;

    // Sistema de eventos
	EventSystem evSys;

    // Instacia del Battle Menu Manager
    private BattleMenuManager instanciaBBManager;



    //---------------------------------------------------------------------------
    // Methods
    //---------------------------------------------------------------------------

    // Adiciona el comando de siguiente al botón.
    // Este botón coincide en nivel de jerarquía. Es el botón al que pertenece este script.
    // Inicializa el sistema de eventos, para poder marcar los botones como seleccionados
    // transversalmente en la jerarquía.
    void Start ()
    {
		Button btn = GetComponent<Button>();

        if(btn != null && tag != "Recurso")
        {
            btn.onClick.AddListener(Siguiente);
        }
        else if(tag == "Recurso")
        {
            //this.GetComponent<ElementoRecursos>().manejarSeleccionBotonRecurso();
        }

        evSys = EventSystem.current;

        instanciaBBManager = BattleMenuManager.instance;
    }

    // En este caso, identifica si debe retrocederse en la jerarquía
    void Update()
    {
    	if(activo && Input.GetKeyDown(KeyCode.Backspace))
    	{
    		Anterior();
    	}
    }

    // Al estar activo el botón, avanza un paso en la jerarquía.
    // Adicionalmente, establece al botón hijo como el elemento seleccionado.
    void Siguiente()
    {
		if(activo)
		{
            if(panelHijo != null && hijo != null)
            {
                if (panelHijo == null) { Debug.Log("panHijo null"); }
                if (hijo==null) { Debug.Log("hijo null"); }
                if (hijo.GetComponent<ElementoMenu>()==null) { Debug.Log(" hijo comp ElMen null"); }
                //if (hijo.GetComponent<ElementoMenu>().padre==null) { Debug.Log("hijo comp ElMen pater null"); }
                if (GetComponent<Button>()==null) { Debug.Log("this comp button null"); }
                if (evSys==null) { Debug.Log("evSys null"); }
                //if () { Debug.Log(); }

                panelHijo.SetActive(true);
                if(hijo.GetComponent<ElementoMenu>().padre != null)
                {
                    hijo.GetComponent<ElementoMenu>().padre = GetComponent<Button>();
                }
                
                evSys.SetSelectedGameObject(hijo.gameObject);
            }
		}
    }

    // Al estar activo el botón, se devuelve un paso en la jerarquía.
    // Adicionalmente, establece al botón padre como el elemento seleccionado.
    // Vacia la lista de ingredeintes a suar
    public void Anterior()
    {
        if(activo && instanciaBBManager.getAccionSeleccionada() != null)
        {
            Accion.instance.vaciarListaRecursosActuales();
            evSys.SetSelectedGameObject(BattleMenuManager.instance.backToLastOpcionSelected());
            if (panelMio != null)
            {
                panelMio.SetActive(false);
            }
        }

    }

    // Este método se llama al sombrearse un botón.
    // Se marca el botón como el botón activo, para que sólo él pueda ejecutar 
    // la acción de siguiente y anterior, sobre los demás scripts.
	public void OnSelect(BaseEventData eventData)
    {
        activo = true;
    }

    // Este método se llama al desmarcarse un botón.
    // Al estar desmarcado, sus acciones no tienen efecto.
    public void OnDeselect(BaseEventData eventData)
    {
        activo = false;
    }

    public void desactivarMenu()
    {
        panelMio.SetActive(false);
    }
}
