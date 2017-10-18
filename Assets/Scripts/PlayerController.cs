using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

	public float moveSpeed;
	public float jumpSpeed;

	private Rigidbody2D rigidBody;

	// Use this for initialization
	void Start ()
	{
		this.rigidBody = GetComponent<Rigidbody2D> ();
	}
	
	// Update is called once per frame
	void Update ()
	{
		if (Input.GetAxisRaw ("Horizontal") > 0f) {
			rigidBody.velocity = new Vector3 (moveSpeed, rigidBody.velocity.y, 0f);
		} else if (Input.GetAxisRaw ("Horizontal") < 0f) {
			rigidBody.velocity = new Vector3 (-moveSpeed, rigidBody.velocity.y, 0f);
		} else {
			rigidBody.velocity = new Vector3 (0f, rigidBody.velocity.y, 0f);
		}

		if (Input.GetButtonDown ("Jump")) {
			rigidBody.velocity = new Vector3(rigidBody.velocity.x, jumpSpeed, 0f);
		}

	}
}
