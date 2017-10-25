using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PowerupManager : MonoBehaviour {

	public string currentPowerup;

	Color textColor;
	float timer, endTimer = 5f;
	GameObject player, textObj;
	PlayerAttack attack;
	Text text;

	void Start()
	{
		currentPowerup = "None";
		player = GameObject.FindGameObjectWithTag ("Player");
		textObj = GameObject.FindGameObjectWithTag ("PText");
		text = textObj.GetComponent<Text> ();
		textColor = text.color;
		attack = player.GetComponent<PlayerAttack> ();
	}

	void Update()
	{
		if (currentPowerup == "FireRate") {
			timer += Time.deltaTime;
			if (timer >= endTimer)
				FireRatePowerup(false);
		}
	}

	void UpdateText()
	{
		//function is easily expandable using similar lines of code whenever we add a new powerup - just add to the if-else chain - if it gets lare enough a switch might be more readable
		if (currentPowerup == "FireRate") {
			text.text = "Current Powerup: " + currentPowerup;
			textColor.a = 1f;
			text.color = textColor;
		} else if (currentPowerup == "None") {
			textColor.a = 0;
			text.color = textColor;
		}
	}

	public void FireRatePowerup(bool start)
	{
		//each powerup function should follow this format where both activation and deactivation occur in the same function as dictated by the bool
		//true when being activated from PlayerMove and false when being called from Update() in this script
		if (start) {
			currentPowerup = "FireRate";
			attack.coolDown = .10f;
			UpdateText ();
		} else {
			currentPowerup = "None";
			attack.coolDown = .5f;
			timer = 0f;
			UpdateText ();
		}
	}
}
