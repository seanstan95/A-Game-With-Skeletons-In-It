using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour {

	public GameObject enemy;
	public Transform[] spawnPoints;

	float spawnTime = 1.5f;
	int spawnPointIndex;
	PlayerHealth playerHealth;

	void Start()
	{
		//InvokeRepeating calls the Spawn() method every spawnTime seconds (1.5 seconds).
		playerHealth = GameObject.FindGameObjectWithTag ("Player").GetComponent<PlayerHealth> ();
		InvokeRepeating ("Spawn", spawnTime, spawnTime);
	}

	void Spawn()
	{
		//First, add a catch so that enemies stop spawning when the player is dead.
		if (playerHealth.currentHealth <= 0f)
			return;

		//Choose a random number from 0 to the amount of spawn points, and Instantiate an enemy at that spawn point.
		spawnPointIndex = Random.Range (0, spawnPoints.Length);
		Instantiate (enemy, spawnPoints[spawnPointIndex].position, spawnPoints[spawnPointIndex].rotation);
	}
}
