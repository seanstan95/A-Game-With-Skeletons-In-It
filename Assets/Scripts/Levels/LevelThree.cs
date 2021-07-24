using UnityEngine;

public class LevelThree : MonoBehaviour {
    public Animator anim;
    public FinalBoss finalBoss;
    public GameObject[] bars;
	public Skeleton[] skeletons;
    public int enemyCount;
    public Wizard[] wizards;
	public WizardBoss wizBoss;

    public void Start()
    {
		GameManager.SetPlayerPosition(-2f, 1.1f, 0.1f, 0f, -90f, 0f);
		GameManager.player.GetComponent<PlayerMove>().UpdateLevel();
		GameManager.player.SetActive(true);
	}
    public void EnemyDied()
    {
		//This function is triggered on enemy death and checks if eemyCount is at certain thresholds for progression.
		enemyCount++;
		switch (enemyCount)
		{
			case 4:
				skeletons[4].active = true; //skeleton boss rematch
				break;
			case 5:
				wizards[0].active = true; //the 4 wizards in the back of the first room
				wizards[1].active = true;
				wizards[2].active = true;
				wizards[3].active = true;
				break;
			case 9:
				bars[0].SetActive(false); //bars at the back of the first room
				break;
			case 10:
				bars[1].SetActive(false); //bars behind wizard boss rematch
				UIManager.levelText.text = "";
				break;
			case 11:
				GameManager.HUD.GetComponent<Animator>().SetTrigger("LevelComplete");
				StartCoroutine(GameManager.LevelDone(GameManager.StateType.MENU));
				break;
			default:
				break;
		}
	}

	public void EnemyTrigger(string trigger)
	{
		//This function is triggered when the player crosses a level trigger and handles enemy activations.
		switch (trigger) {
			case "Trigger1":
				skeletons [0].active = true; //the 4 skeletons in the first room
				skeletons [1].active = true;
				skeletons [2].active = true;
				skeletons [3].active = true;
				break;
			case "WizBossTrigger":
				wizBoss.active = true;
				break;
			case "FinalBossTrigger":
				finalBoss.active = true;
				break;
		}
	}
}
