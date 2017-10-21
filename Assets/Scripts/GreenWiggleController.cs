using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GreenWiggleController : MonoBehaviour {

	public Transform leftPoint;
	public Transform rightPoint;
	public bool movingRight;
	public float moveSpeed;

	private Rigidbody2D rigidBody;

	// Use this for initialization
	void Start () {
		rigidBody = GetComponent<Rigidbody2D>();
	}
	
	// Update is called once per frame
	void Update ()
	{
		if (movingRight && transform.position.x > rightPoint.position.x) {
			movingRight = false;
		} 

		if (!movingRight && transform.position.x < leftPoint.position.x) {
			movingRight = true;
		}

		if (movingRight) {
			rigidBody.velocity = new Vector3 (moveSpeed, rigidBody.velocity.y, 0f);
		} else {
			rigidBody.velocity = new Vector3(-moveSpeed, rigidBody.velocity.y, 0f);
		}
	}
}
