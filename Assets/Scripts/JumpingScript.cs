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
	public float jumpSpeedMultiplier = 7.0f;

	void Start () {
	
	}

	void Update()
	{
		// Jump when space is released
		if(Input.GetButtonUp ("Jump"))
		{
			isJumping = true;
			isCharging = false;

			if(jumpSpeed > maxJumpSpeed)
			{
				jumpSpeed = maxJumpSpeed;
			}
		}

		// Jump is charging while space is pressed
		if(Input.GetButton ("Jump"))
		{
			isCharging = true;
		}

		// Jump speed is set to default value when space is pressed down
		if(Input.GetButtonDown ("Jump"))
		{
			jumpSpeed = startJumpSpeed;
		}
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		grounded = Physics2D.OverlapCircle(groundChecker.position, checkerRadius, consideredGround);
		if (grounded && isJumping) {
			rigidbody2D.AddForce(new Vector2(0, jumpSpeed));
			isJumping = false;
		}
		if(grounded && isCharging)
		{
			jumpSpeed += jumpSpeedMultiplier;
		}
	}
	public bool Grounded() {
		return grounded;
		}
}

