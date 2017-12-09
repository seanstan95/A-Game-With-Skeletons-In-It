using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WizardBoss : Enemy {

	private int threeRoom, twoRoom;
	private float aliveTimer;
	public Transform[] spawns;

	private void Start()
	{
		animator = GetComponent<Animator> ();
		capsule = GetComponent<CapsuleCollider> ();
		coolDown = 2f;
		maxHealth = 500;
		currentHealth = (int)maxHealth;
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
		//First, randomize a room value to spawn from
		threeRoom = Random.Range (0, 3);
		twoRoom = Random.Range (0, 2);

		//Next, use the player's current room + the random value to determine which room the boss will spawn in next.
		switch (PlayerMove.room) {
			case "TopLeft":
				if (twoRoom == 0)
					transform.position = spawns [3].position;
				else if (twoRoom == 1)
					transform.position = spawns [1].position;
				break;
			case "TopRight":
				if (twoRoom == 0)
					transform.position = spawns [1].position;
				else if (twoRoom == 1)
					transform.position = spawns [5].position;
				break;
			case "BottomLeft":
				if (twoRoom == 0)
					transform.position = spawns [4].position;
				else if (twoRoom == 1)
					transform.position = spawns [0].position;
				break;
			case "BottomRight":
				if (twoRoom == 0)
					transform.position = spawns [4].position;
				else if (twoRoom == 1)
					transform.position = spawns [2].position;
				break;
			case "TopMiddle":
				if (threeRoom == 0)
					transform.position = spawns [0].position;
				else if (threeRoom == 1)
					transform.position = spawns [4].position;
				else if (threeRoom == 2)
					transform.position = spawns [2].position;
				break;
			case "BottomMiddle":
				if (threeRoom == 0)
					transform.position = spawns [3].position;
				else if (threeRoom == 1)
					transform.position = spawns [1].position;
				else if (threeRoom == 2)
					transform.position = spawns [5].position;
				break;
		}
	}
}
