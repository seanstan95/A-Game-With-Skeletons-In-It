using UnityEngine;

public class FinalBoss : Enemy {

	private bool size;
	private int nextTrigger = 1600;
	float newX, newY, newZ;
	public static bool shield = true;

	private void Start()
	{
		Setup(2000, 2.76f);
		playerInRange = true;
	}

	private void Update()
	{
		if (active) {
			if (!shield) {
				if (!size) {
					//Each iteration increases size by one lerp and returns out to avoid code progression, until at (10, 1.5, 10)
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

				//Regardless of player distance, continue to update the path to the player. Boss will only move when navAgent.isStopped is false.
				navAgent.SetDestination (playerTrans.position);

				if (Vector3.Distance (transform.position, playerTrans.position) > 2.5f) {
					//If here, boss is charging at the player
					if (playerInRange) {
						playerInRange = false;
						navAgent.isStopped = false;
						animator.SetBool ("Idle", false);
						animator.SetBool ("Attack", false);
						animator.SetBool ("Run", true);
					}
				} else {
					//If here, boss is attacking the player
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
                        GameManager.playerHealth.ChangeHealth(20);
                    }
				}

				if (currentHealth == nextTrigger) {
					//if health is down by 400, return to center until another target is destroyed
					if (!shield) {
                        nextTrigger -= 400;
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
