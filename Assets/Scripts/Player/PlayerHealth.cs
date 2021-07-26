using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
	public bool dead;
	public GameManager gameManager;
	public Image damageImage;
	public int currentHealth = 100;
	public UIManager UI;

	private void Update()
	{
		//Set damageImage to red then lerp back to clear to make a smooth transition
		if (damageImage.color != Color.clear)
			damageImage.color = Color.Lerp(damageImage.color, Color.clear, 2f * Time.deltaTime);
	}

	public void ChangeHealth(int amount)
	{
		damageImage.color = new Color(250, 0, 0);
		currentHealth -= amount;
		UI.playerSlider.value = currentHealth;

		if (currentHealth <= 0 && !dead)
		{
			dead = true;
			UI.HUDAnimator.SetTrigger("GameOver");
			StartCoroutine(gameManager.Done());
		}
	}
}
