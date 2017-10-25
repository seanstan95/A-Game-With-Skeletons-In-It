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
    public GameObject powerUpItem;

	Animator anim;
	bool sinking, drop;
	EnemyManager manager;
	float destroyTimer;
	GameObject enemy;

	void Start () {
		currentHealth = startHealth;
		anim = GetComponent<Animator> ();
		enemy = GameObject.FindGameObjectWithTag ("Manager");
		manager = enemy.GetComponent<EnemyManager>();
    }

	void Update(){
		if (currentHealth <= 0){
			destroyTimer += Time.deltaTime;
			if (!drop) {
				Instantiate (powerUpItem, transform.position, transform.rotation);
				drop = true;
			}
        }

        if (sinking)
            transform.Translate(-Vector3.up * sinkSpeed * Time.deltaTime);
        if (destroyTimer > 2.5f)
            sinking = true;
        if (destroyTimer > 3f)
            Destroy(gameObject);
    }
	
	public void TakeDamage(int amount)
	{
		currentHealth -= amount;

		if (currentHealth <= 0) {
			anim.SetBool ("Walking", false);
			anim.SetTrigger ("Dead");	
			manager.DecreaseTimer();
			GetComponent<UnityEngine.AI.NavMeshAgent>().enabled = false;
			GetComponent<Rigidbody>().isKinematic = true;
			GetComponent<CapsuleCollider> ().enabled = false;
			ScoreManager.score += scoreValue;
		}
	}
}
