using UnityEngine;

public class PlayerShooting : MonoBehaviour
{
    public int damagePerShot = 25;
    public float coolDown = 0.15f;
    public float range = 100f;

    float timer;
    Ray shootRay = new Ray();
    RaycastHit shootHitInfo;
	GameObject gunEnd;
    int shootableMask;
    LineRenderer gunLine;
    //AudioSource gunAudio;  No audio clips yet
    float effectsDisplayTime = 0.2f;


    void Awake ()
    {
		gunEnd = GameObject.FindWithTag ("GunEnd");
        shootableMask = LayerMask.GetMask ("Shootable");
        gunLine = GetComponent <LineRenderer> ();
        //gunAudio = GetComponent<AudioSource> ();  No audio clips yet
    }


    void Update ()
    {
        timer += Time.deltaTime;

		if(Input.GetButton ("Fire1") && timer >= coolDown && Time.timeScale != 0)
        {
            Shoot ();
        }

        if(timer >= coolDown * effectsDisplayTime)
        {
            DisableEffects ();
        }
    }


    public void DisableEffects ()
    {
        gunLine.enabled = false;
  
    }


    void Shoot ()
    {
        timer = 0f;

        //gunAudio.Play ();  No audio clips yet

		gunLine.enabled = true;
		gunLine.SetPosition(0, gunEnd.transform.position);

        shootRay.origin = gunEnd.transform.position;
		shootRay.direction = transform.forward;

        if(Physics.Raycast (shootRay, out shootHitInfo, range, shootableMask))
        {
            
            EnemyHealth enemyHealth = shootHitInfo.collider.GetComponent <EnemyHealth> ();
			if (enemyHealth != null) {
				Debug.Log ("Not null enemy health");
				enemyHealth.TakeDamage (damagePerShot, shootHitInfo.point);
			} else {
				Debug.Log ("Null enemy health");
			}
            

            gunLine.SetPosition (1, shootHitInfo.point);
        }
        else
        {
            gunLine.SetPosition (1, shootRay.origin + shootRay.direction * range);
        }
    }
}
