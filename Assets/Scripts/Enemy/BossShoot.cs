using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossShoot : MonoBehaviour {

	private GameObject player;
	private Rigidbody rigidBody;
	private float speed = 20;

	private void Start()
	{
		player = GameObject.Find ("Player");
		rigidBody = GetComponent<Rigidbody> ();
		transform.LookAt (player.transform);
		Destroy (gameObject, 2.5f);
	}

	private void Update()
	{
		rigidBody.AddForce (transform.forward * speed);
	}
}
