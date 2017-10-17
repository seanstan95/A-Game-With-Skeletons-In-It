using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour {

	public int startHealth;
	public int currentHealth;
	public int scoreValue = 10;
	Animator anim;

	void Start () {
		anim = GetComponent<Animator> ();
		currentHealth = startHealth;
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
		anim.SetBool ("Dead", true);
	}
}
