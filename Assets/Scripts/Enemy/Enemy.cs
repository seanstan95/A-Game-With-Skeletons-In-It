using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour {

	private bool count, dead, drop;
	private float destroyTimer;
	protected Animator animator;
	protected bool playerInRange, sinking;
	protected float attackTimer, coolDown;
	protected GameObject player;
	protected int currentHealth, damagePerHit, powerupIndex;
	protected NavMeshAgent navAgent;

	protected bool Death()
	{
		//If the enemy is dead, disable collider and stop movement.
		if (currentHealth <= 0) {
			if (GetComponent<CapsuleCollider> ().enabled && !navAgent.isStopped) {
				GetComponent<CapsuleCollider> ().enabled = false;
				navAgent.isStopped = true;
			}

			//If the death trigger hasn't happened yet, do so now.
			if (!dead) {
				animator.SetTrigger ("Die");
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
				Destroy (gameObject);

			return true;
		} else {
			return false;
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
