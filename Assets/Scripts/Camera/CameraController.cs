using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {

	private Vector3 offset;
	public GameObject player;
    public GameObject chest;

	private void Start()
	{
		offset = transform.position - player.transform.position;	
	}

	private void LateUpdate()
	{
        transform.position = chest.transform.position;
        transform.rotation = player.transform.rotation;


        //**********************************************
        // Old 3rd person controller
        //**********************************************
		////The camera maintains the same offset away from the player regardless of player position or camera rotation.
		//transform.position = player.transform.position + offset;
      


  //      //Rotation speed can be adjusted by changing the value of 100 to something else.
  //      if (Input.GetKey(KeyCode.Q))
		//	transform.RotateAround (player.transform.position, Vector3.down, (100 * Time.deltaTime));
		//if(Input.GetKey(KeyCode.E))
		//	transform.RotateAround (player.transform.position, Vector3.up, (100 * Time.deltaTime));
  //      if (Input.GetKey(KeyCode.R))
  //      {
  //          //transform.rotation = Quaternion.Euler(player.transform.forward);
  //          //transform.position = player.transform.position + (-player.transform.forward * offset.magnitude);
  //          transform.rotation = Quaternion.Euler(22, player.transform.rotation.y, 0);

  //      }

  //      offset = transform.position - player.transform.position;
	}
}
