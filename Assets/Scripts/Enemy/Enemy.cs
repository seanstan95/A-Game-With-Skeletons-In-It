using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour {

	private bool count, animated;
	private float destroyTimer;
	protected Animator animator;
	protected bool playerInRange;
	protected float attackTimer, coolDown;
	protected int damagePerHit;
	protected LevelTwo levelTwo;
	public bool active;
	public CapsuleCollider capsule;
	public int maxHealth;
    public GameObject player, projectile, wandEnd;
    public int currentHealth;
	public NavMeshAgent navAgent;
	public int enemyNum;

	protected bool Death()
	{
		if (currentHealth <= 0) {

			//Disable collider and stop movement (if using a NavMeshAgent)
			if (capsule.enabled) {
				capsule.enabled = false;
				if(navAgent != null && !navAgent.isStopped)
					navAgent.isStopped = true;
			}

			//If the death animation hasn't happened yet, do so now.
			if (!animated) {
				animator.SetTrigger ("Die");
				animated = true;
			}

			if (!count) {
				//Increment the appropriate level's counter
				switch (GameManager.GetLevel ()) {
					case "LevelOne":
						LevelOne.enemyCount++;
						LevelOne.EnemyDied();
						break;
					case "LevelTwo":
						levelTwo.enemyCount++;
						if ((10 - levelTwo.enemyCount) > 0)
							UIManager.levelText.text = "Defeat " + (10 - levelTwo.enemyCount) + " Skeletons to advance.";
						break;
					case "LevelThree":
						LevelThree.enemyCount++;
						break;
				}
				count = true;
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
