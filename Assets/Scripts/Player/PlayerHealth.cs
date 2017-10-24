using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/*
 	* Controls all aspects regarding the player's health
 	* Starts with 100 health and decreases by the value of amount
 	* If health hits 0 or lower, Game Over trigger is activated
*/

public class PlayerHealth : MonoBehaviour {

	public Color flashColour = new Color(153, 0, 204);
	public float flashSpeed = 2f;
	public Image damageImage;
	public int startHealth = 100, currentHealth;
	public Slider healthSlider;

	bool damaged;
	PlayerAttack playerAttack;
	PlayerMove playerMove;

	void Awake () {
		playerMove = GetComponent<PlayerMove> ();
		playerAttack = GetComponent<PlayerAttack> ();
		currentHealth = startHealth;
	}

	void Update () {
		if (damaged)
			damageImage.color = flashColour;
		else
			damageImage.color = Color.Lerp (damageImage.color, Color.clear, flashSpeed * Time.deltaTime);
		
		damaged = false;
	}

	public void TakeDamage(int amount)
	{
		damaged = true;
		currentHealth -= amount;
		healthSlider.value = currentHealth;

		if(currentHealth <= 0){
			playerMove.enabled = false;
			playerAttack.enabled = false;
		}
	}
}
