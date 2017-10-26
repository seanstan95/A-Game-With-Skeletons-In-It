using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class InputManager : MonoBehaviour {

	//Used to allow the user to press a number of keys to interact with the game.
	//Pressing escape will return to the main menu, and (once implemented) pressing space will pause the game.
	void Update () {
		if (Input.GetKeyDown (KeyCode.Escape))
			SceneManager.LoadScene ("MainMenu");
	}
}
