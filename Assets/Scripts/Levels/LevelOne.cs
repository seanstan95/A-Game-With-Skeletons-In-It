using UnityEngine;

public class LevelOne : MonoBehaviour {
	public GameObject ironBars;
	public int enemyCount;
	public Animator anim;
	public Skeleton[] skeletons;

    public void Start()
    {
		GameManager.player.GetComponent<PlayerMove>().UpdateLevel();
		GameManager.player.SetActive(true);
    }
    public void EnemyDied()
    {
		//This function is triggered on enemy death and checks if eemyCount is at certain thresholds for progression.
		enemyCount++;
		switch (enemyCount)
		{
			case 8:
				skeletons[8].active = true; //last 2 in second room
				skeletons[9].active = true;
				break;
			case 10:
				ironBars.SetActive(false);
				break;
			case 12:
				skeletons[12].active = true; //3rd & 4th in final room
				skeletons[13].active = true;
				break;
			case 14:
				skeletons[14].active = true; //last 2 in final room
				skeletons[15].active = true;
				break;
			case 16:
				skeletons[16].active = true; //boss
				break;
			case 17:
				GameManager.HUD.GetComponent<Animator>().SetTrigger("LevelComplete");
				StartCoroutine(GameManager.LevelDone(GameManager.StateType.LVLTWOT));
				break;
		}
	}

	public void EnemyTrigger(string trigger)
	{
		//This function is triggered when the player crosses a level trigger and handles enemy activations.
		switch (trigger)
		{
			case "Trigger1":
				skeletons[0].active = true; //first of 6 in first hallway
				break;
			case "Trigger2":
				skeletons[1].active = true; //second & 3rd of 6 in first hallway
				skeletons[2].active = true;
				break;
			case "Trigger3":
				skeletons[3].active = true; //final 3 in first hallway
				skeletons[4].active = true;
				skeletons[5].active = true;
				break;
			case "Trigger4":
				skeletons[6].active = true; //first 2 in second room
				skeletons[7].active = true;
				break;
			case "Trigger5":
				skeletons[10].active = true; //first 2 in final room
				skeletons[11].active = true;
				break;
		}
	}
}
