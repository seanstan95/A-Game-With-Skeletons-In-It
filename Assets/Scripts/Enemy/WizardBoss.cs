using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WizardBoss : Enemy {

	private int room;
	private float aliveTimer;
	public GameObject end, projectile;

	private void Start()
	{
		animator = GetComponent<Animator> ();
		coolDown = 2f;
		currentHealth = 500;
		player = GameObject.Find ("Player");
	}

	private void Update()
	{
		transform.LookAt (player.transform);
		if (!Death ()) {
			if (aliveTimer < 5) {
				aliveTimer += Time.deltaTime;
				attackTimer += Time.deltaTime;

				if (Vector3.Distance (transform.position, player.transform.position) < 27 && attackTimer > coolDown) {
					Invoke ("Shoot", 0f);
					Invoke ("Shoot", .1f);
					Invoke ("Shoot", .2f);
					attackTimer = 0;	
				}
			} else {
				//if here, it's time to change spawn
				ChangePosition();
				aliveTimer = 0;
			}
		}
	}

	private void ChangePosition()
	{
		//First, randomize a room value to spawn from (if the room has only 2 options and a 3 is rolled, boss will spawn at #2)
		room = Random.Range (0, 3);

		//Next, use the player's current room + the random value to determine which room the boss will spawn in next.
		switch (PlayerMove.room) {
			case "TopLeft":
				if (room == 0)
					transform.position = GameObject.Find ("BottomLeft").transform.position;
				else if (room == 1 || room == 2)
					transform.position = GameObject.Find ("TopMiddle").transform.position;
				break;
			case "TopRight":
				if (room == 0)
					transform.position = GameObject.Find ("TopMiddle").transform.position;
				else if (room == 1 || room == 2)
					transform.position = GameObject.Find ("BottomRight").transform.position;
				break;
			case "BottomLeft":
				if (room == 0)
					transform.position = GameObject.Find ("BottomMiddle").transform.position;
				else if (room == 1 || room == 2)
					transform.position = GameObject.Find ("TopLeft").transform.position;
				break;
			case "BottomRight":
				if (room == 0)
					transform.position = GameObject.Find ("BottomMiddle").transform.position;
				else if (room == 1 || room == 2)
					transform.position = GameObject.Find ("TopRight").transform.position;
				break;
			case "TopMiddle":
				if (room == 0)
					transform.position = GameObject.Find ("TopLeft").transform.position;
				else if (room == 1)
					transform.position = GameObject.Find ("BottomMiddle").transform.position;
				else if (room == 2)
					transform.position = GameObject.Find ("TopRight").transform.position;
				break;
			case "BottomMiddle":
				if (room == 0)
					transform.position = GameObject.Find ("BottomLeft").transform.position;
				else if (room == 1)
					transform.position = GameObject.Find ("TopMiddle").transform.position;
				else if (room == 2)
					transform.position = GameObject.Find ("BottomRight").transform.position;
				break;
		}
	}

	private void Shoot()
	{
		Instantiate (projectile, end.transform.position, transform.rotation);
	}
}
