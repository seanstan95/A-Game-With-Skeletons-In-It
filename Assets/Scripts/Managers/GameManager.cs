using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

	private static StateType state;
	private static GameManager instance;

	public enum StateType
	{
		MENU,        //transition to main menu
		OPTIONS,     //transition to options menu
		LVLONET,     //transition to level one
		LVLONEP,		 //playing level one
		WAITING,     //waiting at a menu scene
		GAMEOVER     //player is dead
	};

	private void Start()
	{
		if (instance == null) {
			instance = this;
		} else {
			Destroy (gameObject);
			return;
		}

		//This prevents the GameManager object from dying when the MainMenu scene is left.
		DontDestroyOnLoad(gameObject);
	}

	private void Update()
	{
		Debug.Log (state);
		switch (state) {
			case StateType.MENU:
				SceneManager.LoadScene ("MainMenu");
				state = StateType.WAITING;
				break;
			case StateType.OPTIONS:
				SceneManager.LoadScene ("OptionsMenu");
				state = StateType.WAITING;
				break;
			case StateType.LVLONET:
				SceneManager.LoadScene ("LevelOne");
				state = StateType.LVLONEP;
				break;
			case StateType.LVLONEP:
				//Check for player death
				if (PlayerHealth.currentHealth <= 0)
					state = StateType.GAMEOVER;
				break;
			case StateType.WAITING:
				break;
			case StateType.GAMEOVER:
				//Reached when the player is dead - triggers GameOver animation
				GameObject.Find ("HUD").GetComponent<Animator> ().SetTrigger ("GameOver");
				state = StateType.WAITING;
				break;
		}
	}

	public static void SetState(string newState)
	{
		switch (newState) {
			case "MENU":
				state = StateType.MENU;
				break;
			case "OPTIONS":
				state = StateType.OPTIONS;
				break;
			case "LVLONET":
				state = StateType.LVLONET;
				break;
			case "LVLONEP":
				state = StateType.LVLONEP;
				break;
			case "PLAYING":
				state = StateType.WAITING;
				break;
			case "GAMEOVER":
				state = StateType.GAMEOVER;
				break;
		}
	}

	public static string GetLevel()
	{
		switch (state) {
			case StateType.LVLONEP:
				return "LevelOne";
			default:
				return "Error";
		}
	}
}













