using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelThree : MonoBehaviour {

	public static int enemyCount;
	public GameObject ironBars1, ironBars2;
	public Skeleton[] skeletons;
	public Wizard[] wizards;
	public WizardBoss wizBoss;

	private void Update()
	{
		switch (enemyCount) {
			case 4:
				if (!skeletons[4].active)
					skeletons[4].active = true;
				break;
			case 5:
				if (!wizards [0].active) {
					wizards [0].active = true;
					wizards [1].active = true;
					wizards [2].active = true;
					wizards [3].active = true;
				}
				break;
			case 9:
				if (ironBars1.activeSelf)
					ironBars1.SetActive (false);
				break;
			case 10:
				if (ironBars2.activeSelf)
					ironBars2.SetActive (false);
				break;
			default:
				break;
		}
	}

	public void EnemyTrigger(string trigger)
	{
		//This function handles activating enemies from their stationary position once the player hits certain trigger points.
		switch (trigger) {
			case "Trigger1":
				skeletons [0].active = true;
				skeletons [1].active = true;
				skeletons [2].active = true;
				skeletons [3].active = true;
				break;
			case "BossTrigger":
				wizBoss.active = true;
				break;
		}
	}
}
