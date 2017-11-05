using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectHealth : MonoBehaviour {

	int health;

	void Start () 
	{
		if (gameObject.tag == "Box")
			health = 100;
		else
			health = 50;
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
