using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverManager : MonoBehaviour {

	PlayerHealth playerHealth;

	void Start()
	{
		playerHealth = GameObject.FindGameObjectWithTag ("Player").GetComponent<PlayerHealth> ();
	}

	void Update()
	{
		//Simply triggers the GameOver animation when the player dies.
		if (playerHealth.currentHealth <= 0)
			GetComponent<Animator>().SetTrigger ("GameOver");
	}
}
