﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MapMenu : MonoBehaviour
{
    //---------------------------------------------------------------------------
    // Variables
    //---------------------------------------------------------------------------

    public GameObject firstSelectedButton;




    //---------------------------------------------------------------------------
    // Methods
    //---------------------------------------------------------------------------

    // Use this for initialization
    void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
        
	}

    private void OnEnable()
    {
        Time.timeScale = 0;
        EventSystem.current.SetSelectedGameObject(firstSelectedButton);
    }

    private void OnDisable()
    {
        Time.timeScale = 1;
    }


}
