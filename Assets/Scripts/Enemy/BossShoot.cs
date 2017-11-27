using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossShoot : MonoBehaviour {

	private Rigidbody rigidBody;
	private float speed = 10;

	private void Start()
	{
		rigidBody = GetComponent<Rigidbody> ();
		Destroy (gameObject, 2.5f);
	}

	private void Update()
	{
		rigidBody.AddForce (transform.forward * speed);
	}
}
