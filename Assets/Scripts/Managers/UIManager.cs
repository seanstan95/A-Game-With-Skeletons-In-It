﻿using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
	public Animator HUDAnimator;
	public GameManager gameManager;
	public GameObject canvas, HUD, mainMenu, optionsMenu, pauseMenu;
	public Slider bossSlider, enemySlider, playerSlider;
	public Text levelText;

	public void Start()
	{
		DontDestroyOnLoad(canvas);
		DontDestroyOnLoad(HUD);
	}

	private void Update()
	{
		if (gameManager.loading)
			return;

		//Exiting game (back to main menu)
		if (Input.GetKeyDown(KeyCode.Escape))
			ReturnToMenu(false);

		//Pausing/unpausing the game
		if (Input.GetKeyDown(KeyCode.Space) && SceneManager.GetActiveScene().name != "MainMenu")
		{
			if (Time.timeScale == 1)
			{
				Time.timeScale = 0;
				pauseMenu.SetActive(true);
				gameManager.musicSource.Pause();
				Cursor.lockState = CursorLockMode.None;
			}
			else
				Unpause();
		}
	}

	public void UpdateEnemy(Enemy enemy)
	{
		if (enemy.tag == "BossEnemy")
		{
			enemySlider.gameObject.SetActive(false);
			bossSlider.gameObject.SetActive(true);
			bossSlider.maxValue = enemy.maxHealth;
			bossSlider.value = enemy.currentHealth;
		}
		else if (enemy.tag == "NormalEnemy")
		{
			bossSlider.gameObject.SetActive(false);
			enemySlider.gameObject.SetActive(true);
			enemySlider.maxValue = enemy.maxHealth;
			enemySlider.value = enemy.currentHealth;
		}
	}

	public void ResetUI()
    {
		playerSlider.value = gameManager.playerHealth.currentHealth;
		bossSlider.gameObject.SetActive(false);
		enemySlider.gameObject.SetActive(false);
		levelText.text = "";
		canvas.SetActive(false);
		pauseMenu.SetActive(false);
		HUD.SetActive(true);
		Cursor.lockState = CursorLockMode.Locked;
	}

	public void ReturnToMenu(bool fromOptions)
	{
		optionsMenu.SetActive(false);
		mainMenu.SetActive(true);
		if (!fromOptions)
		{
			Cursor.lockState = CursorLockMode.None;
			Time.timeScale = 0;
			canvas.SetActive(true);
			gameManager.musicSource.Stop();
		}
	}

	public void OnClick(string buttonClicked)
	{
		if (buttonClicked == "Quit")
			Application.Quit();
		else if (buttonClicked == "NewGame")
			gameManager.LevelLoad("LevelThree", gameManager.clips[0]);
		else if (buttonClicked == "Back")
			ReturnToMenu(true);
		else if (buttonClicked == "Options")
		{
			mainMenu.SetActive(false);
			optionsMenu.SetActive(true);
		}
	}

	public void Unpause()
	{
		Time.timeScale = 1;
		pauseMenu.SetActive(false);
		gameManager.musicSource.UnPause();
		Cursor.lockState = CursorLockMode.Locked;
	}

	public void Volume(string slider)
	{
		if (slider == "MusicSlider")
			gameManager.musicSource.volume = GameObject.Find(slider).GetComponent<Slider>().value / 100;
		else if (slider == "SfxSlider")
			gameManager.sfxSource.volume = GameObject.Find(slider).GetComponent<Slider>().value / 100;
	}
}
