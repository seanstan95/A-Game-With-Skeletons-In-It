using UnityEngine;

public class LevelOne : MonoBehaviour {

	private bool end;
	private float endTimer;
    public Animator anim;
	public GameObject ironBars;
	public Skeleton[] skeletons;
    public static int enemyCount;

    private void Update()
	{
        //This function handles various level triggers as the player progresses through the level.
		switch (enemyCount) {
			case 8:
                if (!skeletons[8].active)
                {
                    skeletons[8].active = true;
                    skeletons[9].active = true;
                }
				break;
			case 10:
				if (ironBars.activeSelf)
					ironBars.SetActive (false);
				break;
			case 12:
                if (!skeletons[12].active)
                {
                    skeletons[12].active = true;
                    skeletons[13].active = true;
                }
				break;
			case 14:
                if (!skeletons[14].active)
                {
                    skeletons[14].active = true;
                    skeletons[15].active = true;
                }
				break;
			case 16:
                if (!skeletons[16].active)
                {
                    skeletons[16].active = true;
                }
				break;
			case 17:
				if (endTimer < 1) {
					endTimer += Time.deltaTime;
				} else if(!end){
                    anim.SetTrigger("LevelComplete");
					GameManager.SetState ("LVLONED");
					end = true;
				}
				break;
		}
	}

	public void EnemyTrigger(string trigger)
	{
		//This function handles enemy activations once the player touches certain triggers.
		switch (trigger) {
			case "Trigger1":
                skeletons[0].active = true;
				break;
			case "Trigger2":
                if (!skeletons[1].active)
                {
                    skeletons[1].active = true;
                    skeletons[2].active = true;
                }
				break;
			case "Trigger3":
                if (!skeletons[3].active)
                {
                    skeletons[3].active = true;
                    skeletons[4].active = true;
                    skeletons[5].active = true;
                }
				break;
			case "Trigger4":
                if (!skeletons[6].active)
                {
                    skeletons[6].active = true;
                    skeletons[7].active = true;
                }
				break;
			case "Trigger5":
                if (!skeletons[10].active)
                {
                    skeletons[10].active = true;
                    skeletons[11].active = true;
                }
				break;
		}
	}
}
