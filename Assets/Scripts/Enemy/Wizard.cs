using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Wizard : Enemy {

	public bool follow;
	public GameObject projectile;
	private Transform wandEnd;

	private void Start()
	{
		animator = GetComponent<Animator> ();
		coolDown = 1.33f;
		currentHealth = 100;
		damagePerHit = -10;
		navAgent = GetComponent<NavMeshAgent> ();
		player = GameObject.Find ("Player");
		wandEnd = GameObject.Find ("End").transform;
	}

	private void Update()
	{
		//Death() handles timing of destroying the wizard when dead. If it returns false, the wizard is still alive, so continue.
		if (!Death ()) {
			attackTimer += Time.deltaTime;

			//if this copy of Wizard is meant to follow the player around, update tracking
			if (follow) {
				navAgent.SetDestination (player.transform.position);
			}

			//Regardless of whether tracking the player or not, check if the player is close enough to be shot at.
			if (Vector3.Distance (transform.position, player.transform.position) <= 10) {
				if (!playerInRange) {
					playerInRange = true;
					animator.SetBool ("Idle", false);
					animator.SetBool ("Walking", false);
					animator.SetBool ("Attacking", true);
				}
				if (attackTimer >= 1.5) {
					attackTimer = 0;
					Invoke ("Shoot", 0f);
					Invoke ("Shoot", .1f);
					Invoke ("Shoot", .2f);
				}
			} else {
				if (playerInRange) {
					playerInRange = false;
					animator.SetBool ("Attacking", false);
					//when out of range of player, trigger the appripriate animations based on if this wizard follows the player or not
					if (follow) {
						animator.SetBool ("Idle", false);
						animator.SetBool ("Walking", true);
					} else {
						animator.SetBool ("Walking", false);
						animator.SetBool ("Idle", true);
					}
				}
			}
		}
	}

	private void Shoot()
	{
		Debug.Log ("Invoked shoot");
		Instantiate (projectile, wandEnd.position, wandEnd.rotation);
	}
}
