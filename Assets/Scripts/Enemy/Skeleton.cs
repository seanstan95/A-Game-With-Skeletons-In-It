using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Skeleton : Enemy {

	void Start()
	{
		damagePerHit = -10;
		currentHealth = 100;
	}

	void Update()
	{
		ParentUpdate ("Fall");

		//As long as the player and enemy are both still alive, continue nav mesh movement, unless Freeze powerup is active.
		if (currentHealth > 0 && playerHealth.currentHealth > 0) {
			if(PowerupManager.currentPowerup != "Freeze")
				//GetComponent<Animator>().SetBool("Walking", true);
			if(!playerInRange && GetComponent<NavMeshAgent>().isStopped == false)
				GetComponent<NavMeshAgent>().SetDestination (playerHealth.gameObject.transform.position);
		}
			
		attackTimer += Time.deltaTime;
		
		//attackTimer controls how fast the enemy can attack, playerInRange ensures the player is close enough, and the player must be alive.
		if (attackTimer >= coolDown && playerInRange && playerHealth.currentHealth > 0) {
			playerHealth.ChangeHealth (damagePerHit);
			attackTimer = 0f;
		}
	}

	void OnCollisionEnter (Collision other)
	{
		//Checks if an object has entered the collider attached to this enemy. If it is the player, we know the player is in range of being attacked.
		if (other.gameObject == playerHealth.gameObject) {
			playerInRange = true;
			GetComponent<Animator> ().SetBool ("Walking", false);
			GetComponent<Animator> ().SetBool ("Attack", true);
			GetComponent<NavMeshAgent> ().isStopped = true;
		}
	}

	void OnCollisionExit(Collision other)
	{
		//Checks if an object has exited the collider attached to this enemy. If it is the player, we know the player is out of range of being attacked.
		if (other.gameObject == playerHealth.gameObject) {
			playerInRange = false;
			GetComponent<Animator> ().SetBool ("Attack", false);
			GetComponent<Animator> ().SetBool ("Walking", true);
			if(PowerupManager.currentPowerup != "Freeze")
				GetComponent<NavMeshAgent> ().isStopped = false;
		}
	}
}
