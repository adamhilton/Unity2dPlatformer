using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelEnd : MonoBehaviour {

	public string levelToLoad;
	public float waitToMove;
	public float waitToLoad;
	public Sprite flagOpen;

	private PlayerController player;
	private CameraController camera;
	private LevelManager levelManager;
	private bool movePlayer;
	private SpriteRenderer spriteRenderer;

	// Use this for initialization
	void Start () {
		player = FindObjectOfType<PlayerController>();
		camera = FindObjectOfType<CameraController>();
		levelManager = FindObjectOfType<LevelManager>();

		spriteRenderer = GetComponent<SpriteRenderer>();
	}
	
	// Update is called once per frame
	void Update ()
	{
		if (movePlayer) {
			player.rigidBody.velocity = new Vector3(player.moveSpeed, player.rigidBody.velocity.y, 0f);
		}
	}

	void OnTriggerEnter2D (Collider2D other)
	{
		if (other.CompareTag ("Player")) {
			//SceneManager.LoadScene(levelToLoad);

			StartCoroutine("LevelEndCo");

			spriteRenderer.sprite = flagOpen;
		}
	}

	public IEnumerator LevelEndCo ()
	{
		player.canMove = false;
		camera.followTarget = false;
		levelManager.invincible = true;

		levelManager.levelMusic.Stop();
		levelManager.gameOverMusic.Play();

		player.rigidBody.velocity = Vector3.zero;

		yield return new WaitForSeconds(waitToMove);

		movePlayer = true;

		yield return new WaitForSeconds(waitToLoad);

		SceneManager.LoadScene(levelToLoad);
	}
}
