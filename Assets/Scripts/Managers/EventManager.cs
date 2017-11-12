using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventManager : MonoBehaviour {

	public GameObject[] powerups;

	private void Start () {
		PowerupManager.Initialize (powerups);
		UIManager.Initialize ();
	}

	private void Update () {
		PowerupManager.Update ();
		UIManager.Update ();
	}
}
