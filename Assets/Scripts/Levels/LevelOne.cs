using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelOne : MonoBehaviour {

	public static bool continueBars;
	public static int enemyCount;
	private static GameObject skeleton7, skeleton8;

	private void Start()
	{
		skeleton7 = GameObject.Find ("Skeleton7");
		skeleton7.SetActive (false);
		skeleton8 = GameObject.Find ("Skeleton8");
		skeleton8.SetActive (false);
	}

	public static void EnemyTrigger(string trigger)
	{
		switch (trigger) {
			case "Trigger1":
				GameObject.Find ("Skeleton1").GetComponent<Skeleton> ().follow = true;
				break;
			case "Trigger2":
				GameObject.Find ("Skeleton2").GetComponent<Skeleton> ().follow = true;
				break;
			case "Trigger3":
				GameObject.Find ("Skeleton3").GetComponent<Skeleton> ().follow = true;
				GameObject.Find ("Skeleton4").GetComponent<Skeleton> ().follow = true;
				GameObject.Find ("Skeleton5").GetComponent<Skeleton> ().follow = true;
				break;
			case "Trigger4":
				GameObject.Find ("Skeleton6").GetComponent<Skeleton> ().follow = true;
				break;
		}
	}

	public static void ActivateSkeletons()
	{
		skeleton7.SetActive (true);
		skeleton7.GetComponent<Skeleton> ().follow = true;
		skeleton8.SetActive (true);
		skeleton8.GetComponent<Skeleton> ().follow = true;
	}
}
