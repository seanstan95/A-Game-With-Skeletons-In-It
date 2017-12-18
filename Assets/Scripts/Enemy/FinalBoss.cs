using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinalBoss : Enemy {

	public static bool shield = true;
	private bool size;
	private int nextTrigger = 800;
	private float newX, newY, newZ;

	private void Start()
	{
		animator = GetComponent<Animator> ();
		capsule = GetComponent<CapsuleCollider> ();
		coolDown = 2.76f;
		maxHealth = 1000;
		currentHealth = (int)maxHealth;
		playerInRange = true;
		damagePerHit = -20;
	}

	private void Update()
	{
		if (!Death () && active) {
			if (!shield) {
				if (!size) {
					//lerps size increase until it reaches (10, 1.5, 10)
					//each iteration increases it by one lerp and returns out to avoid code progression, until at (10, 1.5, 10)
					if (transform.localScale.x < 10)
						newX = transform.localScale.x + Time.deltaTime * 7;
					if (transform.localScale.y < 1.5)
						newY = transform.localScale.y + Time.deltaTime;
					if (transform.localScale.z < 10)
						newZ = transform.localScale.z + Time.deltaTime * 7;
					transform.localScale = Vector3.Lerp (transform.localScale, new Vector3 (newX, newY, newZ), .75f);

					if (transform.localScale.x >= 10 && transform.localScale.y >= 1.5 && transform.localScale.z >= 10) {
						size = true;
					} else {
						return;
					}
				}

				//Regardless of player distance, continue to update the path to the player. Boss will only move when agent isStopped is false.
				navAgent.SetDestination (player.transform.position);

				if (Vector3.Distance (transform.position, player.transform.position) > 2.5f) {
					//if here, boss is charging at the player
					if (playerInRange) {
						playerInRange = false;
						navAgent.isStopped = false;
						animator.SetBool ("Idle", false);
						animator.SetBool ("Attack", false);
						animator.SetBool ("Run", true);
					}
				} else {
					//if here, boss is attacking the player
					if (!playerInRange) {
						playerInRange = true;
						navAgent.isStopped = true;
						attackTimer = 1.76f;
						animator.SetBool ("Idle", false);
						animator.SetBool ("Run", false);
						animator.SetBool ("Attack", true);
					}
				}

				if (playerInRange) {
					attackTimer += Time.deltaTime;
					if (attackTimer >= coolDown) {
						attackTimer = 0;
						PlayerHealth.ChangeHealth (damagePerHit);
					}
				}

				if (currentHealth == nextTrigger) {
					//if health is down 1/5th, return to center until another target is destroyed
					if (!shield) {
						switch (currentHealth) {
							case 800:
								nextTrigger = 600;
								break;
							case 600:
								nextTrigger = 400;
								break;
							case 400:
								nextTrigger = 200;
								break;
							case 200:
								nextTrigger = 0;
								break;
						}
						navAgent.isStopped = true;
						playerInRange = true;
						transform.localPosition = new Vector3 (-0.3f, -13, 0);
						transform.localScale = new Vector3 (5, .75f, 5);
						size = false;
						shield = true;
						animator.SetBool ("Attack", false);
						animator.SetBool ("Run", false);
						animator.SetBool ("Idle", true);
					}
				}
			}
		}
	}
}
