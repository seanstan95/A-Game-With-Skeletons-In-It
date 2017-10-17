using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMove : MonoBehaviour {
	Transform player;
	PlayerHealth playerHealth;
	EnemyHealth enemyHealth;
	UnityEngine.AI.NavMeshAgent nav;
	Animator anim;
	float timeDead;

	void Awake()
	{
		anim = GetComponent<Animator> ();
		player = GameObject.FindGameObjectWithTag ("Player").transform;
		playerHealth = player.GetComponent<PlayerHealth> ();
		enemyHealth = GetComponent<EnemyHealth> ();
		nav = GetComponent <UnityEngine.AI.NavMeshAgent> ();
	}

	void Update()
	{
		if (enemyHealth.currentHealth > 0 && playerHealth.currentHealth > 0) {
			nav.SetDestination (player.position);
		} else {
			nav.enabled = false;
		}

		bool isDead = anim.GetBool ("Dead");
		if (isDead) {
			timeDead += Time.deltaTime;
		}
		if (timeDead > 20) {
			Destroy (gameObject);
		}
	}
}
