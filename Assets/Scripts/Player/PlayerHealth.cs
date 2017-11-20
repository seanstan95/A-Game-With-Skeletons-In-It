using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour {

	private float flashSpeed = 2f;
	private static bool damaged;
	public Image damageImage;
	public static int currentHealth = 100;

	private void Update () 
	{
		//If the player has been damaged in the last frame, set the color of the damageImage to be red.
		//Then, lerp from red to clear over flashSpeed * Time.deltaTime
		if (damaged)
			damageImage.color = new Color(250, 0, 0);
		damageImage.color = Color.Lerp (damageImage.color, Color.clear, flashSpeed * Time.deltaTime);
		damaged = false;
	}

	public static void ChangeHealth(int amount)
	{
		//Because this function handles taking damage and adding health, taking damage is when amount is negative, and adding health is when amount is positive.
		if (amount < 0)
			damaged = true;
			
		//Either way, adding amount to currentHealth will update it correctly (adding a negative is the same as subtracting).
		//Also, ensure that currentHealth doesn't go above 100. Then, adjust the health slider accordingly.
		currentHealth += amount;
		if (currentHealth > 100)
			currentHealth = 100;
		UIManager.UpdatePlayer (currentHealth);
	}
}
