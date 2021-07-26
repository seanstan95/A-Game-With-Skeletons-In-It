using UnityEngine;

public class LevelThree : MonoBehaviour
{
	private GameManager gameManager;
	private UIManager UI;
	public FinalBoss finalBoss;
	public GameObject[] bars;
	public int enemyCount;
	public Skeleton[] skeletons;
	public Wizard[] wizards;
	public WizardBoss wizBoss;

	public void Start()
	{
		gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
		gameManager.player.SetActive(true);
		gameManager.playerMove.UpdateLevel();
		gameManager.playerTrans.position = new Vector3(-2f, 1.1f, 0.1f);
		gameManager.playerTrans.eulerAngles = new Vector3(0f, -90f, 0f);
		UI = GameObject.Find("UIManager").GetComponent<UIManager>();
		UI.HUDAnimator.SetTrigger("LevelComplete-End");
		UI.ResetUI();
	}

	public void EnemyActivation(bool increment)
	{
		//This function is triggered on enemy death and checks if eemyCount is at certain thresholds for progression.
		if (increment)
			enemyCount++;

		switch (enemyCount)
		{
			case 0:
				skeletons[0].active = true; //the 4 skeletons in the first room
				skeletons[1].active = true;
				skeletons[2].active = true;
				skeletons[3].active = true;
				break;
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
				if (!increment)
					wizBoss.active = true;
				else
					bars[0].SetActive(false); //bars at the back of the first room
				break;
			case 10:
				if (!increment)
				{
					gameManager.playerAttack.UpdateLevel();
					finalBoss.active = true;
				}
				else
				{
					bars[1].SetActive(false); //bars behind wizard boss rematch
					UI.levelText.text = "";
				}
				break;
			case 11:
				UI.HUDAnimator.SetTrigger("GameComplete");
				StartCoroutine(gameManager.Done(gameDone: true));
				break;
			default:
				break;
		}
	}
}
