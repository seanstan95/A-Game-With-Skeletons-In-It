using UnityEngine;

/*
 	* Controls all aspects regarding the player's ability to attack enemies
 	* If the player is holding down the fire button and the cooldown has been reached, shoot the gun
 	* Whenever the bullet collides with an enemy, deals damage via the TakeDamage function of that enemy's health script
*/

public class PlayerAttack : MonoBehaviour
{
	public float coolDown;
	public GameObject gunEnd;
	public int damagePerShot;

	int shootableMask;
	float range, timer, effectsDisplayTime;
	LineRenderer gunLine;
    Ray shootRay = new Ray();
	RaycastHit shootHitInfo;


    void Awake ()
    {
		coolDown = .5f;
		damagePerShot = 20;
		effectsDisplayTime = 0.1f;
		range = 100f;
        shootableMask = LayerMask.GetMask ("Shootable");
        gunLine = GetComponent <LineRenderer> ();
    }


    void Update ()
    {
        timer += Time.deltaTime;

		if(Input.GetButton ("Fire1") && timer > coolDown)
            Shoot ();

		if(timer >= coolDown * effectsDisplayTime)
			gunLine.enabled = false;
    }

    void Shoot ()
    {
        timer = 0f;

		gunLine.enabled = true;
		gunLine.SetPosition(0, gunEnd.transform.position);

        shootRay.origin = gunEnd.transform.position;
		shootRay.direction = transform.forward;

		if (Physics.Raycast (shootRay, out shootHitInfo, range, shootableMask)) {
			EnemyHealth enemyHealth = shootHitInfo.collider.GetComponent <EnemyHealth> ();
			if (enemyHealth != null)
				enemyHealth.TakeDamage (damagePerShot);
			gunLine.SetPosition (1, shootHitInfo.point);
		} else {
			gunLine.SetPosition (1, shootRay.origin + shootRay.direction * range);
		}
    }
}
