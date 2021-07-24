using UnityEngine;

public class Wizard : Enemy {

	private void Start()
	{
		Setup(160, 2.5f);
	}

	private void Update()
	{
		//Active is set to false on death, which prevents this all from happening.
		if (active) {
			transform.LookAt(playerTrans);
			if (Vector3.Distance (transform.position, playerTrans.position) <= 20) {
				if (!playerInRange) {
					playerInRange = true;
					animator.SetBool ("Idle", false);
					animator.SetBool ("Attacking", true);
				}

				attackTimer += Time.deltaTime;
				if (attackTimer >= coolDown) {
					attackTimer = 0;
					Invoke ("Shoot", 0f);
					Invoke ("Shoot", .1f);
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
