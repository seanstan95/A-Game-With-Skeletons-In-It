using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public static class UIManager {

	public static GameObject pauseMenu;
	public static Text heldText, powerupText;

	private static RectTransform enemySlider, playerSlider;

	public static void Initialize () 
	{
		heldText = GameObject.Find ("HeldPowerup").GetComponent<Text> ();
		enemySlider = GameObject.Find ("EnemyValue").GetComponent<RectTransform> ();
		playerSlider = GameObject.Find ("PlayerValue").GetComponent<RectTransform> ();
		pauseMenu = GameObject.Find ("PauseMenu");
		pauseMenu.SetActive (false);
		powerupText = GameObject.Find ("PowerupText").GetComponent<Text> ();
	}

	public static void Update () 
	{
		//If player is dead, trigger game over animation.
		if (PlayerHealth.currentHealth <= 0)
			GameObject.Find ("HUD").GetComponent<Animator> ().SetTrigger ("GameOver");

		//If escape is pressed, exit to the main menu.
		if (Input.GetKeyDown (KeyCode.Escape))
			SceneManager.LoadScene ("MainMenu");

		//If space is pressed, pause or resume the game.
		if (Input.GetKeyDown (KeyCode.Space)) {
			if (Time.timeScale == 1) {
				Time.timeScale = 0;
				pauseMenu.SetActive(true);
			} else {
				pauseMenu.SetActive(false);
				Time.timeScale = 1;
			}
		}

		//If F is pressed, activate the currently held powerup.
		if (Input.GetKeyDown (KeyCode.F) && PowerupManager.currentPowerup == "None") {
			PowerupManager.UsePowerup ();
			heldText.text = "Held Powerup: None";
		}
	}

	public static void UpdateEnemy(Enemy enemy)
	{
		enemySlider.SetSizeWithCurrentAnchors (RectTransform.Axis.Horizontal, enemy.getHealth());
	}

	public static void UpdatePlayer(float amount)
	{
		playerSlider.SetSizeWithCurrentAnchors (RectTransform.Axis.Horizontal, amount);
	}
}
