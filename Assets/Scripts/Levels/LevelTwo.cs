using UnityEngine;

public class LevelTwo : MonoBehaviour {

	private bool end;
    private float endTimer;
    private int spawnNum;
	public int enemyCount;
    public Animator anim;
    public GameObject skeleSpawn;
    public GameObject[] bars, spawnPoints;
	public Wizard[] wizards;
	public WizardBoss boss;

	private void Start()
	{
		InvokeRepeating ("Spawn", 5f, 3f);
		UIManager.levelText.text = "Defeat 10 Skeletons to advance!";
	}

	private void Update()
	{
        //Once 10 skeletons are defeated: kill remaining ones, unblock exit, and activate next enemies.
		if (enemyCount == 10 && bars[0].activeSelf) {
			CancelInvoke ("Spawn");
			foreach (GameObject enemy in GameObject.FindGameObjectsWithTag("NormalEnemy")) {
			    enemy.GetComponent<Skeleton> ().currentHealth -= 120;
			}
			bars [0].SetActive (false);
			bars [1].SetActive (false);
			bars [2].SetActive (false);
			wizards [0].active = true;
			wizards [1].active = true;
			UIManager.levelText.text = "";
		}

		if (enemyCount >= 12 && !wizards[2].active) {
			wizards [2].active = true;
			wizards [3].active = true;
			wizards [4].active = true;
		}

		if (boss.currentHealth <= 0) {
			if (endTimer < 1) {
				endTimer += Time.deltaTime;
			} else if(!end){
				anim.SetTrigger ("LevelComplete");
				GameManager.SetState ("LVLTWOD");
				end = true;
			}
		}
	}

	public void Spawn()
	{
		spawnNum = Random.Range (0, spawnPoints.Length);
        Instantiate(skeleSpawn, spawnPoints[spawnNum].transform.position, spawnPoints[spawnNum].transform.rotation).GetComponent<Skeleton>().active = true;
	}
}
