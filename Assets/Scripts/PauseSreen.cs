using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseSreen : MonoBehaviour {

	public string levelSelect;
	public string mainMenu;
	public GameObject pauseScreen;

	private bool gameIsPaused;
	private LevelManager levelManager;
	private PlayerController player;

	// Use this for initialization
	void Start () {
		levelManager = FindObjectOfType<LevelManager>();
		player = FindObjectOfType<PlayerController>();
	}
	
	// Update is called once per frame
	void Update ()
	{
		if (Input.GetButtonDown ("Pause") && !gameIsPaused) {
			gameIsPaused = true;

			Time.timeScale = 0f;

			pauseScreen.SetActive (true);
			player.canMove = false;
			levelManager.levelMusic.volume = levelManager.levelMusic.volume / 8;
		} else if (Input.GetButtonDown ("Pause") && gameIsPaused) {
			this.ResumeGame();
		}
	}

	public void ResumeGame()
	{
		pauseScreen.SetActive(false);

		Time.timeScale = 1f;
		player.canMove = true;

		levelManager.levelMusic.volume = levelManager.levelMusic.volume * 8;

		gameIsPaused = false;
	}

	public void LevelSelect()
	{
		PlayerPrefs.SetInt("PlayerLives", levelManager.currentLives);
		PlayerPrefs.SetInt("CoinCount", levelManager.coinCount);

		SceneManager.LoadScene(levelSelect);
	}

	public void QuitToMainMenu()
	{
		SceneManager.LoadScene(mainMenu);
	}
}
