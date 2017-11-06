using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
	public float fireRate;
	public GameObject gunEnd;
	public int damagePerShot;
    public GameObject LineRender2;
    public GameObject LineRender3;
    public GameObject LineRender4;
    public GameObject LineRender5;
    public bool spread;


    float range, timer, effectsDisplayTime;
	GameObject pauseMenu;
	int shootableMask;
   
    LineRenderer centerLine;
    LineRenderer nearRightLine;
    LineRenderer farRightLine;
    LineRenderer nearLeftLine;
    LineRenderer farLeftLine;

    Ray centerShootRay = new Ray();
    Ray nearRightShootRay = new Ray();
    Ray farRightShootRay = new Ray();
    Ray nearLeftShootRay = new Ray();
    Ray farLeftShootRay = new Ray();

    RaycastHit centerShootHitInfo;
    RaycastHit nearRightShootHitInfo;
    RaycastHit farRightShootHitInfo;
    RaycastHit nearLeftShootHitInfo;
    RaycastHit farLeftShootHitInfo;


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
        centerLine = GetComponent <LineRenderer> ();
        nearRightLine = LineRender3.GetComponent<LineRenderer>();
        farRightLine = LineRender2.GetComponent<LineRenderer>();
        nearLeftLine = LineRender4.GetComponent<LineRenderer>();
        farLeftLine = LineRender5.GetComponent<LineRenderer>();
        //spread = false;
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
        if (timer >= (1 / fireRate) * effectsDisplayTime)
        {
            centerLine.enabled = false;
            nearRightLine.enabled = false;
            farRightLine.enabled = false;
            nearLeftLine.enabled = false;
            farLeftLine.enabled = false;
        }
    }

    //For standard shot, lineRenderer and Raycast is initalized and fired. Line start point it set
    //to gun tip. Ray is drawn to first shootable object or drawn to max range. if it hits something
    //apply damage and draw line to that object. Spread shot utilizes extra line renderers attached as
    //empty child game objects.
    void Shoot ()
    {
        //If else will chance to a switch statement if more then 2 firetypes are implimented
        if (spread == false)
        {
            //Honestly not too sure how most of this actually works, came from a tutorial.
            timer = 0f;


            centerLine.enabled = true;
            centerLine.SetPosition(0, gunEnd.transform.position);

            centerShootRay.origin = gunEnd.transform.position;
            centerShootRay.direction = transform.forward;

            if (Physics.Raycast(centerShootRay, out centerShootHitInfo, range, shootableMask))
            {
                EnemyHealth enemyHealth = centerShootHitInfo.collider.GetComponent<EnemyHealth>();
                ObjectHealth objectHealth = centerShootHitInfo.collider.GetComponentInParent<ObjectHealth>();
                if (enemyHealth != null)
                    enemyHealth.TakeDamage(damagePerShot);
                if (objectHealth != null)
                    objectHealth.TakeDamage(damagePerShot);
                centerLine.SetPosition(1, centerShootHitInfo.point);
            }
            else
            {
                centerLine.SetPosition(1, centerShootRay.origin + centerShootRay.direction * range);
            }
        }
        else
        {

            timer = 0f;

            centerLine.enabled = true;
            centerLine.SetPosition(0, gunEnd.transform.position);
            centerShootRay.origin = gunEnd.transform.position;
            centerShootRay.direction = transform.forward;

            nearRightLine.enabled = true;
            nearRightLine.SetPosition(0, gunEnd.transform.position);
            nearRightShootRay.origin = gunEnd.transform.position;
            nearRightShootRay.direction = transform.forward + (transform.right / 4);

            farRightLine.enabled = true;
            farRightLine.SetPosition(0, gunEnd.transform.position);
            farRightShootRay.origin = gunEnd.transform.position;
            farRightShootRay.direction = transform.forward + (transform.right / 2);

            nearLeftLine.enabled = true;
            nearLeftLine.SetPosition(0, gunEnd.transform.position);
            nearLeftShootRay.origin = gunEnd.transform.position;
            nearLeftShootRay.direction = transform.forward - (transform.right / 4);

            farLeftLine.enabled = true;
            farLeftLine.SetPosition(0, gunEnd.transform.position);
            farLeftShootRay.origin = gunEnd.transform.position;
            farLeftShootRay.direction = transform.forward - (transform.right / 2);



            if (Physics.Raycast(centerShootRay, out centerShootHitInfo, range, shootableMask))
            {
                EnemyHealth enemyHealth = centerShootHitInfo.collider.GetComponent<EnemyHealth>();
                ObjectHealth objectHealth = centerShootHitInfo.collider.GetComponentInParent<ObjectHealth>();
                if (enemyHealth != null)
                    enemyHealth.TakeDamage(damagePerShot);
                if (objectHealth != null)
                    objectHealth.TakeDamage(damagePerShot);
                centerLine.SetPosition(1, centerShootHitInfo.point);
            }
            else
            {
                centerLine.SetPosition(1, centerShootRay.origin + centerShootRay.direction * range);
            }

            if (Physics.Raycast(nearRightShootRay, out nearRightShootHitInfo, range, shootableMask))
            {
                EnemyHealth enemyHealth = nearRightShootHitInfo.collider.GetComponent<EnemyHealth>();
                ObjectHealth objectHealth = nearRightShootHitInfo.collider.GetComponentInParent<ObjectHealth>();
                if (enemyHealth != null)
                    enemyHealth.TakeDamage(damagePerShot);
                if (objectHealth != null)
                    objectHealth.TakeDamage(damagePerShot);
                nearRightLine.SetPosition(1, nearRightShootHitInfo.point);
            }
            else
            {
                nearRightLine.SetPosition(1, nearRightShootRay.origin + nearRightShootRay.direction * range);
            }

            if (Physics.Raycast(farRightShootRay, out farRightShootHitInfo, range, shootableMask))
            {
                EnemyHealth enemyHealth = farRightShootHitInfo.collider.GetComponent<EnemyHealth>();
                ObjectHealth objectHealth = farRightShootHitInfo.collider.GetComponentInParent<ObjectHealth>();
                if (enemyHealth != null)
                    enemyHealth.TakeDamage(damagePerShot);
                if (objectHealth != null)
                    objectHealth.TakeDamage(damagePerShot);
                farRightLine.SetPosition(1, farRightShootHitInfo.point);
            }
            else
            {
                farRightLine.SetPosition(1, farRightShootRay.origin + farRightShootRay.direction * range);
            }

            if (Physics.Raycast(nearLeftShootRay, out nearLeftShootHitInfo, range, shootableMask))
            {
                EnemyHealth enemyHealth = nearLeftShootHitInfo.collider.GetComponent<EnemyHealth>();
                ObjectHealth objectHealth = nearLeftShootHitInfo.collider.GetComponentInParent<ObjectHealth>();
                if (enemyHealth != null)
                    enemyHealth.TakeDamage(damagePerShot);
                if (objectHealth != null)
                    objectHealth.TakeDamage(damagePerShot);
                nearLeftLine.SetPosition(1, nearLeftShootHitInfo.point);
            }
            else
            {
                nearLeftLine.SetPosition(1, nearLeftShootRay.origin + nearLeftShootRay.direction * range);
            }

            if (Physics.Raycast(farLeftShootRay, out farLeftShootHitInfo, range, shootableMask))
            {
                EnemyHealth enemyHealth = farLeftShootHitInfo.collider.GetComponent<EnemyHealth>();
                ObjectHealth objectHealth = farLeftShootHitInfo.collider.GetComponentInParent<ObjectHealth>();
                if (enemyHealth != null)
                    enemyHealth.TakeDamage(damagePerShot);
                if (objectHealth != null)
                    objectHealth.TakeDamage(damagePerShot);
                farLeftLine.SetPosition(1, farLeftShootHitInfo.point);
            }
            else
            {
                farLeftLine.SetPosition(1, farLeftShootRay.origin + farLeftShootRay.direction * range);
            }

        }

    }
}
