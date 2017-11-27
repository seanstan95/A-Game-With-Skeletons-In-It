using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Wizard : Enemy {

	public bool follow;
	public GameObject end, projectile;

	private void Start()
	{
		animator = GetComponent<Animator> ();
		coolDown = 2f;
		currentHealth = 100;
		navAgent = GetComponent<NavMeshAgent> ();
		player = GameObject.Find ("Player");
	}

	private void Update()
	{
		//Death() handles timing of destroying the wizard when dead. If it returns false, the wizard is still alive, so continue.
		if (!Death ()) {
			attackTimer += Time.deltaTime;

			//if this copy of Wizard is meant to follow the player around, update tracking
			//else, disable nav mesh (avoids odd interaction with some environment areas that don't bake properly)
			if (follow) {
				if (!navAgent.isActiveAndEnabled)
					navAgent.enabled = true;
				navAgent.SetDestination (player.transform.position);
			} else {
				navAgent.enabled = false;
			}

			//Regardless of whether tracking the player or not, check if the player is close enough to be shot at.
			if (Vector3.Distance (transform.position, player.transform.position) <= 20) {
				if (!playerInRange) {
					playerInRange = true;
					animator.SetBool ("Idle", false);
					animator.SetBool ("Walking", false);
					animator.SetBool ("Attacking", true);
				}
				if (attackTimer >= coolDown) {
					attackTimer = 0;
					Invoke ("Shoot", 0f);
					Invoke ("Shoot", .1f);
					Invoke ("Shoot", .2f);
				}
			} else {
				if (playerInRange) {
					playerInRange = false;
					animator.SetBool ("Attacking", false);
					//when out of range of player, trigger the appropriate animations based on if this wizard follows the player or not
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
		Instantiate (projectile, end.transform.position, transform.rotation);
	}
}
