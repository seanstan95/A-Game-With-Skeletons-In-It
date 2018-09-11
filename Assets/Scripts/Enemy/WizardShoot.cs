using UnityEngine;

public class WizardShoot : MonoBehaviour {

	private GameObject player;
	private Rigidbody rigidBody;

	private void Start()
	{
		player = GameObject.Find ("Player");
		rigidBody = GetComponent<Rigidbody> ();
		Destroy (gameObject, 2f);
	}

	private void Update()
	{
		transform.LookAt (player.transform);
		rigidBody.AddForce (transform.forward * 20);
	}

	private void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.tag == "StopShoot")
			Destroy (gameObject);
	}
}
