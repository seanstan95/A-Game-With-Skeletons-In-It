using UnityEngine;

public class LevelTwo : MonoBehaviour {
	private int enemyCount;
	public Animator anim;
	public GameObject bars, skeleSpawn;
	public GameObject[] spawnPoints;
	public Wizard[] wizards;
	public WizardBoss boss;

	private void Start()
	{
		InvokeRepeating ("Spawn", 4f, 2.5f);
		UIManager.levelText.text = "Defeat 10 Skeletons to advance!";
		GameManager.player.SetActive(true);
		GameManager.SetPlayerPosition(68f, 1.04f, 0f, 0f, -90f, 0f);
		GameManager.player.GetComponent<PlayerMove>().UpdateLevel();
	}

	public void EnemyDied()
	{
		//This function is triggered on enemy death and checks if eemyCount is at certain thresholds for progression.
		++enemyCount;
		if (enemyCount < 10)
			UIManager.levelText.text = "Defeat " + (10 - enemyCount) + " Skeletons to advance!";

		switch(enemyCount)
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
				UIManager.levelText.text = "";
				break;
			case 12:
				wizards[2].active = true; //the 3 stationary wizards in the middle room that can't be killed
				wizards[3].active = true;
				wizards[4].active = true;
				break;
			case 14:
				GameManager.HUD.GetComponent<Animator>().SetTrigger("LevelComplete");
				StartCoroutine(GameManager.LevelDone(GameManager.StateType.LVLTHREET));
				break;
        }
	}

	public void Spawn()
	{
		int spawnNum = Random.Range (0, spawnPoints.Length);
        Instantiate(skeleSpawn, spawnPoints[spawnNum].transform.position, spawnPoints[spawnNum].transform.rotation).GetComponent<Skeleton>().active = true;
	}
}
