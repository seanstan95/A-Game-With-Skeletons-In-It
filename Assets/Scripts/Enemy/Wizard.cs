using UnityEngine;

public class Wizard : Enemy {

	private void Start()
	{
		levelTwo = GameObject.Find("Managers").GetComponent<LevelTwo>();
		animator = GetComponent<Animator> ();
		capsule = GetComponent<CapsuleCollider> ();
		maxHealth = 150;
		currentHealth = maxHealth;
	}

	private void Update()
	{
		//Death() handles timing of destroying the wizard when dead. If it returns false, the wizard is still alive, so continue.
		if (!Death () && active) {
			attackTimer += Time.deltaTime;
			if (Vector3.Distance (transform.position, player.transform.position) <= 15) {
				if (!playerInRange) {
					playerInRange = true;
					animator.SetBool ("Idle", false);
					animator.SetBool ("Attacking", true);
				}
				if (attackTimer >= 2.5f) {
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
