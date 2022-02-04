using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
	// Public Fields
	public NavMeshAgent navAgent;
	public GameObject projectile;
	public GameObject wandEnd;
	public Animator animator;
	public int currentHealth;
	public int maxHealth;
	public bool active;
	
	protected GameManager gameManager;
	protected bool playerInRange;
	protected float attackTimer;
	protected float coolDown;

	// Private Fields
	private LevelOne levelOne;
	private LevelTwo levelTwo;
	private LevelThree levelThree;
	
	// Cached Animator Hashes
	private static readonly int Die = Animator.StringToHash("Die");

	protected void Setup(int health, float cool)
	{
		//This function performs setup that every enemy needs to be able to function properly. Type-specific setup is handled locally.
		if (SceneManager.GetActiveScene().name == "LevelOne")
			levelOne = GameObject.Find("LevelOneScript").GetComponent<LevelOne>();
		else if (SceneManager.GetActiveScene().name == "LevelTwo")
			levelTwo = GameObject.Find("LevelTwoScript").GetComponent<LevelTwo>();
		else if (SceneManager.GetActiveScene().name == "LevelThree")
			levelThree = GameObject.Find("LevelThreeScript").GetComponent<LevelThree>();

		gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
		maxHealth = health;
		currentHealth = maxHealth;
		coolDown = cool;
	}

	public void Death(bool increaseCount = true)
	{
		if (currentHealth > 0) return;
		
		//Setting active to false prevents movement, attacking, etc.
		active = false;

		//Disable collider and stop movement (if using a NavMeshAgent)
		GetComponent<CapsuleCollider>().enabled = false;
		if (navAgent != null && navAgent.isOnNavMesh)
			navAgent.isStopped = true;
		
		//Set death animation to start
		animator.SetTrigger(Die);
		
		//After 2 seconds, destroy the object
		Destroy(gameObject, 2f);
		
		if (GetType() == typeof(WizardBoss)) CancelInvoke();
		if (!increaseCount) return;
		
		//Level-specific checks
		if (levelOne != null) levelOne.EnemyActivation(true);
		else if (levelTwo != null) levelTwo.EnemyDied();
		else if (levelThree != null) levelThree.EnemyActivation(true);
	}

	protected void Shoot()
	{
		Instantiate(projectile, wandEnd.transform.position, transform.rotation);
	}
}