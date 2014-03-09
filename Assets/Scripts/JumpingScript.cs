using UnityEngine;
using System.Collections;

public class JumpingScript : MonoBehaviour {

	// Use this for initialization
	bool grounded;
	public Transform groundChecker;
	public LayerMask consideredGround;
	float checkerRadius = 0.2f;
	public float jumpSpeed = 20.0f;
	void Start () {
	
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		grounded = Physics2D.OverlapCircle(groundChecker.position, checkerRadius, consideredGround);
	
	}
	void Update () {
		if (grounded && Input.GetButton ("Jump")) {
			rigidbody2D.AddForce(new Vector2(0, jumpSpeed));
		}
	}
}

