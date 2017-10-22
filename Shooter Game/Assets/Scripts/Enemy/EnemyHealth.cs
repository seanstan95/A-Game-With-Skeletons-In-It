using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 	* Controls all aspects regarding an enemy's health
 	* Starts with 100 health and decreases by the value of amount
 	* If health hits 0 or lower, death animation is triggered. Timer in Update() destroys the game object 4 seconds after death
*/

public class EnemyHealth : MonoBehaviour {

	public int startHealth = 100, currentHealth, scoreValue = 10;
    public float sinkSpeed = 2.5f;
    public GameObject tempItem;
    bool sinking = false;
    bool spawnedDrop = false;

	Animator anim;
	EnemyManager manager;
	GameObject enemy;
	float sinkTimer = 3f, deathTimer = 4f, destroyTimer;


	void Start () {
		currentHealth = startHealth;
		anim = GetComponent<Animator> ();
		enemy = GameObject.FindGameObjectWithTag ("Manager");
		manager = enemy.GetComponent<EnemyManager>();
        
    }

	void Update(){
        if (currentHealth <= 0)
        {
            destroyTimer += Time.deltaTime;
            GetComponent<UnityEngine.AI.NavMeshAgent>().enabled = false;
            GetComponent<Rigidbody>().isKinematic = true;
            if(spawnedDrop == false)
            {
                Instantiate(tempItem, transform.position, transform.rotation);
            }
            
            
        }

        if (sinking)
        {
            transform.Translate(-Vector3.up * sinkSpeed * Time.deltaTime);
        }

        if (destroyTimer >= sinkTimer)
        {
            sinking = true;
            //Destroy(gameObject);
        }

        if (destroyTimer >= deathTimer)
        {
            //sinking = true;
            Destroy(gameObject);
        }
    }
	
	public void TakeDamage(int amount)
	{
		if (currentHealth <= 0)
			return;

		anim.SetTrigger ("Hit");
		currentHealth -= amount;

		if (currentHealth <= 0) {
			anim.SetBool ("Walking", false);
			anim.SetTrigger ("Dead");
			manager.DecreaseTimer();
			ScoreManager.score += scoreValue;
		}
	}
}
