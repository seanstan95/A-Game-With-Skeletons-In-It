using UnityEngine;

public class WizardBoss : Enemy
{
	private bool invoke;
	private int threeRoom, twoRoom;
	private double changeInterval, seconds;
	private float roomTimer;
	private UIManager UI;
	private Vector3 afterPosition, beforePosition;
	public Transform[] spawns;

	private void Start()
	{
		UI = GameObject.Find("UIManager").GetComponent<UIManager>();
		Setup(1000, 2f);
		changeInterval = 5.5;
		seconds = changeInterval;
	}

	private void Update()
	{
		//Active is set to false on death, which prevents this all from happening.
		if (active)
		{
			transform.LookAt(gameManager.playerTrans);
			if (!invoke)
			{
				InvokeRepeating("UpdateText", 0f, 0.5f);
				invoke = true;
			}

			if (roomTimer < changeInterval)
			{
				//Loop through this (attacking when attackTimer reaches 2) until aliveTimer reaches 5 to move location.
				roomTimer += Time.deltaTime;
				attackTimer += Time.deltaTime;

				if (Vector3.Distance(transform.position, gameManager.playerTrans.position) < 15 && attackTimer > coolDown)
				{
					Invoke("Shoot", 0f);
					Invoke("Shoot", .1f);
					attackTimer = 0;
				}
			}
			else
			{
				//if here, it's time to change room.
				beforePosition = transform.position;
				afterPosition = ChangePosition();

				if (beforePosition != afterPosition)
					afterPosition = ChangePosition();

				if (beforePosition != afterPosition)
				{
					transform.position = afterPosition;
					attackTimer = 0;
					roomTimer = 0;
				}
			}
		}
	}

	private Vector3 ChangePosition()
	{
		//First, randomize a room value to spawn from.
		twoRoom = Random.Range(0, 2);
		threeRoom = Random.Range(0, 3);

		//Next, use the player's current room + the random value to determine which room the boss will spawn in next.
		switch (gameManager.playerMove.room)
		{
			case "TopLeft":
				return TwoRoom(twoRoom, spawns[1], spawns[3]);
			case "TopRight":
				return TwoRoom(twoRoom, spawns[1], spawns[5]);
			case "BottomLeft":
				return TwoRoom(twoRoom, spawns[0], spawns[4]);
			case "BottomRight":
				return TwoRoom(twoRoom, spawns[2], spawns[4]);
			case "TopMiddle":
				return ThreeRoom(threeRoom, spawns[0], spawns[2], spawns[4]);
			case "BottomMiddle":
				return ThreeRoom(threeRoom, spawns[1], spawns[3], spawns[5]);
		}

		//should never reach here
		Debug.Log("ruh roh");
		return transform.position;
	}

	private Vector3 TwoRoom(int random, Transform roomOne, Transform roomTwo)
    {
		if (random == 0)
			return roomOne.position;
		else
			return roomTwo.position;
	}

	private Vector3 ThreeRoom(int random, Transform roomOne, Transform roomTwo, Transform roomThree)
    {
		if (random == 0)
			return roomOne.position;
		else if (random == 1)
			return roomTwo.position;
		else
			return roomThree.position;
    }

	private void UpdateText()
	{
		if (seconds <= 3)
			UI.levelText.text = "Boss Moving in " + seconds + " seconds.";
		else
			UI.levelText.text = "";

		seconds -= 0.5;
		if (seconds <= 0)
			seconds = changeInterval;
	}
}