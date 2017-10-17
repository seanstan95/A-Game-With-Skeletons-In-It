using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour {

	public PlayerHealth playerHealth;
	public GameObject skeleton;
	public float spawnTime = 3f;
	public Transform[] spawnPoints;

	void Start()
	{
		InvokeRepeating ("Spawn", spawnTime, spawnTime);
	}

	void Spawn()
	{
		if (playerHealth.currentHealth <= 0f) {
			return;
		}

		int spawnPointIndex = Random.Range (0, spawnPoints.Length);
		Instantiate (skeleton, spawnPoints [spawnPointIndex].position, spawnPoints [spawnPointIndex].rotation);
	}

	public void DecreaseTimer(){
		if (spawnTime >= 0) {
			spawnTime -= .1f;
		}
	}
}
