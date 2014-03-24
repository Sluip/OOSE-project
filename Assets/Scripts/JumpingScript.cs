using UnityEngine;
using System.Collections;

public class JumpingScript : MonoBehaviour
{


		// Use this for initialization
		private bool grounded;
		public Transform groundChecker;
		public Transform jumpMeter;
		public Transform bitch;
		float checkerRadius = 0.2f;
		public float jumpSpeed = 20.0f;
		private bool isJumping = false;
		private float startJumpSpeed;
		public float maxJumpSpeed;
		public float jumpSpeedMultiplier = 400.0f;
		private bool doubleJumped = false;
		private bool jumpThroughPlayer;
		private float timer;
		private CharacterMovementScript charMove;
		private int bitMask = 1 << 9 | 1 << 10;
		Animator anim;

		void Start ()
		{
				charMove = GetComponent<CharacterMovementScript> ();
				startJumpSpeed = (maxJumpSpeed / 1.5f);
				anim = bitch.GetComponent<Animator> ();
		}

		// Update is called once per frame
		void Update ()
		{

				
					grounded = Physics2D.OverlapCircle (groundChecker.position, checkerRadius, bitMask);
				
				anim.SetBool ("Ground", grounded);
				anim.SetFloat ("vSpeed", rigidbody2D.velocity.y);
	
				if (grounded) {
						doubleJumped = false;
						
				}

				if (rigidbody2D.velocity.y < 0.0f) {
						gameObject.layer = LayerMask.NameToLayer ("Player");
				}
				//If character is sprinting and has a velocity above 3.0
				if (charMove.Sprinting () && Mathf.Abs (rigidbody2D.velocity.x) > 1.0f) {

						// Jump if grounded and jump-button is released
						if (grounded && isJumping) {
								rigidbody2D.AddForce (new Vector2 (0, jumpSpeed));
								isJumping = false;
								anim.SetBool ("Ground", false);
						}

						// Double jump once in mid-air if jump-button is pressed
						if (!doubleJumped && Input.GetButtonDown ("Jump") && !grounded) {
								// Set vertical velocity to zero before double jump
								Vector3 vel = rigidbody2D.velocity;
								vel.y = 0;
								rigidbody2D.velocity = vel;

								rigidbody2D.AddForce (new Vector2 (0, startJumpSpeed));

								// Double jump disabled until player grounded
								if (!grounded && !doubleJumped) {
										doubleJumped = true;
								}
						}

						// Jump when space is released
						if (!doubleJumped && Input.GetButtonUp ("Jump")) {
								if(grounded)
								{
									isJumping = true;
									gameObject.layer = LayerMask.NameToLayer ("JumpThroughPlayer");
								}
								

								if (jumpSpeed > maxJumpSpeed) {
										jumpSpeed = maxJumpSpeed;
								}
						}

						// Jump speed is set to start value when space is pressed down
						if (Input.GetButtonDown ("Jump")) {
								jumpSpeed = startJumpSpeed;
						}

						if (grounded && Input.GetButton ("Jump")) {
								if (jumpSpeed <= maxJumpSpeed)
										jumpSpeed += (int)(startJumpSpeed * Time.deltaTime);
						}
				} 
					//If character is not currently sprinting - different conditions for jumping (no charge jump)
					else if (!charMove.Sprinting ()) {
						if (Input.GetButtonDown ("Jump") && grounded) {
								isJumping = true;
								gameObject.layer = LayerMask.NameToLayer ("JumpThroughPlayer");
						}
						if (isJumping && grounded) {
								rigidbody2D.AddForce (new Vector2 (0, startJumpSpeed));
								isJumping = false;
								anim.SetBool ("Ground", false);
						}
						if (!doubleJumped && Input.GetButtonDown ("Jump") && !grounded) {
								// Set vertical velocity to zero before double jump
								Vector3 vel = rigidbody2D.velocity;
								vel.y = 0;
								rigidbody2D.velocity = vel;
								gameObject.layer = LayerMask.NameToLayer("JumpThroughPlayer");
				
								rigidbody2D.AddForce (new Vector2 (0, startJumpSpeed));
				
								// Double jump disabled until player grounded
								if (!grounded && !doubleJumped) {
										doubleJumped = true;
								}
						}
				}
		}

		public bool Grounded ()
		{
				return grounded;
		}
}

