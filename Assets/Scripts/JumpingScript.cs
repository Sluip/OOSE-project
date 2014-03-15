using UnityEngine;
using System.Collections;

public class JumpingScript : MonoBehaviour {

	// Use this for initialization
	bool grounded;
	public Transform groundChecker;
	public LayerMask consideredGround;
	float checkerRadius = 0.2f;
	[HideInInspector] public float jumpSpeed = 20.0f;
	private bool isJumping = false;
	private bool isCharging = false;
	public float startJumpSpeed = 500.0f;
	public float maxJumpSpeed = 1000.0f;
	public float jumpSpeedMultiplier = 10.0f;
	private bool doubleJumped = false;

	void Start () {

	}
	
	void FixedUpdate () 
	{
		grounded = Physics2D.OverlapCircle(groundChecker.position, checkerRadius, consideredGround);

		if (grounded)
			doubleJumped = false;

		if(grounded && isCharging)
			jumpSpeed += jumpSpeedMultiplier;
	}
	
	public bool Grounded() 
	{
		return grounded;
	}

	// Update is called once per frame
	void Update()
	{
		// Jump if grounded and jump-button is released
		if (grounded && isJumping) 
		{
			rigidbody2D.AddForce(new Vector2(0, jumpSpeed));
			isJumping = false;
		}

		// Double jump once in mid-air if jump-buton is pressed
		if (!doubleJumped && Input.GetButtonDown ("Jump") && !grounded)
		{
			// Set vertical velocity to zero before double jump
			Vector3 vel = rigidbody2D.velocity;
			vel.y = 0;
			rigidbody2D.velocity = vel;

			rigidbody2D.AddForce (new Vector2 (0, startJumpSpeed));

			// Double jump disabled until player grounded
			if(!grounded && !doubleJumped)
			{
				doubleJumped = true;
			}
		}

		// Jump when space is released
		if(!doubleJumped && Input.GetButtonUp ("Jump"))
		{
			isJumping = true;
			isCharging = false;

			if(jumpSpeed > maxJumpSpeed)
			{
				jumpSpeed = maxJumpSpeed;
			}
		}

		// Jump charging while space is pressed
		if(Input.GetButton ("Jump"))
		{
			isCharging = true;
		}

		// Jump speed is set to start value when space is pressed down
		if(Input.GetButtonDown ("Jump"))
		{
			jumpSpeed = startJumpSpeed;
		}
	}
}

