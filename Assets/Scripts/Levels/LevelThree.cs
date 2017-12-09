using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelThree : MonoBehaviour {

	public static int enemyCount;
	private GameObject ironBars;
	private Skeleton skeleBoss;
	private WizardBoss wizBoss;

	private void Start()
	{
		skeleBoss = GameObject.Find ("SkeletonBoss").GetComponent<Skeleton> ();
		wizBoss = GameObject.Find ("WizardBoss").GetComponent<WizardBoss> ();
		ironBars = GameObject.Find ("IronBars");
	}

	private void Update()
	{
		switch (enemyCount) {
			case 4:
				if (!skeleBoss.active)
					skeleBoss.active = true;
				break;
			case 7:
				if (ironBars.activeSelf)
					ironBars.SetActive (false);
				break;
			default:
				break;
		}
	}

	public static void EnemyTrigger(string trigger)
	{
		//This function handles activating enemies from their stationary position once the player hits certain trigger points.
		switch (trigger) {
			case "Trigger1":
				GameObject.Find ("Skeleton1").GetComponent<Skeleton> ().active = true;
				GameObject.Find ("Skeleton2").GetComponent<Skeleton> ().active = true;
				GameObject.Find ("Skeleton3").GetComponent<Skeleton> ().active = true;
				GameObject.Find ("Skeleton4").GetComponent<Skeleton> ().active = true;
				break;
		}
	}
}
