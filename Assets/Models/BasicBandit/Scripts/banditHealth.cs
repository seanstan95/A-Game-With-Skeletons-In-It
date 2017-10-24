using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class banditHealth : MonoBehaviour
{
	public int startingHealth = 100;
	public int currentHealth;
	public float sinkSpeed = 2.5f;
	public int scoreValue = 10;

	Animator anim;
	CapsuleCollider capsuleCollider;

	bool isDead;
	bool isSinking;


	void Start ()
	{
		anim = GetComponent <Animator> ();
		capsuleCollider = GetComponent <CapsuleCollider> ();
		currentHealth = startingHealth;
	}


	void Update ()
	{
		if(isSinking)
		{
			transform.Translate (-Vector3.up * sinkSpeed * Time.deltaTime);
		}
	}


	public void TakeDamage (int amount)
	{
		if(isDead)
			return;

		currentHealth -= amount;

		if(currentHealth <= 0)
			Death ();
		
	}


	void Death ()
	{
		isDead = true;

		capsuleCollider.isTrigger = true;

		anim.SetTrigger ("IdleGrab_Neutral");


	}


	public void StartSinking ()
	{
		GetComponent <UnityEngine.AI.NavMeshAgent> ().enabled = false;
		GetComponent <Rigidbody> ().isKinematic = true;
		isSinking = true;
		//ScoreManager.score += scoreValue; // Score tracker
		Destroy (gameObject, 2f);
	}
}



