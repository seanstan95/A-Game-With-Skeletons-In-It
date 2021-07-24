using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour {

	private static bool damaged;
	public Image damageImage;
	public int currentHealth = 100;

	private void Update () 
	{
		//If the player has been damaged in the last frame, set the color of the damageImage to be red.
		//Then, lerp from red to clear over flashSpeed * Time.deltaTime
		if (damaged) {
            damageImage.color = new Color(250, 0, 0);
            damaged = false;
        }

		if(damageImage.color != Color.clear)
			damageImage.color = Color.Lerp (damageImage.color, Color.clear, 2f * Time.deltaTime);
	}

	public void ChangeHealth(int amount)
	{
		damaged = true;
		currentHealth -= amount;
		UIManager.playerSlider.value = currentHealth;

		if (currentHealth <= 0)
		{
			GameManager.HUD.GetComponent<Animator>().SetTrigger("GameOver");
			StartCoroutine(GameManager.PlayerDead());
		}
	}
}
