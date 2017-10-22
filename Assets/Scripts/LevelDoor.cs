using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelDoor : MonoBehaviour {

	public string levelToLoad;
	public bool unlocked;
	public Sprite doorBottomOpen;
	public Sprite doorTopOpen;
	public Sprite doorBottomClosed;
	public Sprite doorTopClosed;

	public SpriteRenderer doorBottomRenderer;
	public SpriteRenderer doorTopRenderer;

	// Use this for initialization
	void Start ()
	{
		PlayerPrefs.SetInt("Level1", 1);

		unlocked = PlayerPrefs.GetInt (levelToLoad).Equals(1);
			

		if (unlocked) {
			doorTopRenderer.sprite = doorTopOpen;
			doorBottomRenderer.sprite = doorBottomOpen;
		} else {
			doorTopRenderer.sprite = doorTopClosed;
			doorBottomRenderer.sprite = doorBottomClosed;
		}
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnTriggerStay2D (Collider2D other)
	{
		if (other.CompareTag ("Player")) { 
			if (Input.GetButtonDown ("Jump") && unlocked) {
				SceneManager.LoadScene(levelToLoad);
			}
		}
	}
}
