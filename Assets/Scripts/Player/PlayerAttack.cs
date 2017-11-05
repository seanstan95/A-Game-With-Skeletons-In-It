using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
	public float fireRate;
	public GameObject gunEnd;
	public int damagePerShot;

	float range, timer, effectsDisplayTime;
	GameObject pauseMenu;
	int shootableMask;
	LineRenderer line;
   Ray shootRay = new Ray();
	RaycastHit shootHitInfo;

	void Start()
	{
		//General presets. Changed coolDown to fireRate to avoid confusion with terminology. Effective cooldown is simply 1/fireRate.
		//Example: Fire rate of 2 per second means there is a 0.5 second wait in between each shot. 1/2 = 0.5.
		//Similarly, when the Fire rate is increased to 10 per second with the powerup, the ratio holds (1/10 = .1 seconds between each shot).
		fireRate = 2f;
		damagePerShot = 20;
		effectsDisplayTime = .1f;
		pauseMenu = GameObject.FindGameObjectWithTag ("Pause");
		range = 100f;
        shootableMask = LayerMask.GetMask ("Shootable");
        line = GetComponent <LineRenderer> ();
	}


	void Update ()
	{
		//Timer is always running to maintain cooldown rate, and is reset to 0 every time the player shoots.
		timer += Time.deltaTime;

		//Fire1 is by default set to either left click, or left control. When either is pressed and the cooldown has been reached, continue with shooting a line.
		//Additionally, there is a check that the game isn't paused. Though when paused everything stops in place, it would still be possible to start a shot.
		if(Input.GetButton ("Fire1") && timer >= (1/fireRate) && !pauseMenu.activeSelf)
			Shoot ();

		//Disable the gun line when the cooldown (multiplied by the time set for how long to display the line) has been reached.
		if(timer >= (1/fireRate) * effectsDisplayTime)
			line.enabled = false;
    }

    void Shoot ()
    {
		//Honestly not too sure how most of this actually works, came from a tutorial.
		timer = 0f;

		line.enabled = true;
		line.SetPosition(0, gunEnd.transform.position);

		shootRay.origin = gunEnd.transform.position;
		shootRay.direction = transform.forward;

		if (Physics.Raycast (shootRay, out shootHitInfo, range, shootableMask)) {
			EnemyHealth enemyHealth = shootHitInfo.collider.GetComponent <EnemyHealth> ();
			ObjectHealth objectHealth = shootHitInfo.collider.GetComponentInParent<ObjectHealth>();
			if (enemyHealth != null)
				enemyHealth.TakeDamage (damagePerShot);
			if (objectHealth != null)
				objectHealth.TakeDamage (damagePerShot);
			line.SetPosition (1, shootHitInfo.point);
		} else {
			line.SetPosition (1, shootRay.origin + shootRay.direction * range);
		}
	}
}
