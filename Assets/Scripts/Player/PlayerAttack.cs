using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerAttack : MonoBehaviour
{
	public FinalBoss finalBoss;
	private float timer;
	private Ray ray = new Ray();
	private RaycastHit hitInfo;
	private readonly int shootableMask = 256, damagePerHit = 40;
	public GameManager gameManager;
	public LineRenderer lineRender;
	public Transform gunEnd;
	public UIManager UI;

	public void UpdateLevel()
    {
		finalBoss = GameObject.Find("LevelThreeScript").GetComponent<LevelThree>().finalBoss;
    }

	private void Update()
	{
		timer += Time.deltaTime;

		//Checks for the mouse button to be held down, cooldown has been reached, and that the game isn't paused.
		if (Input.GetButton("Fire1") && timer >= .5f && Time.timeScale == 1)
		{
			timer = 0f;

			lineRender.enabled = true;
			lineRender.SetPosition(0, gunEnd.position);
			ray.origin = gunEnd.position;
			ray.direction = transform.forward;

			gameManager.playerAnimation.SetTrigger("Shoot");
			gameManager.sfxSource.Play();
			Shoot(ray, lineRender);
		}

		//Disable the gun line(s) when the cooldown (multiplied by the time set for how long to display the line) has been reached.
		if (timer >= .5f * .1f) //.1f = effect display time
			lineRender.enabled = false;
	}

	private void Shoot(Ray ray, LineRenderer line)
	{
		//Physics.Raycast returns true if the ray hits a collider. Check if it hit an enemy (and deal damage if so), then draw the line accordingly.
		if (Physics.Raycast(ray, out hitInfo, 15f, shootableMask))
		{
			Enemy enemyHit = hitInfo.collider.GetComponent<Enemy>();
			if (enemyHit != null)
			{
				if (enemyHit.name == "FinalBoss" && finalBoss.shield)
					UI.levelText.text = "Shoot a target to disable boss's shield.";
				else
				{
					enemyHit.currentHealth -= damagePerHit;
					UI.UpdateEnemy(enemyHit);
					enemyHit.Death();
				}
			}
			if (hitInfo.collider.name == "Target")
			{
				finalBoss.shield = false;
				UI.levelText.text = "";
				Destroy(hitInfo.collider.gameObject);
			}
			line.SetPosition(1, hitInfo.point);
		}
		else
			line.SetPosition(1, ray.origin + ray.direction * 15f);
	}
}
