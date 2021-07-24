using UnityEngine;
using UnityEngine.UI;

public class ButtonManager : MonoBehaviour
{
	public void ChangeState(string state)
	{
		GameManager.SetState(state);
	}

	public void Volume(string slider)
	{
		if (slider == "MusicSlider")
			GameManager.musicVolume = GameObject.Find(slider).GetComponent<Slider>().value;
		else if (slider == "SfxSlider")
			GameManager.sfxVolume = GameObject.Find(slider).GetComponent<Slider>().value;
	}

	public void Resume()
	{
		UIManager.pauseMenu.SetActive(false);
		Cursor.lockState = CursorLockMode.Locked;
		Time.timeScale = 1;
	}

	public void Quit()
	{
		Application.Quit();
	}
}
