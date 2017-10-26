using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PowerupManager : MonoBehaviour {

	public string currentPowerup;

	Color textColor;
	float timer, endTimer = 5f;
	PlayerAttack attack;
	PlayerHealth health;
	Text text;

	void Start()
	{
		currentPowerup = "None";
		attack = GameObject.FindGameObjectWithTag ("Player").GetComponent<PlayerAttack>();
		health = GameObject.FindGameObjectWithTag ("Player").GetComponent<PlayerHealth> ();
		text = GetComponent<Text> ();
		textColor = text.color;
	}

	void Update()
	{
		if (currentPowerup != "None")
			timer += Time.deltaTime;

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
			text.text = "Current Powerup: " + currentPowerup;
			textColor.a = 1f;
			text.color = textColor;
			break;
		case "None":
			textColor.a = 0f;
			text.color = textColor;
			break;
		}
	}

	public void Powerup(bool start, string name)
	{
		if (start) {
			Debug.Log ("Name: " + name);
			switch (name) {
			case "FireRate":
				attack.coolDown = .10f;
				break;
			case "Damage":
				attack.damagePerShot = 40;
				break;
			case "Health":
				health.ChangeHealth (10);
				timer = 4f;
				break;
			}
			currentPowerup = name;
			UpdateText ();
		} else {
			switch (name) {
			case "FireRate":
				attack.coolDown = .5f;
				break;
			case "Damage":
				attack.damagePerShot = 20;
				break;
			}
			currentPowerup = "None";
			UpdateText ();
		}
	}
}
