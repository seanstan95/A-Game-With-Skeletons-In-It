using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {

	private bool dead, drop;
	private float destroyTimer;
	protected bool playerInRange, sinking;
	protected float attackTimer, coolDown;
	protected int currentHealth, damagePerHit, powerupIndex;
	protected PlayerHealth playerHealth;

	protected void DeathTasks()
	{
		//If the enemy is dead, disable collider and stop movement.
		if (currentHealth <= 0){
			GetComponent<CapsuleCollider> ().enabled = false;
			GetComponent<UnityEngine.AI.NavMeshAgent>().isStopped = true;

			//If the death trigger hasn't happened yet, do so now.
			if (!dead) {
				GetComponent<Animator> ().SetTrigger ("Die");
				dead = true;
			}

			//If no random item has been chosen yet, do so now.
			if (!drop) {
				powerupIndex = Random.Range (0, PowerupManager.powerups.Length);
				Instantiate (PowerupManager.powerups [powerupIndex], transform.position, PowerupManager.powerups [powerupIndex].transform.rotation);
				drop = true;
			}

			//After 2 seconds, destroy the object.
			destroyTimer += Time.deltaTime;
			if (destroyTimer >= 2f)
				Destroy(gameObject);
		}
	}

	public void TakeDamage(int amount)
	{
		currentHealth -= amount;
	}

	public int getHealth()
	{
		return currentHealth;
	}
}
