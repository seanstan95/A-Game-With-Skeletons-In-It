using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour {

	public int startHealth = 100, currentHealth, scoreValue = 10;

	Animator anim;
	EnemyManager manager;
	float deathTimer = 4f, destroyTimer;
	GameObject enemyManager;

	void Start () {
		enemyManager = GameObject.FindGameObjectWithTag ("Manager");
		manager = enemyManager.GetComponent<EnemyManager> ();
		anim = GetComponent<Animator> ();
		currentHealth = startHealth;
	}

	void Update(){
		if (currentHealth <= 0) {
			destroyTimer += Time.deltaTime;
		}
		if (destroyTimer >= deathTimer) {
			Destroy (gameObject);
		}
	}
	
	public void TakeDamage(int amount, Vector3 hitpoint)
	{
		if (currentHealth <= 0)
			return;

		anim.SetTrigger ("Hit");
		currentHealth -= amount;
		if (currentHealth <= 0)
			Death ();
	}

	void Death()
	{
		anim.SetBool ("Walking", false);
		anim.SetTrigger ("Dead");
		manager.DecreaseTimer();
	}
}
