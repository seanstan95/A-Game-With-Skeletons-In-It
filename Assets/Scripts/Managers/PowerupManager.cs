using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;

public static class PowerupManager {

	public static string currentPowerup = "None", heldPowerup = "None";
	public static GameObject[] powerups;

	private static float endTimer;
	private static GameObject[] enemies;

	public static void Initialize(GameObject[] powerupsIn)
	{
		powerups = powerupsIn;
	}

	public static void Update()
	{
		//This Update() is called regularly by EventManager's own Update(). Having this class static means being unable to use Unity's built-in monobehaviours internally.
		//End timer is set when a powerup is activated, and is usually just +2.5 seconds from activation time. When it's reached, disable the powerup.
		if (currentPowerup != "None" && Time.time >= endTimer) {
			EndPowerup();
		}
	}

	private static void UpdateText()
	{
		//Each powerup activation will trigger the associated text to turn yellow so it's noticed that it's active.
		switch (currentPowerup) {
			case "Attack":
				UIManager.powerupText.text = "Enemies damaged!";
				break;
			case "Damage":
				UIManager.powerupText.text = "Damage increased!";
				break;
			case "FireRate":
				UIManager.powerupText.text = "Fire rate increased!";
				break;
			case "Freeze":
				UIManager.powerupText.text = "Enemies Frozen!";
				break;
			case "Spread":
				UIManager.powerupText.text = "Spread shot active!";
				break;
		}
	}

	public static void UsePowerup()
	{
		//We are activating a powerup, so switch currentPowerup to what we were holding, and set heldPowerup to be nothing, so the player can pick up another powerup.
		currentPowerup = heldPowerup;
		heldPowerup = "None";

		switch (currentPowerup) {
			case "Attack":
				//Attack powerup damages all active enemies by 50.
				enemies = GameObject.FindGameObjectsWithTag ("NormalEnemy");
				foreach (GameObject enemy in enemies) {
					enemy.GetComponent<Enemy>().TakeDamage (50);
				}
				break;
			case "Damage":
				//Damage powerup increases the player's attack power from 20 to 40.
				PlayerAttack.damagePerShot = 40;
				break;
			case "Health":
				//Health powerup recovers the player's health by 10.
				PlayerHealth.ChangeHealth (10);
				break;
			case "FireRate":
				//FireRate powerup increases the fire rate of the player's gun from 2 to 10 shots per second.
				PlayerAttack.fireRate = 10f;
				break;
			case "Freeze":
				//Freeze powerup freezes all enemies in place for 5 seconds.
				//First, disable all the spawners temporarily so enemies don't continue to spawn while the current enemies are frozen in place.
				//Then, reference each active Skeleton in the scene and stop their movement.
				enemies = GameObject.FindGameObjectsWithTag ("NormalEnemy");
				foreach (GameObject enemy in enemies) {
					enemy.GetComponent<NavMeshAgent> ().isStopped = true;
					enemy.GetComponent<Animator> ().SetBool("Walking", false);
				}
				break;
			case "Spread":
				//Spread powerup makes the player's gun shoot 5 bullet lines instead of just one.
				PlayerAttack.range = 10f;
				PlayerAttack.spread = true;
				break;
		}

		//Health powerup doesn't need a cooldown, so just set endTimer to be now.
		if (currentPowerup == "Health")
			endTimer = Time.time;
		else
			endTimer = Time.time + 2.5f;
		
		UpdateText ();
	}

	private static void EndPowerup()
	{
		//If here, then we are disabling a powerup.
		//Re-set the values back to default, change the text color to black from yellow, and update the text accordingly.
		switch (currentPowerup) {
			case "FireRate":
				PlayerAttack.fireRate = 2f;
				break;
			case "Damage":
				PlayerAttack.damagePerShot = 20;
				break;
			case "Freeze":
				//Re-get the list of enemies - if an enemy is defeated while freeze is active, an error will occur referencing it here.
				enemies = GameObject.FindGameObjectsWithTag ("NormalEnemy");
				foreach (GameObject skeleton in enemies) {
					skeleton.GetComponent<NavMeshAgent> ().isStopped = false;
					skeleton.GetComponent<Animator> ().SetBool ("Walking", true);
				}
				break;
			case "Spread":
				PlayerAttack.spread = false;
				PlayerAttack.range = 25f;
				break;
		}
		//Health and attack powerups doesn't need a case because there is nothing to deactivate.
		UIManager.powerupText.text = "";
		currentPowerup = "None";
	}

	public static int GetIndex(string name)
	{
		switch (name) {
			case "Attack":
				return 0;
			case "Damage":
				return 1;
			case "FireRate":
				return 2;
			case "Freeze":
				return 3;
			case "Health":
				return 4;
			case "Spread":
				return 5;
			default:
				return 6;
		}
	}
}
