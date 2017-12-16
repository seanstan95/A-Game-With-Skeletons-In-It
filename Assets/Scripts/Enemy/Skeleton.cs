using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Skeleton : Enemy {

	private float distance, activeTime, lerpTimer;
	private Vector3 newPos;

	private void Start()
	{
		animator = GetComponent<Animator> ();
		capsule = GetComponent<CapsuleCollider> ();
		capsule.enabled = false;
		coolDown = 1.33f;
		//Differentiate between normal and boss skeleton information
		if (tag == "NormalEnemy") {
			activeTime = .82f;
			maxHealth = 120;
			damagePerHit = -10;
			distance = 4f;
		} else if (tag == "BossEnemy") {
			activeTime = 1.35f;
			if (GameManager.GetLevel () == "LevelOne")
				maxHealth = 300;
			else
				maxHealth = 500;
			damagePerHit = -20;
			distance = 5.2f;
		}
		currentHealth = (int)maxHealth;
		if(GameManager.GetLevel() == "LevelTwo")
			levelTwo = GameObject.Find ("Managers").GetComponent<LevelTwo> ();
		newPos = new Vector3 (transform.position.x, 1.5f, transform.position.z);
		player = GameObject.Find ("Player");
	}

	private void Update()
	{
		//Death() handles timing of destroying the skeleton when dead. If it returns false, the skeleton is still alive, so continue.
		if (!Death () && active) {
			//This section controls how long to wait for the skeleton's upwards lerp to finish before activating the navAgent and continuing
			if (lerpTimer <= activeTime) {
				lerpTimer += Time.deltaTime;
				transform.position = Vector3.Lerp (transform.position, newPos, .01f);
				return;
			} else if (!navAgent.enabled) {
				navAgent.enabled = true;
				capsule.enabled = true;
				animator.SetBool ("Walking", true);
			}

			//Regardless of player position, continue to update the path to the player. Skeleton will only move when agent isStopped is false.
			navAgent.SetDestination (player.transform.position);

			//Check if the player is within 4 distance from the skeleton (roughly how far the sword attack animation reaches outwards).
			if (Vector3.Distance (transform.position, player.transform.position) < distance) {
				if (!playerInRange) {
					playerInRange = true;
					navAgent.isStopped = true;
					attackTimer = .66f;
					animator.SetBool ("Walking", false);
					animator.SetBool ("Attacking", true);
				}
			} else {
				if (playerInRange) {
					playerInRange = false;
					navAgent.isStopped = false;
					animator.SetBool ("Attacking", false);
					animator.SetBool ("Walking", true);
				}
			}

			//If player is in range, begin incrementing attackTimer. coolDown controls how fast the enemy can attack.
			if (playerInRange) {
				attackTimer += Time.deltaTime;
				if (attackTimer >= coolDown) {
					PlayerHealth.ChangeHealth (damagePerHit);
					attackTimer = 0f;
				}
			}
		}
	}
}
