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
		LVLONE,      //transition to level one
		LVLONEBOSS,  //fighting level one boss
		PLAYING,     //playing in a level
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
			case StateType.LVLONE:
				SceneManager.LoadScene ("LevelOne");
				state = StateType.PLAYING;
				break;
			case StateType.LVLONEBOSS:
				//when level one is working properly
				/*if(GameObject.FindGameObjectWithTag("BossEnemey").GetComponent<Enemy>().getHealth() <= 0) {
					state = StateType.MENU;
				}*/
				break;
			case StateType.WAITING:
				break;
			case StateType.PLAYING:
				//Check for player death
				if (PlayerHealth.currentHealth <= 0)
					state = StateType.GAMEOVER;
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
			case "LVLONE":
				state = StateType.LVLONE;
				break;
			case "LVLONEBOSS":
				state = StateType.LVLONEBOSS;
				break;
			case "PLAYING":
				state = StateType.PLAYING;
				break;
			case "GAMEOVER":
				state = StateType.GAMEOVER;
				break;
		}
	}

	public static string GetLevel()
	{
		switch (state) {
			case StateType.PLAYING:
				return "LevelOne";
			default:
				return "Error";
		}
	}
}













