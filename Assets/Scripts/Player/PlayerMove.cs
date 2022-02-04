using UnityEngine;
using UnityEngine.SceneManagement;
using System.Linq;

public class PlayerMove : MonoBehaviour
{
	// Public Fields
	public LevelOne levelOne;
	public LevelTwo levelTwo;
	public LevelThree levelThree;
	public string room;

	// Serialized Fields
	[SerializeField] private GameManager gameManager;
	[SerializeField] private Camera mainCamera;


	// Private Fields
	private bool collision;
	private float timeSinceLastHit;
	private float mouseX;
	private float moveH;
	private float moveV;
	private readonly string[] damageObjects = { "GreatAxe", "Needle", "Cutter", "Spear", "SawBlade" };
	private readonly string[] wizRooms = { "TopLeft", "TopMiddle", "TopRight", "BottomLeft", "BottomMiddle", "BottomRight" };
	private Vector3 movement;
	
	// Cached Animator Hash Values
	private static readonly int Idle = Animator.StringToHash("Idle");
	private static readonly int Walking = Animator.StringToHash("Walking");

	
	private void Awake()
	{
		// Cache Camera.main as it is an expensive lookup
		mainCamera ??= Camera.main;
	}

	private void Start()
	{
		Cursor.lockState = CursorLockMode.Locked;
	}

	private void Update()
	{
		//damageTimer is used to ensure the player can't be hurt by level colliders too quickly.
		if (collision && timeSinceLastHit >= 1)
		{
			collision = false;
			gameManager.playerHealth.ChangeHealth(10);
			timeSinceLastHit = 0f;
		}
		else if (timeSinceLastHit < 1)
			timeSinceLastHit += Time.deltaTime;
	}

	private void FixedUpdate()
	{
		//First, grab the values applied to the movement axis.
		moveH = Input.GetAxisRaw("Horizontal");
		moveV = Input.GetAxisRaw("Vertical");
		mouseX = Input.GetAxisRaw("Mouse X");

		//Move and rotate the player, then animate accordingly.
		movement.Set(moveH, 0f, moveV);
		movement = mainCamera.transform.TransformDirection(movement.normalized * (5f * Time.deltaTime));
		movement.y = 0f;
		transform.position += movement;
		transform.Rotate(new Vector3(0, mouseX, 0) * 4f);

		if (moveH != 0f || moveV != 0f)
		{
			gameManager.playerAnimation.SetBool(Idle, false);
			gameManager.playerAnimation.SetBool(Walking, true);
		}
		else
		{
			gameManager.playerAnimation.SetBool(Walking, false);
			gameManager.playerAnimation.SetBool(Idle, true);
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
		
		if(other.name.StartsWith("EnemyTrigger"))
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
		// Environment objects that deal damage have non-trigger colliders to separate them from enemy activation triggers.
		
		// Return early if collision is not included in damage objects
		if (!damageObjects.Contains(other.gameObject.name)) return;
		
		collision = true;
        if (other.gameObject.name == "Spear")
	        other.gameObject.GetComponentInParent<Animation>().Rewind();
	}

	public void UpdateLevel()
	{
		switch (SceneManager.GetActiveScene().name)
		{
			case "LevelOne":
				levelOne = GameObject.Find("LevelOneScript").GetComponent<LevelOne>();
				break;
			case "LevelTwo":
				levelTwo = GameObject.Find("LevelTwoScript").GetComponent<LevelTwo>();
				break;
			case "LevelThree":
				levelThree = GameObject.Find("LevelThreeScript").GetComponent<LevelThree>();
				break;
		}
	}
}
