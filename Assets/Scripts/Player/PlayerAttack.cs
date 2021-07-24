using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerAttack : MonoBehaviour
{
	private Enemy enemyHit;
	private float timer;
	private int shootableMask;
	private readonly int damagePerHit = 40;
	private Ray ray = new Ray();
	private RaycastHit hitInfo;
    public LineRenderer line;
    public Transform gunEnd;

	private void Start () 
	{
		shootableMask = LayerMask.GetMask ("Shootable");
	}

	private void Update ()
	{
		timer += Time.deltaTime;

		//Checks for the mouse button to be held down, cooldown has been reached, and that the game isn't paused.
		if (Input.GetButton ("Fire1") && timer >= .5f && Time.timeScale == 1 && SceneManager.GetActiveScene().name != "MainMenu") {
			timer = 0f;

			line.enabled = true;
			line.SetPosition (0, gunEnd.position);
			ray.origin = gunEnd.position;
			ray.direction = transform.forward;

			GetComponent<Animator> ().SetTrigger ("Shoot");
			GetComponent<AudioSource> ().Play ();
			Shoot (ray, line);
		}

		//Disable the gun line(s) when the cooldown (multiplied by the time set for how long to display the line) has been reached.
		if (timer >= .5f * .1f) { //.1f = effect display time
			line.enabled = false;
		}
	}

	private void Shoot(Ray ray, LineRenderer line)
	{
		//Physics.Raycast returns true if the ray hits a collider. Check if it hit an enemy (and deal damage if so), then draw the line accordingly.
		if (Physics.Raycast (ray, out hitInfo, 15f, shootableMask)) {
			enemyHit = hitInfo.collider.GetComponent<Enemy> ();
			if (enemyHit != null) {
				if (enemyHit.name == "FinalBoss") {
					if (!FinalBoss.shield) {
						enemyHit.currentHealth -= damagePerHit;
						UIManager.UpdateEnemy (enemyHit);
						enemyHit.Death();
					} else {
						UIManager.levelText.text = "Shoot a target to disable boss's shield.";
					}
				} else {
					enemyHit.currentHealth -= damagePerHit;
					UIManager.UpdateEnemy(enemyHit);
					enemyHit.Death();
				}
			}
			if (hitInfo.collider.name == "Target") {
				FinalBoss.shield = false;
				UIManager.levelText.text = "";
				Destroy (hitInfo.collider.gameObject);
			}
			line.SetPosition (1, hitInfo.point);
		}else{
			line.SetPosition (1, ray.origin + ray.direction * 15f);
		}
	}
}
