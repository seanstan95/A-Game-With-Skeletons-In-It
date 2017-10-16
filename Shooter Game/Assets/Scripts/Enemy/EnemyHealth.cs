using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour {

	public int startHealth;
	public int currentHealth;
	public int scoreValue = 10;
	//public AudioClip dead;  No audio clips yet

	//AudioSource enemyAudio;  No audio clips yet
	//bool isDead;  Bandit doesn't have a dead animation

	// Use this for initialization
	void Start () {
		//enemyAudio = GetComponent<AudioSource> ();  No audio clips yet
		currentHealth = startHealth;
	}
	
	public void TakeDamage(int amount, Vector3 hitpoint)
	{
		if (currentHealth <= 0)
			return;
		
		//enemyAudio.Play ();  No audio clips yet
		currentHealth -= amount;
		if (currentHealth <= 0)
			Death ();
	}

	void Death()
	{
		Destroy (gameObject, 0);
	}
}
