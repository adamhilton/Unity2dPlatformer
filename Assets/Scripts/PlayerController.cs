using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

	public float moveSpeed;
	public float jumpSpeed;
	public Transform groundCheck;
	public float groundCheckRadius;
	public LayerMask whatIsGround;
	public bool isGrounded;
	public Vector3 respawnPosition;
	public LevelManager levelManager;
	public GameObject stompBox;
	public float knockbackForce;
	public float knockbackLength;
	public float invincibilityLength;
	public AudioSource jumpSound;
	public AudioSource hurtSound;

	private Rigidbody2D rigidBody;
	private Animator animator;
	private float knockbackCounter;
	private float invincibilityCounter;

	// Use this for initialization
	void Start ()
	{
		this.rigidBody = GetComponent<Rigidbody2D> ();
		this.animator = GetComponent<Animator> ();

		respawnPosition = transform.position;
		levelManager = FindObjectOfType<LevelManager>();
	}
	
	// Update is called once per frame
	void Update ()
	{
		isGrounded = Physics2D.OverlapCircle (groundCheck.position, groundCheckRadius, whatIsGround);

		if (knockbackCounter <= 0) {

			if (Input.GetAxisRaw ("Horizontal") > 0f) {
				rigidBody.velocity = new Vector3 (moveSpeed, rigidBody.velocity.y, 0f);
				transform.localScale = new Vector3 (1f, 1f, 1f);
			} else if (Input.GetAxisRaw ("Horizontal") < 0f) {
				rigidBody.velocity = new Vector3 (-moveSpeed, rigidBody.velocity.y, 0f);
				transform.localScale = new Vector3 (-1f, 1f, 1f);
			} else {
				rigidBody.velocity = new Vector3 (0f, rigidBody.velocity.y, 0f);
			}

			if (Input.GetButtonDown ("Jump") && isGrounded) {
				rigidBody.velocity = new Vector3 (rigidBody.velocity.x, jumpSpeed, 0f);
				jumpSound.Play();
			}
		}

		if (knockbackCounter > 0) {
			knockbackCounter -= Time.deltaTime;
			if (transform.localScale.x > 0) {
				rigidBody.velocity = new Vector3 (-knockbackForce, knockbackForce, 0f);
			} else {
				rigidBody.velocity = new Vector3 (knockbackForce, knockbackForce, 0f);
			}
		}

		if (invincibilityCounter > 0) {
			invincibilityCounter -= Time.deltaTime;
		}

		if (invincibilityCounter <= 0) {
			levelManager.invincible = false;
		}

		animator.SetFloat ("Speed", Mathf.Abs (rigidBody.velocity.x));
		animator.SetBool ("Grounded", isGrounded);

		if (rigidBody.velocity.y < 0) {
			stompBox.SetActive (true);
		} else {
			stompBox.SetActive (false);
		}
	}

	public void Knockback()
	{
		knockbackCounter = knockbackLength;
		invincibilityCounter = invincibilityLength;
		levelManager.invincible = true;
	}

	void OnTriggerEnter2D (Collider2D other)
	{
		if (other.CompareTag("KillPlane")) {
			levelManager.Respawn();
		}

		if (other.CompareTag("Checkpoint")) {
			respawnPosition = other.transform.position;
		}
	}

	void OnCollisionEnter2D (Collision2D other)
	{
		if (other.gameObject.CompareTag("MovingPlatform")) {
			transform.parent = other.transform;
		}
	}

	void OnCollisionExit2D (Collision2D other)
	{
		if (other.gameObject.CompareTag("MovingPlatform")) {
			transform.parent = null;
		}
	}
}
