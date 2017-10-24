using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/*
 	* Manages the triggers for the Game Over screen
 	* Simply calls the GameOver trigger of the animator when the player dies
*/

public class GameOverManager : MonoBehaviour {

	public PlayerHealth playerHealth;

	Animator anim;

	void Awake()
	{
		anim = GetComponent<Animator> ();
	}

	void Update()
	{
		if (playerHealth.currentHealth <= 0)
			anim.SetTrigger ("GameOver");
	}
}
