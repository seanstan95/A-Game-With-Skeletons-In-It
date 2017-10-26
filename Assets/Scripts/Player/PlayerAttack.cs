using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
	public float coolDown;
	public GameObject gunEnd;
	public int damagePerShot;

	float range, timer, effectsDisplayTime;
	int shootableMask;
	LineRenderer line;
    Ray shootRay = new Ray();
	RaycastHit shootHitInfo;

    void Awake ()
    {
		//General presets.
		coolDown = .5f;
		damagePerShot = 20;
		effectsDisplayTime = .1f;
		range = 100f;
        shootableMask = LayerMask.GetMask ("Shootable");
        line = GetComponent <LineRenderer> ();
    }


    void Update ()
    {
		//Timer is always running to maintain cooldown rate, and is reset to 0 every time the player shoots.
        timer += Time.deltaTime;

		//Fire1 is by default set to either left click, or left control. When either is pressed and the cooldown has been reached, continue with shooting a line.
		if(Input.GetButton ("Fire1") && timer >= coolDown)
            Shoot ();

		//Disable the gun line when the cooldown (multiplied by the time set for how long to display the line) has been reached.
		if(timer >= coolDown * effectsDisplayTime)
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
			if (enemyHealth != null)
				enemyHealth.TakeDamage (damagePerShot);
			line.SetPosition (1, shootHitInfo.point);
		} else {
			line.SetPosition (1, shootRay.origin + shootRay.direction * range);
		}
    }
}
