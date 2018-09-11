using UnityEngine;

/*
    * This class was originall used in a now unused scene for testing enemy spawning and powerup interaction with enemies.
    * After powerups were removed from the final version (and debugging was finished) the scene, and this script, were no longer needed.
*/
public class EnemyManager : MonoBehaviour {

	private int spawnPointIndex;
	public GameObject enemy;
	public static float spawnTime;
	public Transform[] spawnPoints;

	private void Start()
	{
		spawnTime = 1.5f;
		Invoke("Spawn", spawnTime);
	}

	private void Spawn()
	{
		//First, add a catch so that enemies stop spawning when the player is dead.
		if (PlayerHealth.currentHealth <= 0f)
			return;

		//Choose a random number from 0 to the amount of spawn points, and Instantiate an enemy at that spawn point.
		//Don't spawn any enemies if Freeze powerup is active.
		spawnPointIndex = Random.Range (0, spawnPoints.Length);
        Instantiate (enemy, spawnPoints[spawnPointIndex].position, spawnPoints[spawnPointIndex].rotation);

		//Spawn() gets re-called every spawnTime seconds (initially 1.5 but is adjusted when Freeze powerup is active).
		Invoke ("Spawn", spawnTime);
	}
}
