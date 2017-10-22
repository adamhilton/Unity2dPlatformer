using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour {

	public bool bossActive;
	public float timeBetweenDrops;
	public float waitForPlatforms;
	public Transform leftPoint;
	public Transform rightPoint;
	public Transform dropSawSpawnPoint;
	public GameObject dropSaw;
	public GameObject boss;
	public bool bossRight;
	public GameObject rightPlatforms;
	public GameObject leftPlatforms;
	public bool takeDamage;
	public int startingHealth;
	public GameObject bridgeToEnd;
	public bool waitingForRespawn;

	private float dropCount;
	private float platformCount;
	private float currentHealth;
	private CameraController camera;
	private LevelManager levelManager;
	private float timeBetweenDropsStore;

	// Use this for initialization
	void Start () {
		dropCount = timeBetweenDrops;
		timeBetweenDropsStore = timeBetweenDrops;
		platformCount = waitForPlatforms;
		currentHealth = startingHealth;

		camera = FindObjectOfType<CameraController>();
		levelManager = FindObjectOfType<LevelManager>();

		boss.transform.position = rightPoint.position;
		bossRight = true;
	}
	
	// Update is called once per frame
	void Update ()
	{

		if (levelManager.respawnCoActive) {
			bossActive = false;
			waitingForRespawn = true;
		}

		if (waitingForRespawn && !levelManager.respawnCoActive) {
			boss.SetActive(false);
			leftPlatforms.SetActive(false);
			rightPlatforms.SetActive(false);

			timeBetweenDrops = timeBetweenDropsStore;
			platformCount = waitForPlatforms;
			dropCount = timeBetweenDrops;

			boss.transform.position = rightPoint.position;
			bossRight = true;

			currentHealth = startingHealth;

			camera.followTarget = true;

			waitingForRespawn = false;
		}

		if (bossActive) {

			camera.followTarget = false;
			camera.transform.position = Vector3.Lerp(camera.transform.position, new Vector3(transform.position.x, camera.transform.position.y, camera.transform.position.z), camera.smoothing * Time.deltaTime);

			boss.SetActive (true);

			if (dropCount > 0) {
				dropCount -= Time.deltaTime;
			} else {
				dropSawSpawnPoint.position = new Vector3 (Random.Range (leftPoint.position.x, rightPoint.position.x), dropSawSpawnPoint.position.y, 0f);
				Instantiate (dropSaw, dropSawSpawnPoint.position, dropSawSpawnPoint.rotation);
				dropCount = timeBetweenDrops;
			}

			if (bossRight) {
				if (platformCount > 0) {
					platformCount -= Time.deltaTime;
				} else {
					rightPlatforms.SetActive (true);
				}
			} else {
				if (platformCount > 0) {
					platformCount -= Time.deltaTime;
				} else {
					leftPlatforms.SetActive (true);
				}
			}

			if (takeDamage) {
				currentHealth -= 1;

				if (currentHealth <= 0) {
					camera.followTarget = true;
					bridgeToEnd.SetActive(true);
					gameObject.SetActive(false);
				}

				if (bossRight) {
					boss.transform.position = leftPoint.position;
				} else {
					boss.transform.position = rightPoint.position;
				}

				bossRight = !bossRight;

				rightPlatforms.SetActive(false);
				leftPlatforms.SetActive(false);

				platformCount = waitForPlatforms;

				timeBetweenDrops = timeBetweenDrops / 2f;
		
				takeDamage = false;
			}
		}
	}

	void OnTriggerEnter2D (Collider2D other)
	{
		if (other.CompareTag ("Player")) {
			bossActive = true;
		}
	}
}
