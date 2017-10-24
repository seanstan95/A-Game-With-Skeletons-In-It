using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class banditAttack : MonoBehaviour {
	
	public float timeBetweenAttacks = 0.5f;
	public int attackDamage = 10;

	//Animator anim;
	GameObject player;
	PlayerHealth playerHealth;
	EnemyHealth enemyHealth;
	bool playerInRange;
	float timer;

	// Use this for initialization
	void Start () 
	{
		player = GameObject.FindGameObjectWithTag ("Player");
		playerHealth = player.GetComponent <PlayerHealth> ();
		enemyHealth = GetComponent<EnemyHealth>();
		//anim = GetComponent<Animator> ();
	}


	void OnTriggerEnter (Collider other)
	{
		if(other.gameObject == player){
			playerInRange = true;
		}
	}

	void OnTriggerExit(Collider other)
	{
		if(other.gameObject == player){
			playerInRange = false;
		}
	}
	
	// Update is called once per frame
	void Update ()
	{
		timer += Time.deltaTime;

		if(timer >= timeBetweenAttacks && playerInRange && enemyHealth.currentHealth > 0){
			BanditAttack();
		}
	}

	void BanditAttack()
	{
		timer = 0f;

		if(playerHealth.currentHealth > 0){
			playerHealth.TakeDamage (attackDamage);
		}
	}

}
