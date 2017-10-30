using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonManager : MonoBehaviour {

	public void LoadScene(string levelName)
	{
		SceneManager.LoadScene (levelName);
	}

	//Used in DebugRoom to allow for resuming play via a button as well as just pressing Space again.
	public void Resume()
	{
		InputManager.pauseMenu.SetActive (false);
		Time.timeScale = 1;
	}
		
	public void Quit()
	{
		Application.Quit ();
	}
}
