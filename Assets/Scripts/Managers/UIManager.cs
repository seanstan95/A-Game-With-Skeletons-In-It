using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour {

	public string currentPowerup;

	float timer, endTimer = 5f;
	PlayerAttack attack;
	PlayerHealth health;
	Text fText, dText, hText;

	void Start()
	{
		currentPowerup = "None";
		attack = GameObject.FindGameObjectWithTag ("Player").GetComponent<PlayerAttack>();
		health = GameObject.FindGameObjectWithTag ("Player").GetComponent<PlayerHealth> ();
		fText = GameObject.FindGameObjectWithTag ("FText").GetComponent<Text>();
		dText = GameObject.FindGameObjectWithTag ("DText").GetComponent<Text> ();
		hText = GameObject.FindGameObjectWithTag("HText").GetComponent<Text>();
	}

	void Update()
	{
		//As long as there is an active powerup, increase the timer every frame.
		//Whenever a powerup is finished, currentPowerup is always set back to "None" to ensure this timer works only when wanted.
		if (currentPowerup != "None")
			timer += Time.deltaTime;

		//When the timer reaches endTimer (currently a global 5 seconds, will likely be tweaked on a per-powerup basis later), deactivate the current powerup.
		//As long as currentPowerup equals one of the powerup types in the game, go to the Powerup function, sending false to deactivate.
		if (timer >= endTimer) {
			timer = 0f;
			switch (currentPowerup) {
			case "FireRate":
			case "Damage":
			case "Health":
				Powerup (false, currentPowerup);
				break;
			}
		}
	}

	void UpdateText()
	{
		//Each powerup activation will trigger the associated text to turn yellow so it's noticed that it's active.
		switch (currentPowerup) {
		case "FireRate":
			fText.text = "Fire Rate: " + attack.fireRate + "/s";
			fText.color = Color.yellow;
			break;
		case "Damage":
			dText.text = "Damage: " + attack.damagePerShot;
			dText.color = Color.yellow;
			break;
		case "Health":
			hText.text = "Health: " + health.currentHealth + "/100";
			break;
		}
	}

	public void Powerup(bool start, string name)
	{
		//In each powerup activation case, the text reference must be adjusted to the correct one so editing can be done directly.
		//In each powerup deactivation case, the values are reset and the text color is reverted back from yellow to black.
		if (start) {
			switch (name) {
			case "FireRate":
				//FireRate powerup decreases the cooldown of the player's gun from .5 to .1 (thus increasing their fire rate).
				attack.fireRate = 10f;
				break;
			case "Damage":
				//Damage powerup increases the player's attack power from 20 to 40.
				attack.damagePerShot = 40;
				break;
			case "Health":
				//Health powerup recovers the player's health by 10.
				//Timer is set to 4.75 here because there doesn't need to be a 5 second wait after picking up a health powerup. This gives a .25 second wait instead.
				health.ChangeHealth (10);
				timer = 4.75f;
				break;
			}
			//Seeing as you can only reach this code block if a powerup is being activated, we don't have to account for any default case where name isn't equal.
			currentPowerup = name;
			UpdateText ();
		} else {
			switch (currentPowerup) {
			case "FireRate":
				attack.fireRate = 2f;
				fText.color = Color.black;
				fText.text = "Fire Rate: " + attack.fireRate + "/s";
				break;
			case "Damage":
				attack.damagePerShot = 20;
				dText.color = Color.black;
				dText.text = "Damage: " + attack.damagePerShot;
				break;
			}
			//Health powerup doesn't need a case because there is nothing to deactivate as a result of it being picked up.
			currentPowerup = "None";
		}
	}
}
