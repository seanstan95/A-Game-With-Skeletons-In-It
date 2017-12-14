using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WizardShoot : MonoBehaviour {

	private GameObject player;
	private Rigidbody rigidBody;
	private float speed = 20;

	private void Start()
	{
		player = GameObject.Find ("Player");
		rigidBody = GetComponent<Rigidbody> ();
		Destroy (gameObject, 2f);
	}

	private void Update()
	{
		transform.LookAt (player.transform);
		rigidBody.AddForce (transform.forward * speed);
	}

	private void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.tag == "StopShoot")
			Destroy (gameObject);
	}
}
