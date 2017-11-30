using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerMove : MonoBehaviour {

	private bool collision, overItem;
	private float damageTimer, horizontal, speed = 5f, vertical;
	private GameObject otherPowerup;
	private Vector3 movement;
	public static string room;

	//REMOVE WHEN DONE TESTING
	private void Start()
	{
		GameManager.SetState ("LVLTHREEP");
	}

	private void Update()
	{
		//damageTimer is used to ensure the player can't be hurt by level colliders too quickly.
		if(damageTimer < 1)
			damageTimer += Time.deltaTime;

		if (collision) {
			if (damageTimer >= 1) {
				PlayerHealth.ChangeHealth (-10);
				damageTimer = 0f;
			}
		}

		if (overItem && Input.GetKeyDown (KeyCode.Tab)) {
			//Update the current powerup to be the one on the ground, and destroy it.
			PowerupManager.heldPowerup = otherPowerup.tag;
			UIManager.heldText.text = "Held Powerup: " + PowerupManager.heldPowerup;
			Destroy (otherPowerup);
			overItem = false;
			UIManager.powerupInfo.gameObject.SetActive (false);
		}
	}

	private void FixedUpdate()
	{
		//First, grab the values applied to the horizontal and vertical axis. If nothing, it returns 0.
		horizontal = Input.GetAxisRaw ("Horizontal");
		vertical = Input.GetAxisRaw ("Vertical");

		//Run through a few tasks: determine movement of the player, and animate accordingly.
		Move (horizontal, vertical);
		Animate (horizontal, vertical);
	}

	private void Move(float horizontal, float vertical)
	{
		//Set the x and z values using the axis, and set y to always be 0 since we don't want our player flying off to another planet.
		//Normalize the movement value based on speed and deltaTime, and tell the Rigidbody component to move the player to the new position.
		movement.Set (horizontal, 0f, vertical);
		movement = Camera.main.transform.TransformDirection(movement.normalized * speed * Time.deltaTime);
		GetComponent<Rigidbody>().MovePosition (transform.position + movement);
	}

	private void Animate(float horizontal, float vertical)
	{
		//Bool check if horizontal or vertical are 0 - if not, set the walking animation to be true.
		bool walking = horizontal != 0f || vertical != 0f;
		GetComponent<Animator>().SetBool ("Walking", walking);
	}

	private void OnTriggerEnter(Collider other)
	{
		//Manages enemy activations via triggers in levels. Returns out to avoid attempting to get a powerupmanager index since these aren't powerups.
		switch (other.name) {
			case "Trigger1":
			case "Trigger2":
			case "Trigger3":
			case "Trigger4":
				if (GameManager.GetLevel () == "LevelOne") {
					LevelOne.EnemyTrigger (other.name);
					Destroy (other.gameObject);
				}
				if (GameManager.GetLevel () == "LevelThree") {
					LevelThree.EnemyTrigger (other.name);
				}
				break;
			case "BossTrigger":
				if (GameManager.GetLevel() != "LevelOne") {
					GameObject.Find ("WizardBoss").GetComponent<WizardBoss> ().enabled = true;
				}
				return;
			case "WizardShoot(Clone)":
				PlayerHealth.ChangeHealth (-5);
				Destroy (other.gameObject);
				break;
			case "BossShoot(Clone)":
				PlayerHealth.ChangeHealth (-10);
				Destroy (other.gameObject);
				break;
		}

		//Manages boss spawn point room system in level two. Returns out to avoid attemtping to get a powerupmanager index since these aren't powerups.
		switch (other.name) {
			case "TopLeft":
				room = "TopLeft";
				return;
			case "TopMiddle":
				room = "TopMiddle";
				return;
			case "TopRight":
				room = "TopRight";
				return;
			case "BottomLeft":
				room = "BottomLeft";
				return;
			case "BottomMiddle":
				room = "BottomMiddle";
				return;
			case "BottomRight":
				room = "BottomRight";
				return;
		}

		//If the result of index above is 6 (default case from GetIndex), then we know the item we collided with is not a powerup, so skip all of this.
		if (PowerupManager.GetIndex(other.tag) != 6) {
			//If there is no held powerup, pick up the item and set the currently held powerup to be the tag of the item.
			if (PowerupManager.heldPowerup == "None") {
				PowerupManager.heldPowerup = other.gameObject.tag;
				UIManager.heldText.text = "Held Powerup: " + PowerupManager.heldPowerup;
				Destroy (other.gameObject);
			}else{
				//If here, we currently hold a valid powerup, and have collided with another powerup. Set overItem to true.
				UIManager.powerupInfo.gameObject.SetActive(true);
				UIManager.powerupInfo.text = "On Ground: " + other.tag;
				otherPowerup = other.gameObject;
				overItem = true;
			}
		}
	}

	private void OnTriggerExit(Collider other)
	{
		overItem = false;
		UIManager.powerupInfo.gameObject.SetActive (false);
	}

	private void OnCollisionEnter(Collision other)
	{
		//Environemnt objects that deal damage have non-trigger colliders to separate them from enemy activation triggers.
		switch (other.gameObject.name) {
			case "GreatAxe":
			case "Needle":
			case "Cutter":
			case "Spear":
			case "SawBlade":
				collision = true;
				//Reset spear animation on contact with player
				if (other.gameObject.name == "Spear")
					other.gameObject.GetComponentInParent<Animation> ().Rewind ();
				break;
		}
	}

	private void OnCollisionExit(Collision other)
	{
		collision = false;
	}
}
