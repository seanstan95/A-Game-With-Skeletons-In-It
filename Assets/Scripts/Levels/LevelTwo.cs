using UnityEngine;

public class LevelTwo : MonoBehaviour
{
	private GameManager gameManager;
	private int enemyCount;
	private UIManager UI;
	public GameObject bars, skeleSpawn;
	public GameObject[] spawnPoints;
	public Wizard[] wizards;
	public WizardBoss boss;

	private void Start()
	{
		gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
		gameManager.player.SetActive(true);
		gameManager.playerMove.UpdateLevel();
		gameManager.playerTrans.position = new Vector3(68f, 1.04f, 0f);
		gameManager.playerTrans.eulerAngles = new Vector3(0f, -90f, 0f);
		UI = GameObject.Find("UIManager").GetComponent<UIManager>();
		UI.HUDAnimator.SetTrigger("LevelComplete-End");
		UI.levelText.text = "Defeat 10 Skeletons to advance!";
		UI.ResetUI();
		InvokeRepeating("Spawn", 4f, 2.5f);
	}

	public void EnemyDied()
	{
		//This function is triggered on enemy death and checks if eemyCount is at certain thresholds for progression.
		++enemyCount;
		if (enemyCount < 10)
			UI.levelText.text = "Defeat " + (10 - enemyCount) + " Skeletons to advance!";

		switch (enemyCount)
		{
			case 10:
				//When at 10, stop spawning skeletons and kill all existing ones (directly adjusting health instead of calling Death() on them to avoid side effects)
				CancelInvoke("Spawn");
				foreach (GameObject enemy in GameObject.FindGameObjectsWithTag("NormalEnemy"))
				{
					if (enemy.name == "Skeleton(Clone)")
					{
						enemy.GetComponent<Skeleton>().currentHealth = 0;
						enemy.GetComponent<Skeleton>().Death(false);
					}
				}
				bars.SetActive(false); //bars in the first room
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
				UI.HUDAnimator.SetTrigger("LevelComplete-Start");
				StartCoroutine(gameManager.Done("LevelThree", gameManager.clips[2]));
				break;
		}
	}

	public void DisableWizard(Wizard wizard)
	{
		wizard.active = false;
		wizard.animator.SetBool("Attacking", false);
		wizard.animator.SetBool("Idle", true);
	}

	public void Spawn()
	{
		int spawnNum = Random.Range(0, spawnPoints.Length);
		Instantiate(skeleSpawn, spawnPoints[spawnNum].transform.position, spawnPoints[spawnNum].transform.rotation).GetComponent<Skeleton>().active = true;
	}
}
