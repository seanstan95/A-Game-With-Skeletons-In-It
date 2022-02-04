using UnityEngine;

public class FinalBoss : Enemy
{
	// Public Fields
	public bool shield = true;
	
	// Private Fields
	private Transform _transform;
	private int nextTrigger = 1600;
	private float newX;
	private float newY;
	private float newZ;
	private bool size;

	// Cached Animator Hashes 
	private static readonly int Attack = Animator.StringToHash("Attack");
	private static readonly int Run = Animator.StringToHash("Run");
	private static readonly int Idle = Animator.StringToHash("Idle");

	private void Awake()
	{
		_transform = transform;
	}

	private void Start()
	{
		Setup(2000, 2.76f);
		playerInRange = true;
	}

	private void Update()
	{
		if (!active) return;
		if (shield) return;
		
		if (!size)
		{
			//Each iteration increases size by one lerp and returns out to avoid code progression, until at (10, 1.5, 10)
			if (_transform.localScale.x < 10)
				newX = _transform.localScale.x + Time.deltaTime * 7;
			if (_transform.localScale.y < 1.5)
				newY = _transform.localScale.y + Time.deltaTime;
			if (_transform.localScale.z < 10)
				newZ = _transform.localScale.z + Time.deltaTime * 7;

			_transform.localScale = Vector3.Lerp(_transform.localScale, new Vector3(newX, newY, newZ), .75f);

			if (_transform.localScale.x >= 10 && _transform.localScale.y >= 1.5 && _transform.localScale.z >= 10)
				size = true;
			else
				return;
		}

		//Regardless of player distance, continue to update the path to the player. Boss will only move when navAgent.isStopped is false.
		navAgent.SetDestination(gameManager.playerTrans.position);

		if (Vector3.Distance(_transform.position, gameManager.playerTrans.position) > 2.5f)
		{
			//If here, boss is charging at the player
			if (playerInRange)
			{
				playerInRange = false;
				navAgent.isStopped = false;
				animator.SetBool(Idle, false);
				animator.SetBool(Attack, false);
				animator.SetBool(Run, true);
			}
		}
		else
		{
			//If here, boss is attacking the player
			if (!playerInRange)
			{
				playerInRange = true;
				navAgent.isStopped = true;
				attackTimer = 1.76f;
				animator.SetBool(Idle, false);
				animator.SetBool(Run, false);
				animator.SetBool(Attack, true);
			}
		}
		
		if (playerInRange)
		{
			attackTimer += Time.deltaTime;
			if (attackTimer >= coolDown)
			{
				attackTimer = 0;
				gameManager.playerHealth.ChangeHealth(20);
			}
		}

		//if health is down by 400, return to center until another target is destroyed
		if (currentHealth != nextTrigger || shield) return;
		
		nextTrigger -= 400;
		navAgent.isStopped = true;
		playerInRange = true;
		_transform.localPosition = new Vector3(-0.3f, -13, 0);
		_transform.localScale = new Vector3(5, .75f, 5);
		size = false;
		shield = true;
		animator.SetBool(Attack, false);
		animator.SetBool(Run, false);
		animator.SetBool(Idle, true);
	}
}