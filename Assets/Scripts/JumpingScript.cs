using UnityEngine;
using System.Collections;

public class JumpingScript : MonoBehaviour
{
	
	// Use this for initialization
	private bool grounded;
	public Transform groundChecker;
	public Transform jumpMeter;
	public Transform bitch;
	private float checkerRadius = 0.2f;
	[HideInInspector] public float jumpSpeed;
	[HideInInspector] public float startJumpSpeed;
	public float maxJumpSpeed;
	private bool doubleJumped = false;
	private bool jumpThroughPlayer;
	private float timer;
	private CharacterMovementScript charMove;
	private int bitMask = 1 << 9 | 1 << 10;
	private Animator anim;

	void Start ()
	{

		charMove = GetComponent<CharacterMovementScript> ();
		startJumpSpeed = (maxJumpSpeed / 1.5f);
		anim = bitch.GetComponent<Animator> ();
		jumpSpeed = startJumpSpeed;
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

		//If character is sprinting and has a velocity above 1.0
		if (charMove.Sprinting () && Mathf.Abs (rigidbody2D.velocity.x) > 1.0f) {

			// Double jump once in mid-air if jump-button is pressed
			if (Input.GetButtonDown ("Jump")) {

				if (!doubleJumped && !grounded) {


						// Set vertical velocity to zero before double jump
						Vector3 vel = rigidbody2D.velocity;
						vel.y = 0;
						rigidbody2D.velocity = vel;

						rigidbody2D.AddForce (new Vector2 (0, startJumpSpeed));
						gameObject.layer = LayerMask.NameToLayer ("JumpThroughPlayer");

						// Double jump disabled until player grounded
						doubleJumped = true;
					}

										
				if (grounded) {

					rigidbody2D.AddForce (new Vector2 (0, jumpSpeed));
					anim.SetBool ("Ground", false);

					gameObject.layer = LayerMask.NameToLayer ("JumpThroughPlayer");
				}
			}
		}

		if (Input.GetButton ("Run")) {

				if (jumpSpeed <= maxJumpSpeed) {
					jumpSpeed += (int)(startJumpSpeed * Time.deltaTime * 0.2f);
				}
		}
				
		
		
		//If character is not currently sprinting - different conditions for jumping (no charge jump)
		else if (!charMove.Sprinting ()) {

			if (Input.GetButtonDown ("Jump")) {

				if (!doubleJumped && !grounded) {

					// Set vertical velocity to zero before double jump
					Vector3 vel = rigidbody2D.velocity;
					vel.y = 0;
					rigidbody2D.velocity = vel;

					gameObject.layer = LayerMask.NameToLayer ("JumpThroughPlayer");

					rigidbody2D.AddForce (new Vector2 (0, startJumpSpeed));
					doubleJumped = true;
				}

				if (grounded) {
					
					rigidbody2D.AddForce (new Vector2 (0, startJumpSpeed));
					anim.SetBool ("Ground", false);
					gameObject.layer = LayerMask.NameToLayer ("JumpThroughPlayer");
				}
			}
		}

		//Reset jump speed if sprint button released, turns around or 
		if (Input.GetButtonUp ("Run") || charMove.flipped || Mathf.Abs(rigidbody2D.velocity.x) <= 7f) {
			
			jumpSpeed = startJumpSpeed;
			charMove.flipped = false;
		}

		Debug.Log (jumpSpeed);
	}
	
	public bool Grounded ()
	{

		return grounded;
	}
}

