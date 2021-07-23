using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour {

	private CapsuleCollider capsule;
	private float destroyTimer;
	protected Animator animator;
	protected bool playerInRange;
	protected float attackTimer, coolDown;
	protected LevelOne levelOne;
	protected LevelTwo levelTwo;
	protected LevelThree levelThree;
	public bool active;
	public GameObject projectile, wandEnd;
	public int currentHealth, enemyNum, maxHealth;
	public NavMeshAgent navAgent;
	public Transform playerTrans;

	protected void Setup(int health, float cool)
    {
		//This function performs setup that every enemy needs to be able to function properly. Type-specific setup is handled locally.
		if (GameManager.GetLevel() == "LevelOne")
			levelOne = GameObject.Find("Managers").GetComponent<LevelOne>();
		else if(GameManager.GetLevel() == "LevelTwo")
			levelTwo = GameObject.Find("Managers").GetComponent<LevelTwo>();
		else if(GameManager.GetLevel() == "LevelThree")
			levelThree = GameObject.Find("Managers").GetComponent<LevelThree>();

		animator = GetComponent<Animator>();
		capsule = GetComponent<CapsuleCollider>();
		maxHealth = health;
		currentHealth = maxHealth;
		coolDown = cool;
		playerTrans = GameObject.Find("Player").transform;
	}

	public bool Death()
	{
		if (currentHealth <= 0) {
			//Setting active to false prevents movement, attacking, etc.
			active = false;

			//Disable collider and stop movement (if using a NavMeshAgent)
			capsule.enabled = false;
			if(navAgent != null)
				navAgent.isStopped = true;

			//Set death animation to start
			animator.SetTrigger ("Die");

			//Increment the appropriate level's counter
			switch (GameManager.GetLevel ()) {
				case "LevelOne":
					LevelOne.enemyCount++;
					LevelOne.EnemyDied();
					break;
				case "LevelTwo":
					levelTwo.EnemyDied();
					levelTwo.enemyCount++;
					if ((10 - levelTwo.enemyCount) > 0)
						UIManager.levelText.text = "Defeat " + (10 - levelTwo.enemyCount) + " Skeletons to advance.";
					break;
				case "LevelThree":
					LevelThree.enemyCount++;
					break;
			}
		
			//After 2 seconds, destroy the object.
			destroyTimer += Time.deltaTime;
			if (destroyTimer >= 2f)
				Destroy (gameObject);

			//Return true if enemy is dead, false if still alive.
			return true;
		} else {
			return false;
		}
	}

	//Used by wizards for ranged shots.
	protected void Shoot()
	{
		Instantiate (projectile, wandEnd.transform.position, transform.rotation);
	}
}
