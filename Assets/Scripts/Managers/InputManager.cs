using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class InputManager : MonoBehaviour {

	public static GameObject hud, pauseMenu;

	void Start()
	{
		//Note that the references are set up before setting the pause menu to be inactive.
		hud = GameObject.FindGameObjectWithTag ("StatHUD");
		pauseMenu = GameObject.FindGameObjectWithTag ("Pause");
		pauseMenu.SetActive (false);
	}
		
	void Update () {
		//Pressing escape will return to the main menu.
		if (Input.GetKeyDown (KeyCode.Escape))
			SceneManager.LoadScene ("MainMenu");

		//Pressing Q will toggle the stats display on and off (fire rate, damage, and health).
		if (Input.GetKeyDown (KeyCode.Q)) {
			if (hud.activeInHierarchy)
				hud.SetActive (false);
			else
				hud.SetActive(true);
		}

		//Pressing Space will pull up the pause menu, which is initialized as inactive but layered on top of the Debug Room screen.
		//Pressing Space again will unpause the game, as will clicking "resume".
		if (Input.GetKeyDown (KeyCode.Space)) {
			if (Time.timeScale == 1) {
				Time.timeScale = 0;
				pauseMenu.SetActive(true);
			} else {
				pauseMenu.SetActive(false);
				Time.timeScale = 1;
			}
		}
	}
}
