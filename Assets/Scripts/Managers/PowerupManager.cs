using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerupManager : MonoBehaviour {

	bool fireRate;
	float timer, endTimer = 5f;
	GameObject player;
	PlayerAttack attack;

	void Start()
	{
		player = GameObject.FindGameObjectWithTag ("Player");
		attack = player.GetComponent<PlayerAttack> ();
	}

	void Update()
	{
		if (fireRate) {
			timer += Time.deltaTime;
			if (timer >= endTimer)
				DeactivatePowerups ();
		}
	}

	public void FireRatePowerup()
	{
		fireRate = true;
		attack.coolDown = .10f;
	}

	void DeactivatePowerups()
	{
		fireRate = false;
		attack.coolDown = .5f;
		timer = 0f;
	}
}
