using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class GameManager : MonoBehaviour
{
	public Animator playerAnimation;
	public AudioClip[] clips;
	public AudioSource musicSource, sfxSource;
	public bool loading;
	public GameObject player;
	public PlayerAttack playerAttack;
	public PlayerHealth playerHealth;
	public PlayerMove playerMove;
	public Transform playerTrans;
	public UIManager UI;

	private void Start()
	{
		DontDestroyOnLoad(gameObject);
		DontDestroyOnLoad(player);
		DontDestroyOnLoad(UI);
	}

	public void LevelLoad(string scene, AudioClip clip)
	{
		SceneManager.LoadScene(scene);
		playerHealth.currentHealth = 100;
		playerHealth.damageImage.color = Color.clear;
		playerHealth.dead = false;
		UI.HUDAnimator.SetTrigger("Reset");
		UI.HUDAnimator.ResetTrigger("Reset");
		UI.ResetUI();
		Time.timeScale = 1;
		musicSource.clip = clip;
		musicSource.Play();
		loading = false;
	}

	public IEnumerator Done(string level = null, AudioClip clip = null, bool gameDone = false)
    {
		loading = true;
		yield return new WaitForSeconds(3);
		if (gameDone)
			UI.HUDAnimator.SetTrigger("LevelComplete-End");
		if (level != null)
			LevelLoad(level, clip);
		else
			UI.ReturnToMenu(false);
    }
}