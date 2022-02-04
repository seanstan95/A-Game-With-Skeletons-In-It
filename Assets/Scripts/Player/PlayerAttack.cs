using System;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{ 
	// Serialized Fields
	[SerializeField] private LineRenderer lineRender;
	[SerializeField] private Transform gunEnd;
	[SerializeField] private GameManager gameManager;
	[SerializeField] private UIManager UI;
	
	// Private Fields
	private readonly int shootableMask = 256;
	private readonly int damagePerHit = 40;
	private float timer;
	private RaycastHit hitInfo;
	private Ray ray = new Ray();
	private FinalBoss finalBoss;

	// Cached Animator Hashes 
	private static readonly int Shoot1 = Animator.StringToHash("Shoot");

	public void UpdateLevel()
    {
		finalBoss = GameObject.Find("LevelThreeScript").GetComponent<LevelThree>().finalBoss;
    }

	private void Update()
	{
		timer += Time.deltaTime;

		//Checks for the mouse button to be held down, cooldown has been reached, and that the game isn't paused.
		if (Input.GetButton("Fire1") && timer >= .5f && Math.Abs(Time.timeScale - 1) < float.Epsilon)
		{
			timer = 0f;

			Vector3 gunEndPosition = gunEnd.position;
			lineRender.enabled = true;
			lineRender.SetPosition(0, gunEndPosition);
			ray.origin = gunEndPosition;
			ray.direction = transform.forward;

			gameManager.playerAnimation.SetTrigger(Shoot1);
			gameManager.sfxSource.Play();
			Shoot(ray, lineRender);
		}

		//Disable the gun line(s) when the cooldown (multiplied by the time set for how long to display the line) has been reached.
		if (timer >= .5f * .1f) //.1f = effect display time
			lineRender.enabled = false;
	}

	private void Shoot(Ray rayShot, LineRenderer line)
	{
		//Physics.Raycast returns true if the ray hits a collider. Check if it hit an enemy (and deal damage if so), then draw the line accordingly.
		if (Physics.Raycast(rayShot, out hitInfo, 15f, shootableMask))
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
			line.SetPosition(1, rayShot.origin + rayShot.direction * 15f);
	}
}
