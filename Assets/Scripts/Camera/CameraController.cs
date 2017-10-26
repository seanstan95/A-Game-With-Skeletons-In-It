using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {
	
	public GameObject player;
    private Vector3 offset;

    private void Start()
    {
		//Set the offset as the preset distance between the camera object and the player.
		offset = transform.position - player.transform.position;
    }

    private void LateUpdate()
    {
		//Camera control is done in LateUpdate() because LateUpdate runs after all other Update() are ran. 
		//Items may have moved while Update() calls continue, so this ensures camera is always kept accurate.
		transform.position = player.transform.position + offset;
    }
}
