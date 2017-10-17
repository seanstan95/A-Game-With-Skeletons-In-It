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
	float timer;
	float standAnim = 2;

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
		timer += Time.deltaTime;
		if (timer < standAnim) {
			return;
		}
			
		if (enemyHealth.currentHealth > 0 && playerHealth.currentHealth > 0) {
			anim.SetBool ("Walking", true);
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
