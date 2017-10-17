using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour {

	public float timeBetweenAttacks = 2f;
	public int attackDamage = 10;

	Animator anim;
	bool playerInRange;
	EnemyHealth enemyHealth;
	float timer;
	GameObject player;
	PlayerHealth playerHealth;

	void Start () 
	{
		anim = GetComponent<Animator> ();
		enemyHealth = GetComponent<EnemyHealth>();
		player = GameObject.FindGameObjectWithTag ("Player");
		playerHealth = player.GetComponent <PlayerHealth> ();
	}


	void OnTriggerEnter (Collider other)
	{
		if (other.gameObject == player) {
			playerInRange = true;
		}
	}

	void OnTriggerExit(Collider other)
	{
		if(other.gameObject == player)
			playerInRange = false;
	}
		
	void Update ()
	{
		timer += Time.deltaTime;

		if(timer >= timeBetweenAttacks && playerInRange && enemyHealth.currentHealth > 0)
			Attack();
	}

	void Attack()
	{
		if (playerHealth.currentHealth > 0) {
			playerHealth.TakeDamage (attackDamage);
		}
		timer = 0f;
	}

}
