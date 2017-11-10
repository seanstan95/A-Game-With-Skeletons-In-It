using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;

public class PowerupManager : MonoBehaviour {

	public static string currentPowerup;

	float endTimer;
	GameObject[] enemies;
	PlayerAttack attack;
	PlayerHealth health;
	PlayerMove move;

	void Start()
	{
		currentPowerup = "None";
		attack = GameObject.Find("Player").GetComponent<PlayerAttack>();
		health = GameObject.Find("Player").GetComponent<PlayerHealth> ();
		move = GameObject.Find("Player").GetComponent<PlayerMove> ();
	}

	void Update()
	{
		//End timer is set when a powerup is activated, and is usually just +5 second from activation time. When it's reached, disable the powerup.
		if (currentPowerup != "None" && Time.time >= endTimer) {
			Powerup (false, currentPowerup);
		}
	}

	void UpdateText()
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
				UIManager.powerupText.text = "Fire Rate: " + attack.fireRate + "/s";
				break;
			case "Freeze":
				UIManager.powerupText.text = "Enemies Frozen!";
				break;
			case "Speed":
				UIManager.powerupText.text = "Player speed increased!";
				break;
			case "Spread":
				UIManager.powerupText.text = "Spread shot active!";
				break;
		}
	}

	public void Powerup(bool start, string name)
	{
		if (start) {
			//If here, we are activating a powerup.
			switch (name) {
				case "Attack":
					enemies = GameObject.FindGameObjectsWithTag ("Skeleton");
					foreach (GameObject skeleton in enemies) {
						skeleton.GetComponent<Skeleton>().TakeDamage (50);
					}
					break;
				case "Damage":
					//Damage powerup increases the player's attack power from 20 to 40.
					attack.damagePerShot = 40;
					break;
				case "Health":
					//Health powerup recovers the player's health by 10.
					health.ChangeHealth (10);
					break;
				case "FireRate":
					//FireRate powerup increases the fire rate of the player's gun from 2 to 10 shots per second.
					attack.fireRate = 10f;
					break;
				case "Freeze":
					//Freeze powerup freezes all enemies in place for 5 seconds.
					//First, disable all the spawners temporarily so enemies don't continue to spawn while the current enemies are frozen in place.
					//Then, reference each active Skeleton in the scene and stop their movement.
					GameObject.Find("Managers").GetComponent<EnemyManager> ().spawnTime += 5f;
					enemies = GameObject.FindGameObjectsWithTag ("Skeleton");
					foreach (GameObject skeleton in enemies) {
						skeleton.GetComponent<NavMeshAgent> ().isStopped = true;
					skeleton.GetComponent<Animator> ().SetTrigger("Idle");
					}
					break;
				case "Speed":
					//Speed powerup increases the player's movement speed from 10 to 15.
					move.speed = 15f;
					break;
				case "Spread":
					//Spread powerup makes the player's gun shoot 5 bullet lines instead of just one.
					attack.spread = true;
					break;
			}
			//Adjust the currentPowerup value, and set the endTimer to be 5 seconds from now. 
			//Health nd attack powerups don't need a cooldown, so just set endTimer to be now.
			currentPowerup = name;
			if (currentPowerup == "Health")
				endTimer = Time.time;
			else
				endTimer = Time.time + 2.5f;
			
			UpdateText ();
		} else {
			//If here, then we are disabling a powerup.
			//Re-set the values back to default, change the text color to black from yellow, and update the text accordingly.
			switch (name) {
				case "FireRate":
					attack.fireRate = 2f;
					break;
				case "Damage":
					attack.damagePerShot = 20;
					break;
				case "Freeze":
					GameObject.Find ("Managers").GetComponent<EnemyManager> ().spawnTime = 1.5f;
					//Re-get the list of enemies - if an enemy is defeated while freeze is active, an error will occur referencing it here.
					enemies = GameObject.FindGameObjectsWithTag ("Skeleton");
					foreach (GameObject skeleton in enemies) {
						skeleton.GetComponent<NavMeshAgent> ().isStopped = false;
						skeleton.GetComponent<Animator> ().SetTrigger("Walking");
					}
					break;
				case "Speed":
					move.speed = 10f;
					break;
				case "Spread":
					attack.spread = false;
					break;
			}
			//Health and attack powerups doesn't need a case because there is nothing to deactivate.
			UIManager.powerupText.text = "";
			currentPowerup = "None";
		}
	}
}
