using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventManager : MonoBehaviour {

	void Start () {
		PowerupManager.Initialize ();
	}

	void Update () {
		PowerupManager.Update ();
	}
}
