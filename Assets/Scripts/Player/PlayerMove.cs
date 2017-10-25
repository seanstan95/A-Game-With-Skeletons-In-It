using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 	* Controls all aspects regarding the player's movement
 	* Moves with the value of speed, and turns based on mouse position on a floor mask
*/

public class PlayerMove : MonoBehaviour {
	public float speed = 6f;

	Animator anim;
	float camRayLength = 100f, h, v;
	GameObject power;
	int floorMask;
	PowerupManager powerupManager;
	RaycastHit floorhit;
	Rigidbody playerRigidbody;
	Vector3 movement;

	void Awake()
	{
		anim = GetComponent<Animator>();
		floorMask = LayerMask.GetMask ("Floor");
		playerRigidbody = GetComponent<Rigidbody> ();
		power = GameObject.FindGameObjectWithTag ("PowerUpManager");
		powerupManager = power.GetComponent<PowerupManager> ();
	}

	void FixedUpdate()
	{
		h = Input.GetAxisRaw ("Horizontal");
		v = Input.GetAxisRaw ("Vertical");

		Move (h, v);
		Turning ();
		Animate (h, v);
	}

	void Move(float h, float v)
	{
		movement.Set (h, 0f, v);
		movement = movement.normalized * speed * Time.deltaTime;
		playerRigidbody.MovePosition (transform.position + movement);
	}

	void Turning()
	{
		Ray camRay = Camera.main.ScreenPointToRay (Input.mousePosition);

		if (Physics.Raycast (camRay, out floorhit, camRayLength, floorMask)) {
			Vector3 playerToMouse = floorhit.point - transform.position;
			playerToMouse.y = 0f;
			Quaternion newRotation = Quaternion.LookRotation (playerToMouse);
			playerRigidbody.MoveRotation (newRotation);
		}
	}

	void Animate(float h, float v)
	{
		bool walking = h != 0f || v != 0f;
		anim.SetBool ("IsWalking", walking);
	}

	void OnTriggerEnter(Collider other)
	{
		Debug.Log (powerupManager.currentPowerup);
		if (other.gameObject.CompareTag ("Powerup") && powerupManager.currentPowerup == "None") {
			powerupManager.FireRatePowerup (true);
			Destroy (other.gameObject);
		}
	}
}
