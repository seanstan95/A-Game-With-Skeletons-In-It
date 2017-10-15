using UnityEngine;

public class PlayerShooting : MonoBehaviour
{
    public int damagePerShot = 25;
    public float coolDown = 0.15f;
    public float range = 100f;


    float timer;
    Ray shootRay = new Ray();
    RaycastHit shootHitInfo;
    int shootableMask;
    LineRenderer gunLine;
    AudioSource gunAudio;
    float effectsDisplayTime = 0.2f;


    void Awake ()
    {
        shootableMask = LayerMask.GetMask ("Shootable");
        gunLine = GetComponent <LineRenderer> ();
        gunAudio = GetComponent<AudioSource> ();
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

        gunAudio.Play ();

      

        gunLine.enabled = true;
        gunLine.SetPosition (0, this.transform.position);

        shootRay.origin = transform.position;
        shootRay.direction = transform.forward;

        if(Physics.Raycast (shootRay, out shootHitInfo, range, shootableMask))
        {
            /*
            EnemyHealth enemyHealth = shootHitInfo.collider.GetComponent <EnemyHealth> ();
            if(enemyHealth != null)
            {
                enemyHealth.TakeDamage (damagePerShot, shootHitInfo.point);
            }
            */

            gunLine.SetPosition (1, shootHitInfo.point);
        }
        else
        {
            gunLine.SetPosition (1, shootRay.origin + shootRay.direction * range);
        }
    }
}
