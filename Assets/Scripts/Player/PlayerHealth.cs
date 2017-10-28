using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour {

	public Image damageImage;
	public int currentHealth;

	bool damaged;
	float flashSpeed = 2f;
	Text hText;

	void Start () {
		currentHealth = 100;
		hText = GameObject.FindGameObjectWithTag ("HText").GetComponent<Text> ();
	}

	void Update () {
		//If the player has been damaged in the last frame, set the color of the damageImage to be red.
		//Then, lerp from red to clear over flashSpeed * Time.deltaTime
		if (damaged)
			damageImage.color = new Color(250, 0, 0);
		damageImage.color = Color.Lerp (damageImage.color, Color.clear, flashSpeed * Time.deltaTime);
		damaged = false;
	}

	public void ChangeHealth(int amount)
	{
		//Previously this function was TakeDamage, but it proved more convenient to have one function after the Health powerup was added.
		//Because this function handles taking damage and adding health, taking damage is when amount is negative, and adding health is when amount is positive.
		if (amount < 0)
			damaged = true;
			
		//Either way, adding amount to currentHealth will update it correctly (adding a negative is the same as subtracting).
		//Also, ensure that currentHealth doesn't go above 100 if we just added health. Then, adjust the health slider accordingly.
		currentHealth += amount;
		if (currentHealth > 100)
			currentHealth = 100;
		
		hText.text = "Health: " + currentHealth + "/100";

		//On death, disable the PlayerMove and PlayerAttack scripts.
		if(currentHealth <= 0){
			GetComponent<PlayerMove>().enabled = false;
			GetComponent<PlayerAttack>().enabled = false;
		}
	}
}
