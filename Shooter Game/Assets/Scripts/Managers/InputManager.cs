﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class InputManager : MonoBehaviour {

	void Update () {
		if (Input.GetKeyDown (KeyCode.Escape))
			SceneManager.LoadScene ("MainMenu");
		if (Input.GetKeyDown (KeyCode.Space))
			Debug.Log ("Insert Pause Menu Here");//pause menu
	}
}
