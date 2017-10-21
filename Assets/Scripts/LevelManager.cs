using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour {

	public float waitToRespawn;
	public PlayerController player;
	public GameObject deathSplosion;
	public int coinCount;
	public Text coinText;
	public Image heart1;
	public Image heart2;
	public Image heart3;
	public Sprite heartFull;
	public Sprite heartHalf;
	public Sprite heartEmpty;
	public int maxHealth;
	public int healthCount;
	public bool invincible;
	public int startingLives;
	public int currentLives;
	public Text livesText;
	public GameObject gameOverScreen;

	private bool respawning;
	private ResetOnRespawn[] objectsToReset;

	// Use this for initialization
	void Start () {
		player = FindObjectOfType<PlayerController>();
		UpdateCoinCount();
		healthCount = maxHealth;
		objectsToReset = FindObjectsOfType<ResetOnRespawn>();
		currentLives = startingLives;
		UpdateLives();
	}
	
	// Update is called once per frame
	void Update ()
	{
		if (healthCount <= 0 && !respawning) {
			Respawn();
		}		
	}

	public void Respawn ()
	{
		if (respawning) {
			return;
		}

		currentLives -= 1;
		UpdateLives ();

		if (currentLives >= 0) {
		respawning = true;
			StartCoroutine ("RespawnCo");
		} else {
			player.gameObject.SetActive(false);
			gameOverScreen.SetActive(true);
		}
	}

	public IEnumerator RespawnCo ()
	{
		player.gameObject.SetActive (false);

		Instantiate (deathSplosion, player.transform.position, player.transform.rotation);

		yield return new WaitForSeconds (waitToRespawn);

		healthCount = maxHealth;
		respawning = false;
		UpdateHeartMeter ();

		coinCount = 0;
		UpdateCoinCount();

		player.transform.position = player.respawnPosition;
		player.gameObject.SetActive (true);

		for (int i = 0; i < objectsToReset.Length; i++) {
			objectsToReset[i].gameObject.SetActive(true);
			objectsToReset[i].ResetObject();
		}

	}

	public void AddCoins (int coinsToAdd)
	{
		coinCount += coinsToAdd;

		UpdateCoinCount();
	}

	public void HurtPlayer (int damageToTake)
	{
		if (!invincible) {
			healthCount -= damageToTake;
			UpdateHeartMeter ();

			player.Knockback ();
		}
	}

	public void GiveHealth (int healthToGive)
	{
		healthCount += healthToGive;

		if (healthCount > maxHealth) {
			healthCount = maxHealth;
		}

		UpdateHeartMeter();
	}

	public void UpdateHeartMeter ()
	{
		switch (healthCount) {
			case 6:
				heart1.sprite = heartFull;
				heart2.sprite = heartFull;
				heart3.sprite = heartFull;
				return;
			case 5:
				heart1.sprite = heartFull;
				heart2.sprite = heartFull;
				heart3.sprite = heartHalf;
				return;
			case 4:
				heart1.sprite = heartFull;
				heart2.sprite = heartFull;
				heart3.sprite = heartEmpty;
				return;
			case 3:
				heart1.sprite = heartFull;
				heart2.sprite = heartHalf;
				heart3.sprite = heartEmpty;
				return;
			case 2:
				heart1.sprite = heartFull;
				heart2.sprite = heartEmpty;
				heart3.sprite = heartEmpty;
				return;
			case 1:
				heart1.sprite = heartHalf;
				heart2.sprite = heartEmpty;
				heart3.sprite = heartEmpty;
				return;
			case 0:
			default:
				heart1.sprite = heartEmpty;
				heart2.sprite = heartEmpty;
				heart3.sprite = heartEmpty;
				return;
		}
	}

	public void AddLives (int livesToAdd)
	{
		currentLives += livesToAdd;
		UpdateLives();
	}

	private void UpdateCoinCount ()
	{
		coinText.text = "Coins: " + coinCount;
	}

	private void UpdateLives ()
	{
		livesText.text = "Lives x " + currentLives;
	}
}
