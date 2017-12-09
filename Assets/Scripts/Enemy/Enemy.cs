using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour {

	private bool count, deadTrigger;
	private float destroyTimer;
	protected Animator animator;
	protected bool playerInRange;
	protected float attackTimer, coolDown;
	protected int damagePerHit;
	public bool active;
	public CapsuleCollider capsule;
	public float maxHealth;
	public int currentHealth;
	public GameObject player, projectile, wandEnd;
	public NavMeshAgent navAgent;

	protected bool Death()
	{
		//If the enemy is dead, run through a number of tasks
		if (currentHealth <= 0) {

			//Disable collider and stop movement (if using a navmeshagent)
			if (capsule.gameObject.activeSelf) {
				capsule.enabled = false;
				if(navAgent != null)
					navAgent.isStopped = true;
			}

			//If the death animation hasn't happened yet, do so now.
			if (!deadTrigger) {
				animator.SetTrigger ("Die");
				deadTrigger = true;
			}

			if (!count) {
				//Increment the appropriate level's counter
				switch (GameManager.GetLevel ()) {
					case "LevelOne":
						LevelOne.enemyCount++;
						break;
					case "LevelThree":
						LevelThree.enemyCount++;
						break;
				}
				count = true;
			}

			//After 2 seconds, destroy the object.
			destroyTimer += Time.deltaTime;
			if (destroyTimer >= 2f)
				Destroy (gameObject);

			//Return true if enemy is dead, false if still alive
			return true;
		} else {
			return false;
		}
	}

	//Used by wizards for ranged shots
	protected void Shoot()
	{
		Instantiate (projectile, wandEnd.transform.position, transform.rotation);
	}
}
