﻿using UnityEngine;

public class WizardShoot : MonoBehaviour
{
	private Rigidbody rigidBody;

	private void Start()
	{
		rigidBody = GetComponent<Rigidbody>();
		transform.LookAt(GameObject.Find("Player").transform);
		Destroy(gameObject, 2f);
	}

	private void Update() => rigidBody.AddForce(transform.forward * 20);

	private void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.CompareTag("StopShoot"))
			Destroy(gameObject);
	}
}
