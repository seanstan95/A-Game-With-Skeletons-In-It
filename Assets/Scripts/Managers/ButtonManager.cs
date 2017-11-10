using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonManager : MonoBehaviour {

	public void Resume()
	{
		UIManager.pauseMenu.SetActive (false);
		Time.timeScale = 1;
	}

	public void LoadScene(string name)
	{
		SceneManager.LoadScene (name);
	}

	public void Quit()
	{
		Application.Quit ();
	}
}
