using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class banditAttack : MonoBehaviour {


	public float timeBetweenAttacks = 0.5f;
	public int attackDamage = 10;


	Animator anim;
	GameObject player;
	//PlayerHealth playerHealth;
	banditHealth banditHealth1;
	bool playerInRange;
	float timer;




	// Use this for initialization
	void Start () 
	{

		player = GameObject.FindGameObjectWithTag ("ThirdPersonController");
		//playerHealth = player.GetComponent <PlayerHealth> ();
		banditHealth1 = GetComponent<banditHealth>();
		anim = GetComponent <Animator> ();
		
	}


	void OnTriggerEnter (Collider other)
	{
		if(other.gameObject == player)
		{
			playerInRange = true;
		}
	}

	void OnTriggerExit (Collider other)
	{
		if(other.gameObject == player)
		{
			playerInRange = false;
		}
	}
	
	// Update is called once per frame
	/*void Update ()
	{
		timer += Time.deltaTime;

		if(timer >= timeBetweenAttacks && playerInRange) //&& enemyHealth.currentHealth > 0)
		{
			banditAttack ();
		}

		if(playerHealth.currentHealth <= 0)
		{
			anim.SetTrigger ("PlayerDead");
		}
	}

	void banditAttack ()
	{
		timer = 0f;

		if(playerHealth.currentHealth > 0)
		{
			playerHealth.TakeDamage (attackDamage);
		}
	}*/

}
