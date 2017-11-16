using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

	private static StateType state;
	private static GameManager instance;

	public enum StateType
	{
		MENU,         //transition to main menu
		OPTIONS,      //transition to options menu
		LVLONE,       //transition to level one
		LVLONEBOSS,   //fighting level one boss
		PLAYING,      //playing in a level
		WAITING,      //waiting at a menu screen
		GAMEOVER      //game is over
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
		DontDestroyOnLoad (gameObject);
	}

	private void Update()
	{
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
				/*Debug.Log(GameObject.FindGameObjectWithTag("BossEnemy").GetComponent<Enemy>().getHealth());
				if (GameObject.FindGameObjectWithTag ("BossEnemy").GetComponent<Enemy> ().getHealth () <= 0) {
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
				//Reached when player is dead - triggers gameover animation
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
			case "LEVELONE":
				state = StateType.LVLONE;
				break;
			case "PLAYING":
				state = StateType.PLAYING;
				break;
			case "GAMEOVER":
				state = StateType.GAMEOVER;
				break;
		}
	}
}
