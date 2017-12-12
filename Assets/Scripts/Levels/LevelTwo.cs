using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelTwo : MonoBehaviour {

	private int spawn;
	public int enemyCount;
	public GameObject[] spawns;
	public GameObject enemy, newSpawn;

	private void Start()
	{
		Spawn ();
	}

	private void Update()
	{
		if (enemyCount == 10) {
			if (GameObject.Find ("IronBars1").activeInHierarchy) {
				GameObject.Find ("IronBars1").SetActive (false);
				GameObject.Find ("IronBars2").SetActive (false);
				GameObject.Find ("IronBars3").SetActive (false);
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
