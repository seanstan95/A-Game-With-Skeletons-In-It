using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour {

	public int attackDamage;

	public bool playerInRange;
	float attackTimer, timeBetweenAttacks = 1f;
	PlayerHealth playerHealth;

	void Start () 
	{
		//General presets. Attack damage is -10 because ChangeHealth can be positive (for health powerup adding health) or negative (for taking damage or decreasing health)
		attackDamage = -10;
		playerHealth = GameObject.FindGameObjectWithTag ("Player").GetComponent<PlayerHealth> ();
	}


	void OnCollisionEnter (Collision other)
	{
		//Checks if an object has entered the collider attached to this enemy. If it is the player, we know the player is in range of being attacked.
		if (other.gameObject == playerHealth.gameObject) {
			playerInRange = true;
			GetComponent<UnityEngine.AI.NavMeshAgent> ().enabled = false;
		}
	}

	void OnCollisionExit(Collision other)
	{
		//Checks if an object has exited the collider attached to this enemy. If it is the player, we know the player is out of range of being attacked.
		if (other.gameObject == playerHealth.gameObject) {
			playerInRange = false;
			GetComponent<UnityEngine.AI.NavMeshAgent> ().enabled = true;
		}
	}
		
	void Update ()
	{
		//Using Time.deltaTime ensures 1 second is counted properly regardless of framerate.
		attackTimer += Time.deltaTime;

		//attackTimer controls how fast the enemy can attack, playerInRange ensures the player is close enough, and the player must be alive.
		if (attackTimer >= timeBetweenAttacks && playerInRange && playerHealth.currentHealth > 0) {
			playerHealth.ChangeHealth (attackDamage);
			attackTimer = 0f;
		}
	}
}
