﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerUI : MonoBehaviour {


    [SerializeField]
    GameObject pauseMenu;

	// Use this for initialization
	void Start () {
        PauseMenu.isOn = false;
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown(KeyCode.Escape))
        {
            TogglePauseMenu();
        }
	}

    void TogglePauseMenu()
    {
        pauseMenu.SetActive(!pauseMenu.activeSelf);
        PauseMenu.isOn = pauseMenu.activeSelf;
    }
}
