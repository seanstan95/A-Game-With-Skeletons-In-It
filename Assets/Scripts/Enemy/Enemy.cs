using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {

	protected int coolDown;
	protected PlayerHealth playerHealth;

	protected bool animating, playerInRange, drop, sinking;
	protected float attackTimer, destroyTimer;
	public GameObject[] powerups;
	protected int currentHealth, damagePerHit, powerupIndex;

	void Awake()
	{
		playerHealth = GameObject.Find ("Player").GetComponent<PlayerHealth> ();
	}

	public Enemy()
	{
		coolDown = 1;
	}

	public void ParentUpdate(string deathTrigger)
	{
		//If the enemy is dead and hasn't dropped anything yet, randomly select a powerup to drop.
		if (currentHealth <= 0){
			GetComponent<CapsuleCollider> ().enabled = false;
			GetComponent<UnityEngine.AI.NavMeshAgent>().isStopped = true;
			if(!animating)
				GetComponent<Animator> ().SetTrigger (deathTrigger);
			animating = true;

			destroyTimer += Time.deltaTime;
			if (!drop) {
				powerupIndex = Random.Range (0, powerups.Length);
				Instantiate (powerups [powerupIndex], transform.position, powerups [powerupIndex].transform.rotation);
				drop = true;
			}
		}

		//After death, wait 2 seconds to before destroying the object.
		if (destroyTimer > 2f)
			Destroy(gameObject);
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
