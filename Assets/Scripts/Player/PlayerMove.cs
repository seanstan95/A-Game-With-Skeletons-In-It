using UnityEngine;
using UnityEngine.SceneManagement;
using System.Linq;

public class PlayerMove : MonoBehaviour
{
	private bool collision;
	private float damageTimer, mouseX, moveH, moveV;
	private readonly string[] damageObjects = { "GreatAxe", "Needle", "Cutter", "Spear", "SawBlade" };
	private readonly string[] wizRooms = { "TopLeft", "TopMiddle", "TopRight", "BottomLeft", "BottomMiddle", "BottomRight" };
	private Vector3 movement;
	public GameManager gameManager;
	public LevelOne levelOne;
	public LevelTwo levelTwo;
	public LevelThree levelThree;
	public string room;

	private void Start()
	{
		Cursor.lockState = CursorLockMode.Locked;
	}

	private void Update()
	{
		//damageTimer is used to ensure the player can't be hurt by level colliders too quickly.
		if (collision && damageTimer >= 1)
		{
			collision = false;
			gameManager.playerHealth.ChangeHealth(10);
			damageTimer = 0f;
		}
		else if (damageTimer < 1)
			damageTimer += Time.deltaTime;
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
			gameManager.playerAnimation.SetBool("Idle", false);
			gameManager.playerAnimation.SetBool("Walking", true);
		}
		else
		{
			gameManager.playerAnimation.SetBool("Walking", false);
			gameManager.playerAnimation.SetBool("Idle", true);
		}
	}

	private void OnTriggerEnter(Collider other)
	{
		//Manages Wizard boss spawn positions.
		if (wizRooms.Contains(other.name))
		{
			room = other.name;
			return;
		}

		if(other.name.Substring(0, other.name.Length - 1) == "EnemyTrigger")
        {
			switch (SceneManager.GetActiveScene().name)
			{
				case "LevelOne":
					levelOne.EnemyActivation(false);
					break;
				case "LevelTwo": //only enemy trigger activation in level 2 is the boss, no need to make a function for this
					levelTwo.boss.active = true;
					break;
				case "LevelThree":
					levelThree.EnemyActivation(false);
					break;
			}
			Destroy(other.gameObject);
			return;
		}

		//Manages enemy activations via triggers in levels.
		switch (other.name)
		{
			case "WizardTrigger":
				levelTwo.DisableWizard(levelTwo.wizards[2]);
				levelTwo.DisableWizard(levelTwo.wizards[4]);
				break;
			case "Fire":
				collision = true;
				break;
			case "WizardShoot(Clone)":
			case "WizardShoot2(Clone)":
				gameManager.playerHealth.ChangeHealth(5);
				Destroy(other.gameObject);
				break;
		}
	}

	private void OnCollisionEnter(Collision other)
	{
		//Environemnt objects that deal damage have non-trigger colliders to separate them from enemy activation triggers.
        if (damageObjects.Contains(other.gameObject.name))
        {
			collision = true;
			if (other.gameObject.name == "Spear")
				other.gameObject.GetComponentInParent<Animation>().Rewind();
        }
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
