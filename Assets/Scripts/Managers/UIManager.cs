using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public static class UIManager {

	public static GameObject pauseMenu;
	public static Text enemyText, heldText, powerupText;

	private static RectTransform bossSlider, enemySlider, playerSlider;

	public static void Initialize () 
	{
		pauseMenu = GameObject.Find ("PauseMenu");
		pauseMenu.SetActive (false);
		enemyText = GameObject.Find ("Title").GetComponent<Text> ();
		heldText = GameObject.Find ("HeldPowerup").GetComponent<Text> ();
		powerupText = GameObject.Find ("PowerupText").GetComponent<Text> ();
		bossSlider = GameObject.Find ("BossValue").GetComponent<RectTransform> ();
		bossSlider.gameObject.SetActive (false);
		enemySlider = GameObject.Find ("EnemyValue").GetComponent<RectTransform> ();
		playerSlider = GameObject.Find ("PlayerValue").GetComponent<RectTransform> ();
	}

	public static void Update () 
	{
		//If player is dead, trigger game over animation.
		if (PlayerHealth.currentHealth <= 0)
			GameManager.SetState ("GAMEOVER");

		//If escape is pressed, exit to the main menu.
		if (Input.GetKeyDown (KeyCode.Escape))
			GameManager.SetState ("MENU");

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
		if (enemy.tag == "BossEnemy")
			enemyText.text = "Boss Health";
		else if (enemy.tag == "NormalEnemy")
			enemyText.text = "Enemy Health";
		
		if (enemy.getHealth () > 100) {
			if (!bossSlider.gameObject.activeSelf)
				bossSlider.gameObject.SetActive (true);
			enemySlider.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, 100);
			bossSlider.SetSizeWithCurrentAnchors (RectTransform.Axis.Horizontal, enemy.getHealth () - 100);
		} else {
			if (bossSlider.gameObject.activeSelf)
				bossSlider.gameObject.SetActive (false);
			enemySlider.SetSizeWithCurrentAnchors (RectTransform.Axis.Horizontal, enemy.getHealth ());
		}

	}

	public static void UpdatePlayer(float amount)
	{
		playerSlider.SetSizeWithCurrentAnchors (RectTransform.Axis.Horizontal, amount);
	}
}
