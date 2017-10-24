using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
	 * Controls the camera's offset compared to the player
	 * Offset is calculated as the difference between the camera's starting point and the player's starting point
	 * Offset is then used to maintain the same distance from the player every update frame
*/

public class CameraController : MonoBehaviour {
	
	public GameObject player;
    private Vector3 offset;

    private void Start()
    {
		offset = transform.position - player.transform.position;
    }

    private void LateUpdate()
    {
		transform.position = player.transform.position + offset;
    }
}
