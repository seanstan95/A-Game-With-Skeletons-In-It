using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Wizard : Enemy {

	private void Start()
	{
		animator = GetComponent<Animator> ();
		capsule = GetComponent<CapsuleCollider> ();
		coolDown = 2f;
		maxHealth = 150;
		currentHealth = (int)maxHealth;
	}

	private void Update()
	{
		//Death() handles timing of destroying the wizard when dead. If it returns false, the wizard is still alive, so continue.
		if (!Death ()) {
			attackTimer += Time.deltaTime;

			//Regardless of whether tracking the player or not, check if the player is close enough to be shot at.
			if (Vector3.Distance (transform.position, player.transform.position) <= 20) {
				if (!playerInRange) {
					playerInRange = true;
					animator.SetBool ("Idle", false);
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
					animator.SetBool ("Idle", true);
				}
			}
		}
	}
}
