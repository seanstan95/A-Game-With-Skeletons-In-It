using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
	
	// Serialized Fields
	[SerializeField] private Image damageImage;
	[SerializeField] private GameManager gameManager;
	[SerializeField] private UIManager UI;

	// Private Fields
	private int _currentHealth = 100;
	private bool dead;

	// Cached Animator Hash Values
	private static readonly int GameOver = Animator.StringToHash("GameOver");
	
	
	private void Update()
	{
		//Set damageImage to red then lerp back to clear to make a smooth transition
		if (damageImage.color != Color.clear)
			damageImage.color = Color.Lerp(damageImage.color, Color.clear, 2f * Time.deltaTime);
	}

	public void ChangeHealth(int amount)
	{
		damageImage.color = new Color(250, 0, 0);
		_currentHealth -= amount;
		UI.playerSlider.value = _currentHealth;

		if (_currentHealth <= 0 && !dead)
		{
			dead = true;
			UI.HUDAnimator.SetTrigger(GameOver);
			StartCoroutine(gameManager.Done());
		}
	}

	public int GetCurrentHealth() => _currentHealth;

	// Reset health status. Used in LevelLoad() in GameManager.cs
	public void HealthReset()
	{
		_currentHealth = 100;
		damageImage.color = Color.clear;
		dead = false;
	}
}
