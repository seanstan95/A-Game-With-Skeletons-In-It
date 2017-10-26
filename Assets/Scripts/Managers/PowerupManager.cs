using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PowerupManager : MonoBehaviour {

	public string currentPowerup;

	Color color;
	float timer, endTimer = 5f;
	PlayerAttack attack;
	PlayerHealth health;
	Text text;

	void Start()
	{
		//General presets. You cannot adjust the alpha of a text's color directly, only through another variable - which is why color variable is here.
		currentPowerup = "None";
		text = GetComponent<Text> ();
		color = text.color;
		attack = GameObject.FindGameObjectWithTag ("Player").GetComponent<PlayerAttack>();
		health = GameObject.FindGameObjectWithTag ("Player").GetComponent<PlayerHealth> ();
	}

	void Update()
	{
		//As long as there is an active powerup, increase the timer every frame.
		//Whenever a powerup is finished, currentPowerup is always set back to "None" to ensure this timer works properly.
		if (currentPowerup != "None")
			timer += Time.deltaTime;

		//When the timer reaches endTimer (currently a global 5 seconds, will likely be tweaked later), deactivate the current powerup.
		//Switches are, we feel, more readable and easier to type than a long if-else chain, with a (relatively) insignificant performance increase as well.
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
		switch (currentPowerup) {
		case "FireRate":
		case "Damage":
		case "Health":
			//Before adjusting the alpha, set the text to represent what powerup is active.
			//Then, set the alpha to 1 to make the text visible, and apply it to the object.
			text.text = "Current Powerup: " + currentPowerup;
			color.a = 1f;
			text.color = color;
			break;
		case "None":
			//Because the text will be invisible anyways, we don't need to waste a line of code on adjusting the text value to say nothing.
			color.a = 0f;
			text.color = color;
			break;
		}
	}

	public void Powerup(bool start, string name)
	{
		if (start) {
			switch (name) {
			case "FireRate":
				//FireRate powerup decreases the cooldown of the player's gun from .5 to .1 (thus increasing their fire rate).
				attack.coolDown = .1f;
				break;
			case "Damage":
				//Damage powerup increases the player's attack power from 20 to 40.
				attack.damagePerShot = 40;
				break;
			case "Health":
				//Health powerup recovers the player's health by 10.
				//Timer is set to 4.5 here because there doesn't need to be a 5 second wait after picking up a health powerup. This gives a .5 second wait instead.
				health.ChangeHealth (10);
				timer = 4.5f;
				break;
			}
			//Seeing as you can only reach this code block if a powerup is being activated, we don't have to account for any default case where name isn't equal.
			//currentPowerup can safely be set to be the value of name, and we can now update the on-screen text accordingly.
			currentPowerup = name;
			UpdateText ();
		} else {
			switch (currentPowerup) {
			case "FireRate":
				attack.coolDown = .5f;
				break;
			case "Damage":
				attack.damagePerShot = 20;
				break;
			}
			//Health powerup doesn't need a case because there is nothing to deactivate as a result of it being picked up.
			//Set the currentPowerup back to "None", and update the on-screen text accordingly.
			currentPowerup = "None";
			UpdateText ();
		}
	}
}
