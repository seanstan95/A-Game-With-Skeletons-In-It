using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public static class UIManager {

	public static GameObject pauseMenu;
	public static Text heldText, onGroundText, powerupText;
	public static Slider playerSlider;
	private static Slider bossSlider, enemySlider;

	public static void Initialize () 
	{
		pauseMenu = GameObject.Find ("PauseMenu");
		heldText = GameObject.Find ("HeldPowerup").GetComponent<Text> ();
		powerupText = GameObject.Find ("PowerupText").GetComponent<Text> ();
		onGroundText = GameObject.Find ("OnGround").GetComponent<Text> ();
		bossSlider = GameObject.Find ("BossHealth").GetComponent<Slider> ();
		enemySlider = GameObject.Find ("EnemyHealth").GetComponent<Slider> ();
		playerSlider = GameObject.Find ("PlayerHealth").GetComponent<Slider> ();

		pauseMenu.SetActive (false);
		enemySlider.gameObject.SetActive (false);
		bossSlider.gameObject.SetActive (false);
		onGroundText.gameObject.SetActive (false);
	}

	public static void Update () 
	{
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
			if (!bossSlider.gameObject.activeSelf) {
				bossSlider.gameObject.SetActive (true);
			}
			if (enemySlider.gameObject.activeSelf) {
				enemySlider.gameObject.SetActive (false);
			}
			if (bossSlider.maxValue != enemy.maxHealth)
				bossSlider.maxValue = enemy.maxHealth;
			bossSlider.value = enemy.currentHealth;
		} else if(enemy.tag == "NormalEnemy") {
			if (!enemySlider.gameObject.activeSelf) {
				enemySlider.gameObject.SetActive (true);
			}
			if (bossSlider.gameObject.activeSelf) {
				bossSlider.gameObject.SetActive (false);
			}
			if (enemySlider.maxValue != enemy.maxHealth)
				enemySlider.maxValue = enemy.maxHealth;
			enemySlider.value = enemy.currentHealth;
		}
	}
}
