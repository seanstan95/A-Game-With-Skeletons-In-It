using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
	public bool spread;
	public float fireRate;
	public GameObject gunEnd, farLeftRenderer, farRightRenderer, nearLeftRenderer, nearRightRenderer;
	public int damagePerShot;

	Skeleton skeleton;
	float range, timer, effectsDisplayTime;
	int shootableMask;
	LineRenderer centerLine, farLeftLine, farRightLine, nearLeftLine, nearRightLine;
	ObjectHealth objectHealth;
	Ray ray = new Ray();
	RaycastHit hitInfo;

	void Start()
	{
		//General presets. Changed coolDown to fireRate to avoid confusion with terminology. Effective cooldown is simply 1/fireRate.
		//Example: Fire rate of 2 per second means there is a 0.5 second wait in between each shot. 1/2 = 0.5.
		//Similarly, when the Fire rate is increased to 10 per second with the powerup, the ratio holds (1/10 = .1 seconds between each shot).
		fireRate = 2f;
		damagePerShot = 20;
		effectsDisplayTime = .1f;
		range = 100f;
		shootableMask = LayerMask.GetMask ("Shootable");
		centerLine = GetComponent <LineRenderer> ();
		farLeftLine = farLeftRenderer.GetComponent<LineRenderer>();
		farRightLine = farRightRenderer.GetComponent<LineRenderer>();
		nearLeftLine = nearLeftRenderer.GetComponent<LineRenderer>();
		nearRightLine = nearRightRenderer.GetComponent<LineRenderer>();
	}


	void Update ()
	{
		//Timer is always running to maintain cooldown rate, and is reset to 0 every time the player shoots.
		timer += Time.deltaTime;

		//Fire1 is by default set to either left click, or left control. When either is pressed and the cooldown has been reached, continue with shooting a line.
		//Additionally, there is a check that the game isn't paused. Though when paused everything stops in place, it would still be possible to start a shot.
		if (Input.GetButton ("Fire1") && timer >= (1 / fireRate) && !UIManager.pauseMenu.activeSelf) {
			//First, reset the cooldown timer to 0. Then enable/setup the center line (regardless of if spread shot or not, center line will always be active).
			timer = 0f;

			centerLine.enabled = true;
			centerLine.SetPosition (0, gunEnd.transform.position);
			ray.origin = gunEnd.transform.position;
			ray.direction = transform.forward;

			Shoot (ray, centerLine);

			if (spread) {
				//If spread shot is active, fire the extra 4 shots as well. Angle() returns the ray, adjusted by an angle.
				//Angle() also handles the setup for the line just like centerLine above.
				Shoot (Angle (ray, nearLeftLine, -.5f), nearLeftLine);
				Shoot (Angle (ray, farLeftLine, -.25f), farLeftLine);
				Shoot (Angle (ray, nearRightLine, .5f), nearRightLine);
				Shoot (Angle (ray, farRightLine, .25f), farRightLine);
			}
		}

		//Disable the gun line(s) when the cooldown (multiplied by the time set for how long to display the line) has been reached.
		if (timer >= (1 / fireRate) * effectsDisplayTime) {
			centerLine.enabled = false;
			nearRightLine.enabled = false;
			farRightLine.enabled = false;
			nearLeftLine.enabled = false;
			farLeftLine.enabled = false;
		}
	}

	void Shoot(Ray ray, LineRenderer line)
	{
		//Physics.Raycast returns true if the ray hits a collider. Check if it hit an enemy or object, and deal damage if so, then draw the line accordingly.
		if (Physics.Raycast (ray, out hitInfo, range, shootableMask)) {
			skeleton = hitInfo.collider.GetComponent<Skeleton> ();
			objectHealth = hitInfo.collider.GetComponentInParent<ObjectHealth> ();
			if (skeleton != null) {
				skeleton.TakeDamage (damagePerShot);
				GameObject.Find("HUD").GetComponent<UIManager>().UpdateEnemy (skeleton);
			}
			if (objectHealth != null)
				objectHealth.TakeDamage (damagePerShot);
			line.SetPosition (1, hitInfo.point);
		}else{
			line.SetPosition (1, ray.origin + ray.direction * range);
		}
	}

	Ray Angle(Ray ray, LineRenderer line, float angleAdjust)
	{
		line.enabled = true;
		line.SetPosition (0, gunEnd.transform.position);
		ray.direction = transform.forward + (transform.right * angleAdjust);
		return ray;
	}
}
