using UnityEngine;

public class LevelOne : MonoBehaviour
{
	private bool lerpBars;
	private GameManager gameManager;
	private UIManager UI;
	private Vector3 barLerpPos;
	public GameObject ironBars;
	public int enemyCount;
	public Skeleton[] skeletons = new Skeleton[16];

	public void Start()
	{
		gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
		gameManager.playerMove.UpdateLevel();
		gameManager.player.SetActive(true);
		gameManager.playerTrans.position = new Vector3(-29, 0, -33);
		gameManager.playerTrans.eulerAngles = new Vector3(0f, 90f, 0f);
		UI = GameObject.Find("UIManager").GetComponent<UIManager>();
		barLerpPos = new Vector3(ironBars.transform.localPosition.x, ironBars.transform.localPosition.y, -25f);
	}

    public void Update()
    {
		if (lerpBars && Time.timeScale == 1)
			ironBars.transform.localPosition = Vector3.Lerp(ironBars.transform.localPosition, barLerpPos, .015f);
		if (ironBars.transform.localPosition.z < -24.5f)
			lerpBars = false;
    }

    public void EnemyActivation(bool increment)
	{
		//This function is triggered on enemy death and checks if eemyCount is at certain thresholds for progression.
		if (increment) {
			enemyCount++;
			if (enemyCount == 1 || enemyCount == 3 || enemyCount == 6 || enemyCount == 11)
				return;
        }

		switch (enemyCount)
		{
			case 0:
				skeletons[0].active = true; //first of 6 in first hallway
				break;
			case 1:
				skeletons[1].active = true; //second & 3rd of 6 in first hallway
				skeletons[2].active = true;
				break;
			case 3:
				skeletons[3].active = true; //final 3 in first hallway
				skeletons[4].active = true;
				skeletons[5].active = true;
				break;
			case 6:
				skeletons[6].active = true; //first 2 in second room
				skeletons[7].active = true;
				break;
			case 8:
				skeletons[8].active = true; //last 2 in second room
				skeletons[9].active = true;
				break;
			case 10:
				lerpBars = true;
				skeletons[10].active = true;
				break;
			case 11:
				skeletons[11].active = true; //first 2 in final room
				skeletons[12].active = true;
				break;
			case 13:
				skeletons[13].active = true; //3rd & 4th in final room
				skeletons[14].active = true;
				break;
			case 15:
				skeletons[15].active = true; //last 2 in final room
				skeletons[16].active = true;
				break;
			case 17:
				skeletons[17].active = true; //boss
				break;
			case 18:
				UI.HUDAnimator.SetTrigger("LevelComplete-Start");
				StartCoroutine(gameManager.Done("LevelTwo", gameManager.clips[1]));
				break;
		}
	}
}