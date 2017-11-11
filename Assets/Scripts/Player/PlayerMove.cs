using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour {

	public float speed;

	bool overItem;
	float camRayLength = 100f, horizontal, vertical;
	GameObject otherPowerup;
	int floorMask, index;
	RaycastHit floorhit;
	Vector3 movement;

	void Start()
	{
		speed = 10f;
		floorMask = LayerMask.GetMask ("Floor");
	}

	void Update()
	{
		if (overItem && Input.GetKeyDown (KeyCode.Tab)) {
			//First, instantiate our currently held powerup so that it is also on the ground with the one we're switching with.
			//Then update the current powerup to be the new one, and destroy it on the ground so that what's left on the ground is the one we just swapped.
			Instantiate (PowerupManager.powerups [PowerupManager.GetIndex(PowerupManager.heldPowerup)], otherPowerup.transform.position, PowerupManager.powerups [index].transform.rotation);
			PowerupManager.heldPowerup = otherPowerup.tag;
			UIManager.heldText.text = "Held Powerup: " + PowerupManager.heldPowerup;
			Destroy (otherPowerup);
		}
	}

	void FixedUpdate()
	{
		//This script runs through FixedUpdate instead of Update because FixedUpdate runs as often as needed with the physics engine, whereas Update is always per-frame.
		//First, grab the values applied to the horizontal and vertical axis. If nothing, it returns 0.
		horizontal = Input.GetAxisRaw ("Horizontal");
		vertical = Input.GetAxisRaw ("Vertical");

		//Run through a few tasks: determine movement of the player, determine any rotation of the player, and animate accordingly.
		Move (horizontal, vertical);
		Turning ();
		Animate (horizontal, vertical);
	}

	void Move(float horizontal, float vertical)
	{
		//Set the x and z values using the axis, and set y to always be 0 since we don't want our player flying off to another planet.
		//Normalize the movement value based on speed and deltaTime, and tell the Rigidbody component to move the player to the new position.
		movement.Set (horizontal, 0f, vertical);
		movement = Camera.main.transform.TransformDirection(movement.normalized * speed * Time.deltaTime);
		GetComponent<Rigidbody>().MovePosition (transform.position + movement);
	}

	void Turning()
	{
		//Honestly not too sure how most of this actually works, came from a tutorial.
		Ray camRay = Camera.main.ScreenPointToRay (Input.mousePosition);

		if (Physics.Raycast (camRay, out floorhit, camRayLength, floorMask)) {
			Vector3 playerToMouse = floorhit.point - transform.position;
			playerToMouse.y = 0f;
			Quaternion newRotation = Quaternion.LookRotation (playerToMouse);
			GetComponent<Rigidbody>().MoveRotation (newRotation);
		}
	}

	void Animate(float horizontal, float vertical)
	{
		//Bool check if horizontal or vertical are 0 - if not, set the walking animation to be true.
		bool walking = horizontal != 0f || vertical != 0f;
		GetComponent<Animator>().SetBool ("Walking", walking);
	}

	void OnTriggerEnter(Collider other)
	{
		//First, to get it out of the way, find the Powerup Array index for the item we have collided with.
		index = PowerupManager.GetIndex (other.gameObject.tag);

		//If the result of index above is less than 7, then we know the item we collided with is not a powerup, so skip all of this.
		if (index < 7) {
			//If there is no held powerup, pick up the item and set the currently held powerup to be the tag of the item.
			if (PowerupManager.heldPowerup == "None") {
				PowerupManager.heldPowerup = other.gameObject.tag;
				UIManager.heldText.text = "Held Powerup: " + PowerupManager.heldPowerup;
				Destroy (other.gameObject);
			}else{
				//If here, we currently hold a valid powerup, and have collided with another powerup. Set overItem to true.
				otherPowerup = other.gameObject;
				overItem = true;
			}
		}
	}

	void OnTriggerExit(Collider other)
	{
		overItem = false;
	}
}
