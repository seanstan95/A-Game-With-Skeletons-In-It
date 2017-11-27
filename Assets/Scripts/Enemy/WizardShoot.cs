using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WizardShoot : MonoBehaviour {

	private Rigidbody rigidBody;
	private float speed = 20;

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
