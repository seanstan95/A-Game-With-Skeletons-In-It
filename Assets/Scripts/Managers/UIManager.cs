using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour {

    private static Slider bossSlider, enemySlider;
    public static GameObject pauseMenu;
	public static Text levelText;
	public static Slider playerSlider;

	private void Start () 
	{
		pauseMenu = GameObject.Find ("PauseMenu");
		levelText = GameObject.Find ("LevelText").GetComponent<Text> ();
		bossSlider = GameObject.Find ("BossHealth").GetComponent<Slider> ();
		enemySlider = GameObject.Find ("EnemyHealth").GetComponent<Slider> ();
		playerSlider = GameObject.Find ("PlayerHealth").GetComponent<Slider> ();

		pauseMenu.SetActive (false);
		enemySlider.gameObject.SetActive (false);
		bossSlider.gameObject.SetActive (false);
	}

	private void Update () 
	{
		//If escape is pressed, exit to the main menu.
		if (Input.GetKeyDown (KeyCode.Escape)) {
            GameObject.Find("Player").GetComponent<AudioSource>().Stop();
			GameManager.SetState ("MENU");
		}

		//If space is pressed, pause or resume the game.
		if (Input.GetKeyDown (KeyCode.Space)) {
			if (Time.timeScale == 1) {
				Time.timeScale = 0;
				pauseMenu.SetActive(true);
				GameManager.audioSource.Pause ();
                Cursor.lockState = CursorLockMode.None;
            } else {
				pauseMenu.SetActive(false);
				Time.timeScale = 1;
				GameManager.audioSource.UnPause ();
                Cursor.lockState = CursorLockMode.Locked;
            }
		}
	}

	public static void UpdateEnemy(Enemy enemy)
	{
		if (enemy.tag == "BossEnemy") {
			if (enemySlider.gameObject.activeSelf) {
				enemySlider.gameObject.SetActive (false);
			}

			if (!bossSlider.gameObject.activeSelf) {
				bossSlider.gameObject.SetActive (true);
			}

			if (enemy.name == "FinalBoss" && !FinalBoss.shield) {
				bossSlider.maxValue = (float)enemy.maxHealth;
				bossSlider.value = enemy.currentHealth;
			} else {
				bossSlider.maxValue = (float)enemy.maxHealth;
				bossSlider.value = enemy.currentHealth;
			}
		} else if(enemy.tag == "NormalEnemy") {
			if (!enemySlider.gameObject.activeSelf) {
				enemySlider.gameObject.SetActive (true);
			}

			if (bossSlider.gameObject.activeSelf) {
				bossSlider.gameObject.SetActive (false);
			}

			enemySlider.maxValue = (float)enemy.maxHealth;
			enemySlider.value = enemy.currentHealth;
		}
	}
}
