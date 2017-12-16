using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelOne : MonoBehaviour {

	private bool end;
	private float endTimer;
	public static int enemyCount;
	public GameObject ironBars;
	public Skeleton[] skeletons;

	private void Update()
	{
		switch (enemyCount) {
			case 8:
				GameObject.Find ("Skeleton9").GetComponent<Skeleton> ().active = true;
				GameObject.Find ("Skeleton10").GetComponent<Skeleton> ().active = true;
				break;
			case 10:
				if (ironBars.activeSelf)
					ironBars.SetActive (false);
				break;
			case 12:
				GameObject.Find ("Skeleton13").GetComponent<Skeleton> ().active = true;
				GameObject.Find ("Skeleton14").GetComponent<Skeleton> ().active = true;
				break;
			case 14:
				GameObject.Find ("Skeleton15").GetComponent<Skeleton> ().active = true;
				GameObject.Find ("Skeleton16").GetComponent<Skeleton> ().active = true;
				break;
			case 16:
				GameObject.Find ("SkeletonBoss").GetComponent<Skeleton> ().active = true;
				break;
			case 17:
				if (endTimer < 1) {
					endTimer += Time.deltaTime;
				} else if(!end){
					GameObject.Find ("HUD").GetComponent<Animator> ().SetTrigger ("LevelComplete");
					GameManager.SetState ("LVLONED");
					end = true;
				}
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
				break;
			case "Trigger2":
				GameObject.Find ("Skeleton2").GetComponent<Skeleton> ().active = true;
				GameObject.Find ("Skeleton3").GetComponent<Skeleton> ().active = true;
				break;
			case "Trigger3":
				GameObject.Find ("Skeleton4").GetComponent<Skeleton> ().active = true;
				GameObject.Find ("Skeleton5").GetComponent<Skeleton> ().active = true;
				GameObject.Find ("Skeleton6").GetComponent<Skeleton> ().active = true;
				break;
			case "Trigger4":
				GameObject.Find ("Skeleton7").GetComponent<Skeleton> ().active = true;
				GameObject.Find ("Skeleton8").GetComponent<Skeleton> ().active = true;
				break;
			case "Trigger5":
				GameObject.Find ("Skeleton11").GetComponent<Skeleton> ().active = true;
				GameObject.Find ("Skeleton12").GetComponent<Skeleton> ().active = true;
				break;
		}
	}
}
