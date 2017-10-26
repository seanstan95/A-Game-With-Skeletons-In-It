using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyMove : MonoBehaviour {

	float animTimer, standTime = 2f;
	PlayerHealth playerHealth;

	void Start()
	{
		playerHealth = GameObject.FindGameObjectWithTag ("Player").GetComponent<PlayerHealth>();
	}

	void Update()
	{
		//The Skeleton standing-up animation lasts 2.5 seconds, and we don't want the enemy moving before it's done. standTime controls this.
		if (animTimer < standTime) {
			animTimer += Time.deltaTime;
			return;
		}
			
		//Nav Mesh Agent should only work when both the enemy and player are alive.
		if (GetComponent<EnemyHealth>().currentHealth > 0 && playerHealth.currentHealth > 0) {
			GetComponent<Animator>().SetBool ("Walking", true);
			GetComponent<NavMeshAgent>().SetDestination (playerHealth.gameObject.transform.position);
		}
	}
}
