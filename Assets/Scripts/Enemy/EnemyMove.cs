using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 	* Controls all aspects regarding an enemy's ability to move
 	* Timer first delays movement until standing-up animation is finished
 	* Nav Mesh Agent sets the destination to be the player unless either the player or enemy is dead
*/

public class EnemyMove : MonoBehaviour {

	Animator anim;
	EnemyHealth enemyHealth;
	float animTimer, standTime = 2f;
	GameObject player;
	PlayerHealth playerHealth;
	UnityEngine.AI.NavMeshAgent navAgent;

	void Awake()
	{
		anim = GetComponent<Animator> ();
		enemyHealth = GetComponent<EnemyHealth> ();
		player = GameObject.FindGameObjectWithTag ("Player");
		playerHealth = player.GetComponent<PlayerHealth> ();
		navAgent = GetComponent <UnityEngine.AI.NavMeshAgent> ();
	}

	void Update()
	{
		animTimer += Time.deltaTime;
		if (animTimer < standTime) 
			return;
			
		if (enemyHealth.currentHealth > 0 && playerHealth.currentHealth > 0) {
			anim.SetBool ("Walking", true);
			navAgent.SetDestination (player.transform.position);
		} else {
			navAgent.enabled = false;
		}
	}
}
