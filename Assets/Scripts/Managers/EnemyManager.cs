using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 	* Manages the enemy spawn points
 	* Calls Spawn() every spawnTime seconds, which chooses a spawn point at random to spawn an enemy from
 	* Every time an enemy is killed, spawnTime decreases slightly to increase the speed at which enemies spawn
*/

public class EnemyManager : MonoBehaviour {

	public float spawnTime = 1.5f;
	public GameObject skeleton;
	public PlayerHealth playerHealth;
	public Transform[] spawnPoints;

	int spawnPointIndex;

	void Start()
	{
		InvokeRepeating ("Spawn", spawnTime, spawnTime);
	}

	void Spawn()
	{
		if (playerHealth.currentHealth <= 0f)
			return;

		spawnPointIndex = Random.Range (0, spawnPoints.Length);
		Instantiate (skeleton, spawnPoints [spawnPointIndex].position, spawnPoints [spawnPointIndex].rotation);
	}

	public void DecreaseTimer(){
		if (spawnTime >= 0) {
			spawnTime -= .1f;
		}
	}
}
