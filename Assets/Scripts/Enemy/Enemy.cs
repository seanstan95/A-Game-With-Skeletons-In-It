using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.AI;

public class Enemy : MonoBehaviour {

	private CapsuleCollider capsule;
	protected bool playerInRange;
	protected float attackTimer, coolDown;
	protected LevelOne levelOne;
	protected LevelTwo levelTwo;
	protected LevelThree levelThree;
	public Animator animator;
	public bool active;
	public GameObject projectile, wandEnd;
	public int currentHealth, maxHealth;
	public NavMeshAgent navAgent;
	public Transform playerTrans;

	protected void Setup(int health, float cool)
    {
		//This function performs setup that every enemy needs to be able to function properly. Type-specific setup is handled locally.
		if (SceneManager.GetActiveScene().name == "LevelOne")
			levelOne = GameObject.Find("LevelOneScript").GetComponent<LevelOne>();
		else if(SceneManager.GetActiveScene().name == "LevelTwo")
			levelTwo = GameObject.Find("LevelTwoScript").GetComponent<LevelTwo>();
		else if(SceneManager.GetActiveScene().name == "LevelThree")
			levelThree = GameObject.Find("LevelThreeScript").GetComponent<LevelThree>();

		capsule = GetComponent<CapsuleCollider>();
		maxHealth = health;
		currentHealth = maxHealth;
		coolDown = cool;
		playerTrans = GameManager.player.transform;
	}

	public void Death(bool increaseCount = true)
	{
		if (currentHealth <= 0)
		{
			//Setting active to false prevents movement, attacking, etc.
			active = false;

			//Disable collider and stop movement (if using a NavMeshAgent)
			capsule.enabled = false;
			if (navAgent != null)
				navAgent.isStopped = true;

			//Set death animation to start
			animator.SetTrigger("Die");

			//After 2 seconds, destroy the object
			Destroy(gameObject, 2f);

			if (!increaseCount)
				return;

			//Level-specific checks
			if (levelOne != null)
				levelOne.EnemyDied();
			else if (levelTwo != null)
				levelTwo.EnemyDied();
			else if (levelThree != null)
				levelThree.EnemyDied();
		}
	}

	//Here because both Wizard and WizardBoss use this via Invoke() at time ranges to shoot projectiles at the player.
	protected void Shoot()
	{
		Instantiate (projectile, wandEnd.transform.position, transform.rotation);
	}
}
