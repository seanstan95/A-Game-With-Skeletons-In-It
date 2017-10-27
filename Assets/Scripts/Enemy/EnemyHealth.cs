using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour {

	public int currentHealth;
	public GameObject[] powerups;

	bool sinking, drop;
	float destroyTimer, sinkSpeed = 2.5f;
	int powerupIndex;

	void Start () {
		//General presets.
		currentHealth = 100;
    }

	void Update(){
		//If the enemy is dead, start counting how long it's been.
		//If the enemy hasn't dropped anything yet, randomly select a powerup to drop. Currently set to a 50/50 chance to drop nothing, or one of the 3 powerups.
		if (currentHealth <= 0){
			destroyTimer += Time.deltaTime;
			if (!drop) {
				powerupIndex = Random.Range (0, powerups.Length);
				if (powerupIndex == 0 || powerupIndex == 1 || powerupIndex == 2)
					Instantiate (powerups[powerupIndex], transform.position, transform.rotation);
				drop = true;
			}
        }

		//After death, wait 2.5 seconds to allow for death animation to finish before sinking.
		//While sinking, continue to translate the position downwards for .5 seconds. Then, destroy the gameObject as it is no longer needed, and out of sight.
		if (destroyTimer > 2.5f)
			sinking = true;
        if (sinking)
            transform.Translate(-Vector3.up * sinkSpeed * Time.deltaTime);
        if (destroyTimer > 3f)
            Destroy(gameObject);
    }
	
	public void TakeDamage(int amount)
	{
		//Amount represents the amount of damage being dealt to the enemy. This will always be positive, so subtract it from currentHealth.
		currentHealth -= amount;

		//Opted for an if statement instead of a separate function for Death tasks.
		//First, walking stops and the death trigger animation is set. 
		//Then, disable the nav mesh agent so the enemy no longer follows the player. Set kinematic to be true, and disable the collider.
		if (currentHealth <= 0) {
			GetComponent<CapsuleCollider> ().enabled = false;
			GetComponent<Animator>().SetBool ("Walking", false);
			GetComponent<Animator>().SetTrigger ("Dead");	
			GetComponent<UnityEngine.AI.NavMeshAgent>().enabled = false;
			GetComponent<Rigidbody>().isKinematic = true;
		}
	}
}
