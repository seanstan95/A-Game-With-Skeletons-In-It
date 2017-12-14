using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerMove : MonoBehaviour {

	private Animator animator;
	private bool collision, overItem;
	private float damageTimer, mouseX, moveH, moveV, speed = 5f;
	private GameObject otherPowerup;
	private Rigidbody rigidBody;
	private Vector3 movement;
	public static string room;

	private void Start()
	{
		Cursor.lockState = CursorLockMode.Locked;
		animator = GetComponent<Animator> ();
		rigidBody = GetComponent<Rigidbody> ();
		//REMOVE WHEN DONE TESTING
		GameManager.SetState ("LVLTWOP");
	}

	private void Update()
	{
		//damageTimer is used to ensure the player can't be hurt by level colliders too quickly.
		if (collision && damageTimer >= 1) {
			PlayerHealth.ChangeHealth (-10);
			damageTimer = 0f;
		} else if (damageTimer < 1) {
			damageTimer += Time.deltaTime;
		}

		if (overItem && Input.GetKeyDown (KeyCode.Tab)) {
			//Update the current powerup to be the one on the ground, and destroy it.
			PowerupManager.heldPowerup = otherPowerup.tag;
			UIManager.heldText.text = "Held Powerup: " + PowerupManager.heldPowerup;
			Destroy (otherPowerup);
			overItem = false;
			UIManager.onGroundText.gameObject.SetActive (false);
		}
	}

	private void FixedUpdate()
	{
		//First, grab the values applied to the movement axis.
		moveH = Input.GetAxisRaw ("Horizontal");
		moveV = Input.GetAxisRaw ("Vertical");
		mouseX = Input.GetAxisRaw ("Mouse X");

		//Move and rotate the player, then animate accordingly.
		movement.Set(moveH, 0f, moveV);
		movement = Camera.main.transform.TransformDirection (movement.normalized * speed * Time.deltaTime);
		rigidBody.MovePosition (transform.position + movement);
		Vector3 rotate = new Vector3 (0, mouseX, 0);
		transform.Rotate(rotate * 4f);

		if (moveH != 0f || moveV != 0f)
			animator.SetBool ("Walking", true);
		else
			animator.SetBool ("Walking", false);
	}

	private void OnTriggerEnter(Collider other)
	{
		//Manages enemy activations via triggers in levels. Returns out to avoid attempting to get a powerupmanager index since these aren't powerups.
		switch (other.name) {
			case "Trigger1":
			case "Trigger2":
			case "Trigger3":
			case "Trigger4":
			case "Trigger5":
			case "BossTrigger":
				switch (GameManager.GetLevel ()) {
					case "LevelOne":
						LevelOne.EnemyTrigger (other.name);
						break;
					case "LevelTwo":
						LevelTwo.boss.active = true;
						break;
					case "LevelThree":
						LevelThree.EnemyTrigger (other.name);
						break;
				}
				Destroy (other.gameObject);
				break;
			case "Fire":
				collision = true;
				break;
			case "WizardShoot(Clone)":
				//If wizard boss is active, then we're definitely facing level 2 boss, so do 10 damage instead of 5
				if(LevelTwo.boss.active)
					PlayerHealth.ChangeHealth (-10);
				else 
					PlayerHealth.ChangeHealth (-5);
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
				UIManager.onGroundText.gameObject.SetActive(true);
				UIManager.onGroundText.text = "On Ground: " + other.tag;
				otherPowerup = other.gameObject;
				overItem = true;
			}
		}
	}

	private void OnTriggerExit(Collider other)
	{
		overItem = false;
		collision = false;
		UIManager.onGroundText.gameObject.SetActive (false);
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
