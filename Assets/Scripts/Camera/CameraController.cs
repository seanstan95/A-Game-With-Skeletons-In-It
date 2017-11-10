using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {

	public GameObject player;

	Vector3 offset;

	public void Start()
	{
		offset = transform.position - player.transform.position;	
	}

	void LateUpdate()
	{
		//First set the position to the same offest from the player. Then, if pressing Q/E, rotate the camera around the player as well.
		transform.position = player.transform.position + offset;

		if(Input.GetKey(KeyCode.Q))
			transform.RotateAround (player.transform.position, Vector3.down, (100 * Time.deltaTime));
		if(Input.GetKey(KeyCode.E))
			transform.RotateAround (player.transform.position, Vector3.up, (100 * Time.deltaTime));

		offset = transform.position - player.transform.position;
	}
}
