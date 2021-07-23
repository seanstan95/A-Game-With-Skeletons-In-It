using UnityEngine;

public class Skeleton : Enemy {

	private float hitRange, lerpFinish, lerpTimer;
	private int damagePerHit = 10;
	private Vector3 newPos;

	private void Start()
	{
		//Differentiate between normal and boss skeleton information (logic is the same beyond this point, don't need separate classes for it)
		if (tag == "NormalEnemy")
        {
			Setup(120, 1.33f);
			lerpFinish = .82f;
			hitRange = 4f;
		}
		else if(tag == "BossEnemy")
        {
			Setup(600, 1.33f);
			lerpFinish = 1.35f;
			hitRange = 5.2f;
		}

		newPos = new Vector3 (transform.position.x, 1.5f, transform.position.z);
	}

	private void Update()
	{
		//Active is set to true on enemy activation and false on death.
		if (active) {
			//This section controls how long to wait for the skeleton's upwards lerp to finish.
			if (lerpTimer <= lerpFinish && Time.timeScale == 1) {
				lerpTimer += Time.deltaTime;
				transform.position = Vector3.Lerp (transform.position, newPos, .01f);
				return;
			} else if (!navAgent.enabled) {
				navAgent.enabled = true;
				animator.SetBool ("Walking", true);
			}

			//Regardless of player position, continue to update the path to the player. Skeleton will only move when navAgent.isStopped is false.
			navAgent.SetDestination (playerTrans.position);

			//Check if the player is in range to be hit by the skeleton (roughly how far the sword attack animation reaches outwards).
			if (Vector3.Distance (transform.position, playerTrans.position) < hitRange) {
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
