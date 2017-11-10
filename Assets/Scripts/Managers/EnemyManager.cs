using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyManager : MonoBehaviour {

	public GameObject enemy;
	public Transform[] spawnPoints;

	float loopTime;
	GameObject newSpawn;
	public float spawnTime;
	int spawnPointIndex;
	PlayerHealth playerHealth;

	void Start()
	{
		spawnTime = 1.5f;
		//InvokeRepeating calls the Spawn() method every spawnTime seconds (1.5 seconds).
		playerHealth = GameObject.Find("Player").GetComponent<PlayerHealth> ();
		Invoke("Spawn", spawnTime);
	}

	void Spawn()
	{
		//First, add a catch so that enemies stop spawning when the player is dead.
		if (playerHealth.currentHealth <= 0f)
			return;

		//Choose a random number from 0 to the amount of spawn points, and Instantiate an enemy at that spawn point.
		//Only spawn an enemy if the current powerup is not freeze. Also, if an enemy spawns while Slow is active, slow their speed on spawn.
		spawnPointIndex = Random.Range (0, spawnPoints.Length);
		if(PowerupManager.currentPowerup != "Freeze")
			newSpawn = Instantiate (enemy, spawnPoints[spawnPointIndex].position, spawnPoints[spawnPointIndex].rotation);

		if (PowerupManager.currentPowerup == "Slow")
			newSpawn.GetComponent<NavMeshAgent> ().speed = 1.5f;

		Invoke ("Spawn", spawnTime);
	}
}
