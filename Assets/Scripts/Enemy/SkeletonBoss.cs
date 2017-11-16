using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletonBoss : Enemy {

	private void Start()
	{
		attackTimer = .50f;
		coolDown = 1.33f;
		currentHealth = 200;
		damagePerHit = -30;
		playerHealth = GameObject.Find ("Player").GetComponent<PlayerHealth> ();
		GetComponent<Animator> ().SetBool ("Walking", true);
	}

	private void Update()
	{
		DeathTasks();

		//First of all, none of the below matters unless both the player and skeleton are alive.
		if (currentHealth > 0 && PlayerHealth.currentHealth > 0) {

			//Check if the player is within 4.25 distance from the skeleton (roughly how far the sword attack animation reaches outwards).
			//If so and the player was not previously in range, we know to change from attacking to walking.
			//Vice versa if the player was in range but now isn't.
			if (Vector3.Distance (transform.position, playerHealth.gameObject.transform.position) < 4.25f) {
				if (!playerInRange) {
					GetComponent<Animator> ().SetBool ("Walking", false);
					GetComponent<Animator> ().SetBool ("Attack", true);
					playerInRange = true;
				}
			} else {
				if (playerInRange) {
					GetComponent<Animator> ().SetBool ("Attack", false);
					GetComponent<Animator> ().SetBool ("Walking", true);
					playerInRange = false;
				}
			}

			//If movement is allowed (player is out of range and nav mesh is not stopped)
			if(!playerInRange && GetComponent<UnityEngine.AI.NavMeshAgent>().isStopped == false)
				GetComponent<UnityEngine.AI.NavMeshAgent>().SetDestination (playerHealth.gameObject.transform.position);

			//If player is in range, begin incrementing attackTimer
			if(playerInRange)
				attackTimer += Time.deltaTime;

			//coolDown controls how fast the enemy can attack, playerInRange ensures the player is close enough, and the player must be alive.
			if (attackTimer >= coolDown && playerInRange) {
				PlayerHealth.ChangeHealth (damagePerHit);
				attackTimer = 0f;
			}
		}
	}
}
