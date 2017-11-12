using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour {

	private bool overItem;
	private float horizontal, rayLength = 100f, speed = 10f, vertical;
	private GameObject otherPowerup;
	private int floorMask, index;
	private RaycastHit floorhit;
	private Vector3 movement;

	private void Start()
	{
		floorMask = LayerMask.GetMask ("Floor");
	}

	private void Update()
	{
		if (overItem && Input.GetKeyDown (KeyCode.Tab)) {
			//Update the current powerup to be the one on the ground, and destroy it.
			PowerupManager.heldPowerup = otherPowerup.tag;
			UIManager.heldText.text = "Held Powerup: " + PowerupManager.heldPowerup;
			Destroy (otherPowerup);
			overItem = false;
		}
	}

	private void FixedUpdate()
	{
		//First, grab the values applied to the horizontal and vertical axis. If nothing, it returns 0.
		horizontal = Input.GetAxisRaw ("Horizontal");
		vertical = Input.GetAxisRaw ("Vertical");

		//Run through a few tasks: determine movement of the player, determine any rotation of the player, and animate accordingly.
		Move (horizontal, vertical);
		Turning ();
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

	private void Turning()
	{
		//This rotates the player model to wherever the mouse is placed, as long as the mouse is on the floor of the game.
		Ray camRay = Camera.main.ScreenPointToRay (Input.mousePosition);

		if (Physics.Raycast (camRay, out floorhit, rayLength, floorMask)) {
			Vector3 playerToMouse = floorhit.point - transform.position;
			playerToMouse.y = 0f;
			Quaternion newRotation = Quaternion.LookRotation (playerToMouse);
			GetComponent<Rigidbody>().MoveRotation (newRotation);
		}
	}

	private void Animate(float horizontal, float vertical)
	{
		//Bool check if horizontal or vertical are 0 - if not, set the walking animation to be true.
		bool walking = horizontal != 0f || vertical != 0f;
		GetComponent<Animator>().SetBool ("Walking", walking);
	}

	private void OnTriggerEnter(Collider other)
	{
		//First, to get it out of the way, find the Powerup Array index for the item we have collided with.
		index = PowerupManager.GetIndex (other.gameObject.tag);

		//If the result of index above is 6 (default case from GetIndex), then we know the item we collided with is not a powerup, so skip all of this.
		if (index != 6) {
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

	private void OnTriggerExit(Collider other)
	{
		overItem = false;
	}
}
