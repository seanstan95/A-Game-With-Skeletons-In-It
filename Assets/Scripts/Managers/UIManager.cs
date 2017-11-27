using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public static class UIManager {

	public static GameObject pauseMenu;
	public static Text bossText, enemyText, heldText, powerupInfo, powerupText;
	private static GameObject bossSlider2;
	private static RectTransform bossSlider1, enemySlider, playerSlider;

	public static void Initialize () 
	{
		pauseMenu = GameObject.Find ("PauseMenu");
		pauseMenu.SetActive (false);
		bossText = GameObject.Find ("BossTitle").GetComponent<Text> ();
		bossText.gameObject.SetActive (false);
		enemyText = GameObject.Find ("EnemyTitle").GetComponent<Text> ();
		heldText = GameObject.Find ("HeldPowerup").GetComponent<Text> ();
		powerupText = GameObject.Find ("PowerupText").GetComponent<Text> ();
		powerupInfo = GameObject.Find ("PowerupInfo").GetComponent<Text> ();
		powerupInfo.gameObject.SetActive (false);
		bossSlider1 = GameObject.Find ("BossValue").GetComponent<RectTransform> ();
		bossSlider1.gameObject.SetActive (false);
		bossSlider2 = GameObject.Find ("BossHealth");
		bossSlider2.SetActive (false);
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
		if (enemy.tag == "BossEnemy") {
			if (!bossSlider1.gameObject.activeSelf) {
				bossSlider1.gameObject.SetActive (true);
				bossSlider2.SetActive (true);
				bossText.gameObject.SetActive (true);
			}
			if (enemySlider.gameObject.activeSelf) {
				enemySlider.gameObject.SetActive (false);
				enemyText.gameObject.SetActive (false);
				GameObject.Find ("EnemyHealth").SetActive (false);
			}
			bossSlider1.SetSizeWithCurrentAnchors (RectTransform.Axis.Horizontal, enemy.getHealth ());
		} else if(enemy.tag == "NormalEnemy") {
			if (!enemySlider.gameObject.activeSelf) {
				enemySlider.gameObject.SetActive (true);
				enemyText.gameObject.SetActive (true);
				GameObject.Find ("EnemyHealth").SetActive (true);
			}
			if (bossSlider1.gameObject.activeSelf) {
				bossSlider1.gameObject.SetActive (false);
				bossSlider2.SetActive (false);
				bossText.gameObject.SetActive (false);
			}
			enemySlider.SetSizeWithCurrentAnchors (RectTransform.Axis.Horizontal, enemy.getHealth ());
		}
	}

	public static void UpdatePlayer(float amount)
	{
		playerSlider.SetSizeWithCurrentAnchors (RectTransform.Axis.Horizontal, amount);
	}
}
