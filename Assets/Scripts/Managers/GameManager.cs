using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

	public AudioClip[] clips;
	public static AudioSource audioSource;
	private bool audioSet;
	private static StateType state;
	private static GameManager instance;
	private float loadTimer;
	private GameObject canvas, mainMenu, optionsMenu;
	public static float musicVolume = 100, sfxVolume = 100;

	public enum StateType
	{
		MENU,        //transition to main menu
		OPTIONS,     //transition to options menu
		LVLONET,     //transition to level one
		LVLONEP,     //playing level one
		LVLONED,     //level 1 done
		LVLTWOT, 	 //transition to level two
		LVLTWOP,	 //playing level 2
		LVLTWOD,     //level 2 done
		LVLTHREET,   //transition to level three
		LVLTHREEP,   //playing level 3
		LVLTHREED,   //level 3 done
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
		audioSource = GetComponent<AudioSource>();
		canvas = GameObject.Find ("Canvas");
		mainMenu = GameObject.Find ("MainMenu");
		optionsMenu = GameObject.Find ("OptionsMenu");
		optionsMenu.SetActive (false);
		DontDestroyOnLoad (canvas);
		DontDestroyOnLoad (gameObject);
	}

	private void Update()
	{
		switch (state) {
			case StateType.MENU:
				Cursor.lockState = CursorLockMode.None;
				//if returning to menu from a level
				if (!canvas.activeSelf) {
					Time.timeScale = 0;
					canvas.SetActive (true);
				} else {
					optionsMenu.SetActive (false);
					mainMenu.SetActive (true);
				}
				audioSource.Stop ();
				state = StateType.WAITING;
				break;
			case StateType.OPTIONS:
				mainMenu.SetActive (false);
				optionsMenu.SetActive (true);
				state = StateType.WAITING;
				break;
			case StateType.LVLONET:
				PlayerHealth.currentHealth = 100;
				LevelOne.enemyCount = 0;
				Time.timeScale = 1;
				canvas.SetActive (false);
				SceneManager.LoadScene ("LevelOne");
				audioSource.clip = clips [0];
				audioSource.Play ();
				state = StateType.LVLONEP;
				break;
			case StateType.LVLONED:
				loadTimer += Time.deltaTime;
				if (loadTimer >= 5) {
					state = StateType.LVLTWOT;
					loadTimer = 0;
				}
				break;
			case StateType.LVLTWOT:
				PlayerHealth.currentHealth = 100;
				Time.timeScale = 1;
				canvas.SetActive (false);
				SceneManager.LoadScene ("LevelTwo");
				audioSource.clip = clips [1];
				audioSource.Play ();
				state = StateType.LVLTWOP;
				break;
			case StateType.LVLTWOD:
				loadTimer += Time.deltaTime;
				if (loadTimer >= 5) {
					state = StateType.LVLTHREET;
					loadTimer = 0;
				}
				break;
			case StateType.LVLTHREET:
				PlayerHealth.currentHealth = 100;
				Time.timeScale = 1;
				canvas.SetActive (false);
				SceneManager.LoadScene ("LevelThree");
				audioSource.clip = clips [2];
				audioSource.Play ();
				state = StateType.LVLTHREEP;
				break;
			case StateType.LVLTHREED:
				loadTimer += Time.deltaTime;
				if (loadTimer >= 5) {
					state = StateType.MENU;
					loadTimer = 0;
				}
				break;
			case StateType.LVLONEP:
			case StateType.LVLTWOP:
			case StateType.LVLTHREEP:
				audioSource.volume = musicVolume / 100;
				GameObject.Find ("Player").GetComponent<AudioSource> ().volume = sfxVolume / 100;
				if (PlayerHealth.currentHealth <= 0)
					state = StateType.GAMEOVER;
				break;
			case StateType.GAMEOVER:
				//Reached when the player is dead - triggers GameOver animation
				GameObject.Find ("HUD").GetComponent<Animator> ().SetTrigger ("GameOver");
				loadTimer += Time.deltaTime;
				if (loadTimer >= 5) {
					state = StateType.MENU;
					loadTimer = 0;
				}
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
			case "LVLONED":
				state = StateType.LVLONED;
				break;
			case "LVLTWOT":
				state = StateType.LVLTWOT;
				break;
			case "LVLTWOP":
				state = StateType.LVLTWOP;
				break;
			case "LVLTWOD":
				state = StateType.LVLTWOD;
				break;
			case "LVLTHREET":
				state = StateType.LVLTHREET;
				break;
			case "LVLTHREEP":
				state = StateType.LVLTHREEP;
				break;
			case "LVLTHREED":
				state = StateType.LVLTHREED;
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
			case StateType.LVLTWOP:
				return "LevelTwo";
			case StateType.LVLTHREEP:
				return "LevelThree";
			default:
				return "Error";
		}
	}
}

