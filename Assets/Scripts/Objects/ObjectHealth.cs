using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectHealth : MonoBehaviour {

	int health;

	void Start () 
	{
		health = 100;
	}

	void Update () 
	{
		if (health <= 0)
			Destroy (gameObject);
	}

	public void TakeDamage(int amount)
	{
		health -= amount;
	}
}
