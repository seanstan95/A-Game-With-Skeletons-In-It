using UnityEngine;

public class Wizard : Enemy
{
	// Cached Animator Hashes
	private static readonly int Idle = Animator.StringToHash("Idle");
	private static readonly int Attacking = Animator.StringToHash("Attacking");

	private void Start()
	{
		Setup(160, 2.5f);
	}

	private void Update()
	{
		//Active is set to false on death, which prevents this all from happening.
		if (!active) return;
		
		transform.LookAt(gameManager.playerTrans);
		if (Vector3.Distance(transform.position, gameManager.playerTrans.position) <= 20)
		{
			if (!playerInRange)
			{
				playerInRange = true;
				animator.SetBool(Idle, false);
				animator.SetBool(Attacking, true);
			}

			attackTimer += Time.deltaTime;
			
			if (!(attackTimer >= coolDown)) return;
			
			attackTimer = 0;
			Invoke(nameof(Shoot), 0f);
			Invoke(nameof(Shoot), .1f);
		}
		else
		{
			if (!playerInRange) return;
			playerInRange = false;
			animator.SetBool(Attacking, false);
			animator.SetBool(Idle, true);
		}
	}
}