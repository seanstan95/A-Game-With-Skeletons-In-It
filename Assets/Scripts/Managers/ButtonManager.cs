using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonManager : MonoBehaviour {

	//Used in the MainMenu scene for adding functionality to the buttons.
	public void LoadScene(string levelName)
	{
		SceneManager.LoadScene (levelName);
	}

	public void Quit()
	{
		Application.Quit ();
	}
}
