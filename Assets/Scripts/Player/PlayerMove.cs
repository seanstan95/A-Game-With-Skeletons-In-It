using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour {

	float camRayLength = 100f, horizontal, speed = 6f, vertical;
	int floorMask;
	UIManager uiManager;
	RaycastHit floorhit;
	Vector3 movement;

	void Start()
	{
		floorMask = LayerMask.GetMask ("Floor");
		uiManager = GameObject.FindGameObjectWithTag ("HUD").GetComponent<UIManager>();
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
		movement = movement.normalized * speed * Time.deltaTime;
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
		GetComponent<Animator>().SetBool ("IsWalking", walking);
	}

	void OnTriggerEnter(Collider other)
	{
		//First of all, none of the below matters unless there is no active powerup, so check for that first.
		//Next, check if the object in question is a powerup - if so, activate it using the tag name as reference. Destroy it immediately after.
		if (uiManager.currentPowerup == "None") {
			switch (other.gameObject.tag) {
			case "FireRate":
			case "Damage":
			case "Health":
				uiManager.Powerup (true, other.gameObject.tag);
				Destroy (other.gameObject);
				break;
			}
		}
	}
}
