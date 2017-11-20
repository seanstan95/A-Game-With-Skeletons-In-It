﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Skeleton : Enemy {

	public bool deathTasks, follow;

	private void Start()
	{
		coolDown = 1.33f;
		currentHealth = 100;
		damagePerHit = -10;
		player = GameObject.Find ("Player");
	}

	private void Update()
	{
		//Death() handles timing of destroying the skeleton when dead. If it returns false, the skeleton is still alive, so continue.
		if (!Death ()) {
			if (!follow) {
				GetComponent<NavMeshAgent> ().isStopped = true;
				GetComponent<Animator> ().SetBool ("Walking", false);
				GetComponent<Animator> ().SetBool ("Idle", true);
			} else {
				//If not already walking, start walking, since the player is close enough to be chased now. Same for nav mesh movement.
				if (!GetComponent<Animator> ().GetBool ("Walking")) {
					GetComponent<Animator> ().SetBool ("Walking", true);
					GetComponent<Animator> ().SetBool ("Idle", false);
				}
				if (GetComponent<NavMeshAgent> ().isStopped)
					GetComponent<NavMeshAgent> ().isStopped = false;
				
				//Check if the player is within 4 distance from the skeleton (roughly how far the sword attack animation reaches outwards).
				//If so and the player was not previously in range, we know to change from attacking to walking.
				//Vice versa if the player was in range but now isn't.
				if (Vector3.Distance (transform.position, player.transform.position) < 4f) {
					if (!playerInRange) {
						attackTimer = .50f;
						GetComponent<Animator> ().SetBool ("Walking", false);
						GetComponent<Animator> ().SetBool ("Attacking", true);
						playerInRange = true;
					}
				} else {
					if (playerInRange) {
						GetComponent<Animator> ().SetBool ("Attacking", false);
						GetComponent<Animator> ().SetBool ("Walking", true);
						playerInRange = false;
					}
				}
			}

			//If movement is allowed (player is out of range and nav mesh is not stopped)
			if (!playerInRange && follow)
				GetComponent<NavMeshAgent> ().SetDestination (player.transform.position);

			//If player is in range, begin incrementing attackTimer
			if (playerInRange)
				attackTimer += Time.deltaTime;

			//coolDown controls how fast the enemy can attack, playerInRange ensures the player is close enough, and the player must be alive.
			if (attackTimer >= coolDown && playerInRange) {
				PlayerHealth.ChangeHealth (damagePerHit);
				attackTimer = 0f;
			}
		} else {
			//If the skeleton died, check if its death should trigger another Skeleton to activate.
			//continueBars determines if one of skeleton7/8 is already dead, deathTasks determines if the 
			if (!deathTasks) {
				if (name == "Skeleton6") {
					LevelOne.ActivateSkeletons ();
				} else if ((name == "Skeleton7" || name == "Skeleton8") && !LevelOne.continueBars) {
					LevelOne.continueBars = true;
					deathTasks = true;
				} else if (name == "Skeleton7" && LevelOne.continueBars || name == "Skeleton8" && LevelOne.continueBars) {
					GameObject.Find ("IronBars1").SetActive (false);
					deathTasks = true;
				}
			}
		}
	}
}
