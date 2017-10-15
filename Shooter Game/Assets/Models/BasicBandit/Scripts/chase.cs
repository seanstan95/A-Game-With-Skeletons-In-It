
	using UnityEngine;
	using System.Collections;

	[RequireComponent(typeof(AudioSource))]


	public class chase : MonoBehaviour {

		public Transform player;
		public Animator anim;

		// Use this for initialization
		void Start () 
		{
			anim = GetComponent<Animator>();
		}
		
		void Update ()
	{

		Vector3 direction = player.position - this.transform.position;
		float angle = Vector3.Angle(direction,this.transform.forward);

		if (Vector3.Distance (player.position, this.transform.position) < 5.75 && angle < 100)
		{

			direction.y = 0;

			this.transform.rotation = Quaternion.Slerp (this.transform.rotation,
				Quaternion.LookRotation (direction), 0.1f);

			anim.SetBool ("isIdle", false);

			/*AudioSource audio = GetComponent<AudioSource> ();
			audio.Play ();
			audio.Play (44100);*/



			if (direction.magnitude > 0.1f) 
			{
				this.transform.Translate (0, 0, 0.05f);
				anim.SetBool ("isAttacking", true);

				anim.SetBool ("isWalking", false);
			} 


			else 
			{
				anim.SetBool ("isAttacking", false);

				anim.SetBool ("isWalking", true);
			}

		}
		else 
		{
			anim.SetBool("isIdle", true);
			anim.SetBool("isWalking", false);
			anim.SetBool ("isAttacking", false);
		}

		}

		
}

