using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class LevelTwo : MonoBehaviour
{
	// Public Fields
	public GameObject bars1, bars2, bars3, skeleSpawn;
	public GameObject[] spawnPoints;
	public Wizard[] wizards;
	public WizardBoss boss;
	
	// Private Fields
	private GameManager gameManager;
	private UIManager UI;
	private Vector3 bar1LerpPos, bar2LerpPos, bar3LerpPos;
	private int enemyCount;
	private bool lerpBars;
	
	// Cached Animator Hashes
	private static readonly int levelCompleteStart = Animator.StringToHash("LevelComplete-Start");
	private static readonly int levelCompleteEnd = Animator.StringToHash("LevelComplete-End");
	private static readonly int Attacking = Animator.StringToHash("Attacking");
	private static readonly int Idle = Animator.StringToHash("Idle");

	private void Start()
	{
		gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
		gameManager.player.SetActive(true);
		gameManager.playerMove.UpdateLevel();
		gameManager.playerTrans.position = new Vector3(68f, 1.04f, 0f);
		gameManager.playerTrans.eulerAngles = new Vector3(0f, -90f, 0f);
		UI = GameObject.Find("UIManager").GetComponent<UIManager>();
		UI.HUDAnimator.SetTrigger(levelCompleteEnd);
		UI.ResetUI();
		UI.levelText.text = "Defeat 10 Skeletons to advance!";
		InvokeRepeating(nameof(Spawn), 4f, 2.5f);
		
		Vector3 barPosition = bars1.transform.localPosition;
		bar1LerpPos = new Vector3(barPosition.x, -3f, barPosition.z);
		barPosition = bars2.transform.localPosition;
		bar2LerpPos = new Vector3(barPosition.x, -3f, barPosition.z);
		barPosition = bars3.transform.localPosition;
		bar3LerpPos = new Vector3(barPosition.x, -3f, barPosition.z);
	}

    private void Update()
    {
		if (lerpBars && Math.Abs(Time.timeScale - 1) < float.Epsilon)
		{
			bars1.transform.localPosition = Vector3.Lerp(bars1.transform.localPosition, bar1LerpPos, .01f);
			bars2.transform.localPosition = Vector3.Lerp(bars2.transform.localPosition, bar2LerpPos, .01f);
			bars3.transform.localPosition = Vector3.Lerp(bars3.transform.localPosition, bar3LerpPos, .01f);
		}
		
		if (bars1.transform.localPosition.y < -2.9f) lerpBars = false;
	}

    public void EnemyDied()
	{
		//This function is triggered on enemy death and checks if enemyCount is at certain thresholds for progression.
		++enemyCount;
		if (enemyCount < 10)
			UI.levelText.text = "Defeat " + (10 - enemyCount) + " Skeletons to advance!";

		switch (enemyCount)
		{
			case 10:
				//When at 10, stop spawning skeletons and kill all existing ones (directly adjusting health instead of calling Death() on them to avoid side effects)
				CancelInvoke(nameof(Spawn));
				foreach (GameObject enemy in GameObject.FindGameObjectsWithTag("NormalEnemy"))
				{
					if (enemy.name != "Skeleton(Clone)") continue;

					Skeleton s = enemy.GetComponent<Skeleton>();
					
					s.currentHealth = 0;
					s.Death(false);
				}
				
				lerpBars = true;
				wizards[0].active = true; //the 2 wizards behind the bars
				wizards[1].active = true;
				UI.levelText.text = "";
				break;
			case 12:
				wizards[2].active = true; //the 3 stationary wizards in the middle room that can't be killed
				wizards[3].active = true;
				wizards[4].active = true;
				break;
			case 14:
				UI.HUDAnimator.SetTrigger(levelCompleteStart);
				StartCoroutine(gameManager.Done("LevelThree", gameManager.clips[2]));
				break;
		}
	}

	public void DisableWizard(Wizard wizard)
	{
		wizard.active = false;
		wizard.animator.SetBool(Attacking, false);
		wizard.animator.SetBool(Idle, true);
	}

	public void Spawn()
	{
		int spawnNum = Random.Range(0, spawnPoints.Length);
		Instantiate(skeleSpawn, spawnPoints[spawnNum].transform.position, spawnPoints[spawnNum].transform.rotation).GetComponent<Skeleton>().active = true;
	}
}
