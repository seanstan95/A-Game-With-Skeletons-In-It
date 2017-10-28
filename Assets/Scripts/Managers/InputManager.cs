using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class InputManager : MonoBehaviour {

	GameObject hud;

	void Start()
	{
		hud = GameObject.FindGameObjectWithTag ("StatHUD");
	}

	//Pressing escape will return to the main menu, and (once implemented) pressing space will pause the game.
	//Pressing Q will toggle the stats display on and off (fire rate, damage, and health).
	void Update () {
		if (Input.GetKeyDown (KeyCode.Escape))
			SceneManager.LoadScene ("MainMenu");
		if (Input.GetKeyDown (KeyCode.Q)) {
			if (hud.activeInHierarchy)
				hud.SetActive (false);
			else
				hud.SetActive(true);
		}
	}
}
