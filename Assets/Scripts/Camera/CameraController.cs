﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {

	private Vector3 offset;
	public GameObject player;

	public void Start()
	{
		offset = transform.position - player.transform.position;	
	}

	void LateUpdate()
	{
		//The camera maintains the same offset away from the player regardless of player position or camera rotation.
		transform.position = player.transform.position + offset;

		//Rotation speed can be adjusted by changing the value of 100 to something else.
		if(Input.GetKey(KeyCode.Q))
			transform.RotateAround (player.transform.position, Vector3.down, (100 * Time.deltaTime));
		if(Input.GetKey(KeyCode.E))
			transform.RotateAround (player.transform.position, Vector3.up, (100 * Time.deltaTime));

		offset = transform.position - player.transform.position;
	}
}
