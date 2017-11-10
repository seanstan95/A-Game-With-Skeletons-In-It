using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour {

	public static GameObject pauseMenu;
	public static Text powerupText;

	PlayerHealth playerHealth;
	RectTransform enemySlider, playerSlider;

	void Start () 
	{
		enemySlider = GameObject.Find ("EnemyValue").GetComponent<RectTransform> ();
		playerSlider = GameObject.Find ("PlayerValue").GetComponent<RectTransform> ();
		pauseMenu = GameObject.Find ("PauseMenu");
		pauseMenu.SetActive (false);
		powerupText = GameObject.Find ("PowerupText").GetComponent<Text> ();
		playerHealth = GameObject.Find ("Player").GetComponent<PlayerHealth> ();
	}

	void Update () 
	{
		//If player is dead, trigger game over animation.
		if (playerHealth.currentHealth <= 0)
			GetComponent<Animator> ().SetTrigger ("GameOver");

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
	}

	public void UpdateEnemy(Skeleton skeleton)
	{
		enemySlider.SetSizeWithCurrentAnchors (RectTransform.Axis.Horizontal, skeleton.getHealth());
	}

	public void UpdatePlayer(float amount)
	{
		playerSlider.SetSizeWithCurrentAnchors (RectTransform.Axis.Horizontal, amount);
	}
}
