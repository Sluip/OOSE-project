using UnityEngine;
using System.Collections;

public class JumpingScript : MonoBehaviour {

	// Use this for initialization
	[HideInInspector] public bool grounded;
	public Transform groundChecker;
	public Transform jumpMeter;
	public LayerMask consideredGround;
	float checkerRadius = 0.2f;
	[HideInInspector] public float jumpSpeed = 20.0f;
	private bool isJumping = false;
	public float startJumpSpeed;
	public float maxJumpSpeed = 800.0f;
	public float jumpSpeedMultiplier = 400.0f;
	private bool doubleJumped = false;
	private float timer;

	void Start () {

		startJumpSpeed = (maxJumpSpeed / 2);
	}
	
	void FixedUpdate () 
	{
		grounded = Physics2D.OverlapCircle(groundChecker.position, checkerRadius, consideredGround);

		if (grounded)
			doubleJumped = false;
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

		// Double jump once in mid-air if jump-button is pressed
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

			if(jumpSpeed > maxJumpSpeed)
			{
				jumpSpeed = maxJumpSpeed;
			}
		}

		// Jump speed is set to start value when space is pressed down
		if(Input.GetButtonDown ("Jump"))
		{
			jumpSpeed = startJumpSpeed;
		}

		if (grounded && Input.GetButton ("Jump"))
		{
			if(jumpSpeed <= maxJumpSpeed)
			jumpSpeed += (int)(startJumpSpeed * Time.deltaTime);
		}
	}
}

