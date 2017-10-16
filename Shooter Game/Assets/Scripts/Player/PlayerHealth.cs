using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour {

	public int startHealth = 100;
	public int currentHealth;
	public Slider healthSlider;
	public Image damageImage;
	//public AudioClip death;  No audio clips yet
	public float flashSpeed = 2f;
	public Color flashColour = new Color(153, 0, 204);

	Animator anim;
	//AudioSource playerAudio;  No audio clips yet
	PlayerMove playerMove;
	PlayerShooting playerShooting;
	bool isDead;
	bool damaged;


	// Use this for initialization
	void Awake () {
		anim = GetComponent<Animator> ();

		//playerAudio = GetComponent<AudioSource> ();  No audio clips yet
		playerMove = GetComponent<PlayerMove> ();
		playerShooting = GetComponent<PlayerShooting> ();
		currentHealth = startHealth;
	}
	
	// Update is called once per frame
	void Update () {
		if (damaged) {
			damageImage.color = flashColour;
		}else{
			damageImage.color = Color.Lerp (damageImage.color, Color.clear, flashSpeed * Time.deltaTime);
		}
		damaged = false;
	}

	public void TakeDamage(int amount)
	{
		damaged = true;
		currentHealth -= amount;
		healthSlider.value = currentHealth;
		//playerAudio.Play();  No audio clips yet

		if(currentHealth <= 0 && !isDead)
		{
			Death();
		}
	}

	void Death()
	{
		isDead = true;

		anim.SetTrigger ("Dead");
		//playerAudio.clip = deathClip;  No audio clips yet
		//playerAudio.Play();  No audio clips yet

		playerMove.enabled = false;
		playerShooting.enabled = false;
	}
}
