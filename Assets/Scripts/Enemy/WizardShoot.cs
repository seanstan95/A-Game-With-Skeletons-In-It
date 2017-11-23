using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WizardShoot : MonoBehaviour {

	private GameObject player;

	private void Start()
	{
		player = GameObject.Find ("Player");
		Destroy (gameObject, 5f);
	}

	private void Update()
	{
		transform.Translate (-player.transform.position * Time.deltaTime, Space.World);
	}
}
