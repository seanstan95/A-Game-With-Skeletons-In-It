using UnityEngine;
using System.Collections.Generic;

public class LevelOne : MonoBehaviour {
	public static GameObject ironBars;
	public static int enemyCount = 0;
	public static SortedList<int, Skeleton> skeletons = new SortedList<int, Skeleton>();

	private void Start()
    {
		//Manually grabbing game objects because static variables can't be set in the editor
		ironBars = GameObject.Find("IronBars1");
		foreach(GameObject enemy in GameObject.FindGameObjectsWithTag("NormalEnemy"))
        {
			var skele = enemy.GetComponent<Skeleton>();
			skeletons.Add(skele.enemyNum, skele);
        }
		skeletons.Add(17, GameObject.Find("SkeletonBoss").GetComponent<Skeleton>());
    }

	public static void EnemyDied()
    {
		//This function is triggered on enemy death and checks if enemyCount is at certain thresholds for progression.
		//This used to be done in Update() but was moved here (at the cost of working around things becoming static) to remove redundant checks
		switch (enemyCount)
		{
			case 8:
				if (!skeletons.Values[8].active)
				{
					skeletons.Values[8].active = true;
					skeletons.Values[9].active = true;
				}
				break;
			case 10:
				if (ironBars.activeSelf)
					ironBars.SetActive(false);
				break;
			case 12:
				if (!skeletons.Values[12].active)
				{
					skeletons.Values[12].active = true;
					skeletons.Values[13].active = true;
				}
				break;
			case 14:
				if (!skeletons.Values[14].active)
				{
					skeletons.Values[14].active = true;
					skeletons.Values[15].active = true;
				}
				break;
			case 16:
				if (!skeletons.Values[16].active)
					skeletons.Values[16].active = true;
				break;
			case 17:
				float endTimer = 0;
				while (endTimer < 1)
					endTimer += Time.deltaTime;
				GameObject.Find("HUD").GetComponent<Animator>().SetTrigger("LevelComplete");
				GameManager.SetState("LVLONED");
				break;
		}
	}

	public void EnemyTrigger(string trigger)
	{
		//This function is triggered when the player crosses a level trigger and handles enemy activations.
		switch (trigger) {
			case "Trigger1":
                skeletons.Values[0].active = true;
				break;
			case "Trigger2":
                skeletons.Values[1].active = true;
                skeletons.Values[2].active = true;
				break;
			case "Trigger3":
                skeletons.Values[3].active = true;
                skeletons.Values[4].active = true;
                skeletons.Values[5].active = true;
                break;
			case "Trigger4":
                skeletons.Values[6].active = true;
                skeletons.Values[7].active = true;
				break;
			case "Trigger5":
                skeletons.Values[10].active = true;
				skeletons.Values[11].active = true;
				break;
		}
	}
}
