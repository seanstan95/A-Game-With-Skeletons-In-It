using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Threading.Tasks;

public class GameManager : MonoBehaviour
{
	private static StateType state;
	private static GameManager instance;
	public static float musicVolume = 100, sfxVolume = 100;
	public AudioClip[] clips;
	public static PlayerHealth playerHealth;
	public static AudioSource musicSource, sfxSource;
	public GameObject canvas, mainMenu, optionsMenu;
	public UIManager UI;
	public static GameObject player, HUD;

	public enum StateType
	{
		MENU,        //transition to main menu
		OPTIONS,     //transition to options menu	
		LVLONET,     //transition to level one
		LVLTWOT,     //transition to level two
		LVLTHREET,   //transition to level three
		WAITING,     //waiting at a menu scene
	};

	private void Start()
	{
		if (instance == null)
			instance = this;
		else
		{
			Destroy (gameObject);
			return;
		}

		state = StateType.MENU; //should be inferred by C# from StateType declaration but explicitly stating just in case
		player = GameObject.Find("Player");
		musicSource = GetComponent<AudioSource>();
		sfxSource = player.GetComponent<AudioSource>();
		playerHealth = player.GetComponent<PlayerHealth>();
		HUD = GameObject.Find("HUD");
		UI.Setup();

		optionsMenu.SetActive(false);
		player.SetActive(false);
		HUD.SetActive(false);

		DontDestroyOnLoad(canvas);
		DontDestroyOnLoad(gameObject);
		DontDestroyOnLoad(player);
		DontDestroyOnLoad(HUD);
		DontDestroyOnLoad(UI);
	}

	private void Update()
	{
		switch (state)
		{
			case StateType.MENU:
				Cursor.lockState = CursorLockMode.None;
				Time.timeScale = 0;
				canvas.SetActive(true);
				optionsMenu.SetActive(false);
				mainMenu.SetActive(true);
				musicSource.Stop();
				state = StateType.WAITING;
				break;
			case StateType.OPTIONS:
				mainMenu.SetActive(false);
				optionsMenu.SetActive(true);
				state = StateType.WAITING;
				break;
			case StateType.LVLONET:
				LevelLoad("LevelOne", clips[0], StateType.WAITING);
				break;
			case StateType.LVLTWOT:
				LevelLoad("LevelTwo", clips[1], StateType.WAITING);
				break;
			case StateType.LVLTHREET:
				LevelLoad("LevelThree", clips[2], StateType.WAITING);
				break;
		}
	}

	public static void SetState(string newState)
	{
		switch (newState)
		{
			case "MENU":
				state = StateType.MENU;
				break;
			case "OPTIONS":
				state = StateType.OPTIONS;
				break;
			case "LVLONET":
				state = StateType.LVLONET;
				break;
			case "LVLTWOT": //for debugging only, never used in-game normally
				state = StateType.LVLTWOT;
				break;
			case "LVLTHREET": //for debugging only, never used in-game normally
				state = StateType.LVLTHREET;
				break;
		}
	}

	private void LevelLoad(string scene, AudioClip clip, StateType newState)
	{
		SceneManager.LoadScene(scene);
		playerHealth.currentHealth = 100;
		Time.timeScale = 1;
		canvas.SetActive(false);
		musicSource.clip = clip;
		musicSource.Play();
		state = newState;
		musicSource.volume = musicVolume / 100;
		player.GetComponent<AudioSource>().volume = sfxVolume / 100;
		HUD.SetActive(true);
	}

	public static IEnumerator LevelDone(StateType newState)
    {
		yield return new WaitForSeconds(5);
		state = newState;
	}

	public static IEnumerator PlayerDead()
    {
		yield return new WaitForSeconds(3);
		state = StateType.MENU;
    }

	public static void SetPlayerPosition(float x, float y, float z, float xRot, float yRot, float zRot)
    {
		player.transform.position = new Vector3(x, y, z);
		player.transform.eulerAngles = new Vector3(xRot, yRot, zRot);
    }
}