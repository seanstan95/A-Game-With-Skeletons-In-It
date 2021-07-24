using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMove : MonoBehaviour
{
	private bool collision;
	private float damageTimer, mouseX, moveH, moveV;
	public LevelOne levelOne;
	public LevelTwo levelTwo;
	public LevelThree levelThree;
	private Vector3 movement;
	public Animator animator;
	public static string room;

	private void Start()
	{
		Cursor.lockState = CursorLockMode.Locked;
	}

	private void Update()
	{
		//damageTimer is used to ensure the player can't be hurt by level colliders too quickly.
		if (collision && damageTimer >= 1)
		{
			GameManager.playerHealth.ChangeHealth(10);
			damageTimer = 0f;
		}
		else if (damageTimer < 1)
		{
			damageTimer += Time.deltaTime;
		}
	}

	private void FixedUpdate()
	{
		//First, grab the values applied to the movement axis.
		moveH = Input.GetAxisRaw("Horizontal");
		moveV = Input.GetAxisRaw("Vertical");
		mouseX = Input.GetAxisRaw("Mouse X");

		//Move and rotate the player, then animate accordingly.
		movement.Set(moveH, 0f, moveV);
		movement = Camera.main.transform.TransformDirection(movement.normalized * 5f * Time.deltaTime);
		movement.y = 0f;
		transform.position += movement;
		Vector3 rotate = new Vector3(0, mouseX, 0);
		transform.Rotate(rotate * 4f);

		if (moveH != 0f || moveV != 0f)
		{
			animator.SetBool("Idle", false);
			animator.SetBool("Walking", true);
		}
		else
		{
			animator.SetBool("Walking", false);
			animator.SetBool("Idle", true);
		}
	}

	private void OnTriggerEnter(Collider other)
	{
		//Manages enemy activations via triggers in levels.
		switch (other.name)
		{
			case "Trigger1":
			case "Trigger2":
			case "Trigger3":
			case "Trigger4":
			case "Trigger5":
			case "BossTrigger":
			case "WizBossTrigger":
			case "FinalBossTrigger":
				switch (SceneManager.GetActiveScene().name)
				{
					case "LevelOne":
						levelOne.EnemyTrigger(other.name);
						break;
					case "LevelTwo": //only trigger activation in level 2 is the boss
						levelTwo.boss.active = true;
						break;
					case "LevelThree":
						levelThree.EnemyTrigger(other.name);
						break;
				}
				Destroy(other.gameObject);
				break;
			case "WizardTrigger":
				levelTwo.wizards[2].active = false;
				levelTwo.wizards[2].animator.SetBool("Attacking", false);
				levelTwo.wizards[2].animator.SetBool("Idle", true);
				levelTwo.wizards[4].active = false;
				levelTwo.wizards[4].animator.SetBool("Attacking", false);
				levelTwo.wizards[4].animator.SetBool("Idle", true);
				break;
			case "Fire":
				collision = true;
				break;
			case "WizardShoot(Clone)":
			case "WizardShoot2(Clone)":
				GameManager.playerHealth.ChangeHealth(5);
				Destroy(other.gameObject);
				break;
		}

		//Manages Wizard boss spawn positions.
		switch (other.name)
		{
			case "TopLeft":
				room = "TopLeft";
				break;
			case "TopMiddle":
				room = "TopMiddle";
				break;
			case "TopRight":
				room = "TopRight";
				break;
			case "BottomLeft":
				room = "BottomLeft";
				break;
			case "BottomMiddle":
				room = "BottomMiddle";
				break;
			case "BottomRight":
				room = "BottomRight";
				break;
		}
	}

	private void OnTriggerExit(Collider other)
	{
		collision = false;
	}

	private void OnCollisionEnter(Collision other)
	{
		//Environemnt objects that deal damage have non-trigger colliders to separate them from enemy activation triggers.
		switch (other.gameObject.name)
		{
			case "GreatAxe":
			case "Needle":
			case "Cutter":
			case "Spear":
			case "SawBlade":
				collision = true;
				//Reset spear animation on contact with player
				if (other.gameObject.name == "Spear")
					other.gameObject.GetComponentInParent<Animation>().Rewind();
				break;
		}
	}

	private void OnCollisionExit(Collision other)
	{
		collision = false;
	}

	public void UpdateLevel()
	{
		if (SceneManager.GetActiveScene().name == "LevelOne")
			levelOne = GameObject.Find("LevelOneScript").GetComponent<LevelOne>();
		else if (SceneManager.GetActiveScene().name == "LevelTwo")
			levelTwo = GameObject.Find("LevelTwoScript").GetComponent<LevelTwo>();
		else if (SceneManager.GetActiveScene().name == "LevelThree")
			levelThree = GameObject.Find("LevelThreeScript").GetComponent<LevelThree>();
	}
}
