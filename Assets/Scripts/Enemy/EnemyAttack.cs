using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 	* Controls all aspects regarding an enemy's ability to attack the player
 	* playerInRange bool is set when a player is in range of the enemy
 	* Calls the TakeDamage function of PlayerHealth if in range and the player is able to take damage
*/

public class EnemyAttack : MonoBehaviour {

	public float timeBetweenAttacks;
	public int attackDamage;

	bool playerInRange;
	EnemyHealth enemyHealth;
	float attackTimer;
	GameObject player;
	PlayerHealth playerHealth;

	void Start () 
	{
		attackDamage = -10;
		timeBetweenAttacks = 1f;
		enemyHealth = GetComponent<EnemyHealth>();
		player = GameObject.FindGameObjectWithTag ("Player");
		playerHealth = player.GetComponent <PlayerHealth> ();
	}


	void OnTriggerEnter (Collider other)
	{
		if (other.gameObject == player)
			playerInRange = true;
	}

	void OnTriggerExit(Collider other)
	{
		if(other.gameObject == player)
			playerInRange = false;
	}
		
	void Update ()
	{
		attackTimer += Time.deltaTime;

		if (attackTimer >= timeBetweenAttacks && playerInRange && enemyHealth.currentHealth > 0) {
			if (playerHealth.currentHealth > 0)
				playerHealth.ChangeHealth (attackDamage);
			attackTimer = 0f;
		}
	}
}
