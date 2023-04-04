using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class GameManager : MonoBehaviour
{
	public Transform playerTrans;
	public Animator playerAnimation;
	public AudioClip[] clips;
	public AudioSource musicSource, sfxSource;
	public GameObject player;
	
	public PlayerAttack playerAttack;
	public PlayerHealth playerHealth;
	public PlayerMove playerMove;
	public UIManager UI;
	public bool loading;
	
	// Private Fields
	private WaitForSeconds threeSeconds;

	// Cached Animator Hashes
	private static readonly int Reset = Animator.StringToHash("Reset");
	private static readonly int Property = Animator.StringToHash("LevelComplete-End");

	private void Awake()
	{
		threeSeconds = new WaitForSeconds(3);
	}

	private void Start()
	{
		DontDestroyOnLoad(gameObject);
		DontDestroyOnLoad(player);
		DontDestroyOnLoad(UI);
	}

	public void LevelLoad(string scene, AudioClip clip)
	{
		SceneManager.LoadScene(scene);
		playerHealth.HealthReset();
		UI.HUDAnimator.SetTrigger(Reset);
		UI.HUDAnimator.ResetTrigger(Reset);
		UI.ResetUI();
		Time.timeScale = 1;
		musicSource.clip = clip;
		musicSource.Play();
		loading = false;
	}

	public IEnumerator Done(string level = null, AudioClip clip = null, bool gameDone = false)
    {
		loading = true;
		yield return threeSeconds;
		if (gameDone)
			UI.HUDAnimator.SetTrigger(Property);
		if (level != null)
			LevelLoad(level, clip);
		else
			UI.ReturnToMenu(false);
    }
}