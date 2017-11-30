using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelThree : MonoBehaviour {

	public static int enemyCount;

	private void Start()
	{
		
	}

	private void Update()
	{
		if(enemyCount == 7)
			GameObject.Find ("IronBars").SetActive (false);
	}

	public static void EnemyTrigger(string trigger)
	{
		//This function handles activating enemies from their stationary position once the player hits certain trigger points.
		switch (trigger) {
			case "Trigger1":
				GameObject.Find ("Skeleton1").GetComponent<Skeleton> ().follow = true;
				GameObject.Find ("Skeleton2").GetComponent<Skeleton> ().follow = true;
				GameObject.Find ("Skeleton3").GetComponent<Skeleton> ().follow = true;
				GameObject.Find ("Skeleton4").GetComponent<Skeleton> ().follow = true;
				GameObject.Find ("SkeletonBoss").GetComponent<SkeletonBoss> ().follow = true;
				break;
		}
	}
}
