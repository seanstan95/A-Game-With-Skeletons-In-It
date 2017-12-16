using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelTwo : MonoBehaviour {

	private bool end;
	private GameObject newSpawn;
	private int spawn;
	private float endTimer;
	public int enemyCount;
	public GameObject[] bars, spawns;
	public Wizard[] wizards;
	public static WizardBoss boss;
	public GameObject enemy;

	private void Start()
	{
		boss = GameObject.Find ("WizardBoss").GetComponent<WizardBoss> ();
		InvokeRepeating ("Spawn", 5f, 3f);
		UIManager.levelText.text = "Defeat 10 Skeletons to advance!";
	}

	private void Update()
	{
		if (enemyCount == 10 && bars[0].activeSelf) {
			CancelInvoke ("Spawn");
			foreach (GameObject enemy in GameObject.FindGameObjectsWithTag("NormalEnemy")) {
				if (enemy.name == "Skeleton(Clone)")
					enemy.GetComponent<Skeleton> ().currentHealth -= 120;
			}
			bars [0].SetActive (false);
			bars [1].SetActive (false);
			bars [2].SetActive (false);
			wizards [0].active = true;
			wizards [1].active = true;
			UIManager.levelText.text = "";
		}
		if (enemyCount >= 12 && !wizards[2].active) {
			wizards [2].active = true;
			wizards [3].active = true;
			wizards [4].active = true;
		}
		if (boss.currentHealth <= 0) {
			if (endTimer < 1) {
				endTimer += Time.deltaTime;
			} else if(!end){
				GameObject.Find ("HUD").GetComponent<Animator> ().SetTrigger ("LevelComplete");
				GameManager.SetState ("LVLTWOD");
				end = true;
			}
		}
	}

	public void Spawn()
	{
		spawn = Random.Range (0, spawns.Length);
		newSpawn = Instantiate (enemy, spawns [spawn].transform.position, spawns[spawn].transform.rotation);
		newSpawn.GetComponent<Skeleton> ().active = true;
	}
}
