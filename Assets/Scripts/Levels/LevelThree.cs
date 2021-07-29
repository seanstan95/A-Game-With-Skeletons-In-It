using UnityEngine;

public class LevelThree : MonoBehaviour
{
	private bool lerpBars1, lerpBars2;
	private GameManager gameManager;
	private UIManager UI;
	private Vector3 bar1LerpPos, bar2LerpPos;
	public FinalBoss finalBoss;
	public GameObject bars1, bars2;
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
		bar1LerpPos = new Vector3(bars1.transform.localPosition.x, bars1.transform.localPosition.y, -25f);
		bar2LerpPos = new Vector3(bars2.transform.localPosition.x, bars2.transform.localPosition.y, -25f);
	}

    public void Update()
    {
		if (lerpBars1 && Time.timeScale == 1)
			bars1.transform.localPosition = Vector3.Lerp(bars1.transform.localPosition, bar1LerpPos, .01f);
		if (lerpBars2 && Time.timeScale == 1)
			bars2.transform.localPosition = Vector3.Lerp(bars2.transform.localPosition, bar2LerpPos, .01f);

		if (bars1.transform.localPosition.z < -24.5f)
			lerpBars1 = false;
		if (bars2.transform.localPosition.z < -24.5f)
			lerpBars2 = false;
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
					lerpBars1 = true; //bars at the back of the first room
				break;
			case 10:
				if (!increment)
				{
					gameManager.playerAttack.UpdateLevel();
					finalBoss.active = true;
				}
				else
				{
					lerpBars2 = true; //bars behind wizard boss rematch
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
