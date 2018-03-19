using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ElementoMenu : MonoBehaviour, ISelectHandler, IDeselectHandler {

	public Button padre;

	public Button hijo;

	public GameObject panelHijo;

	public GameObject panelMio;

	bool activo = false;

	EventSystem evSys;
	// Use this for initialization
	void Start () {
		Button btn = GetComponent<Button>();
        if(btn != null)
        {
            btn.onClick.AddListener(Siguiente);
        }
        evSys = EventSystem.current;
    }

    void Update()
    {
    	if(activo && Input.GetKeyDown(KeyCode.Backspace))
    	{
    		Anterior();
    	}
    }

    void Siguiente()
    {
		if(activo)
		{
			panelHijo.SetActive(true);
			hijo.GetComponent<ElementoMenu>().padre = GetComponent<Button>();
			evSys.SetSelectedGameObject(hijo.gameObject);
		}
    }

    void Anterior()
    {
		if(activo && padre != null)
		{
			evSys.SetSelectedGameObject(padre.gameObject);
			panelMio.SetActive(false);
		}
    }

	public void OnSelect(BaseEventData eventData)
    {
        activo = true;
    }

    public void OnDeselect(BaseEventData eventData)
    {
        activo = false;
    }
}
