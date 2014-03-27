using UnityEngine;
using System.Collections;

public class JumpingScript : MonoBehaviour
{

	// Use this for initialization
	private bool grounded;
	public Transform groundChecker;
	public Transform jumpMeter;
	public Transform player;
	private float checkerRadius = 0.2f;
	[HideInInspector] 
	public float jumpSpeed;
	[HideInInspector] 
	public float startJumpSpeed;
	public float maxJumpSpeed;
	private bool doubleJumped = false;
	private bool jumpThroughPlayer;
	private float timer;
	private MovementScript charMove;
	private int bitMask = 1 << 9 | 1 << 10;
	private Animator anim;
	private bool sprintJumped;
	private bool jumped;
	private bool running;
	private Vector2 rectangleSize;
	
	void Start ()
	{
		
		charMove = GetComponent<MovementScript> ();
		startJumpSpeed = (maxJumpSpeed / 1.5f);
		anim = player.GetComponent<Animator> ();
		jumpSpeed = startJumpSpeed;
	}

	void FixedUpdate()
	{
				//grounded = Physics2D.OverlapCircle (groundChecker.position, checkerRadius, bitMask);
				grounded = Physics2D.OverlapArea (groundChecker.position, rectangleSize, bitMask);

				if (grounded) {

					doubleJumped = false;
				}

				anim.SetBool ("Ground", grounded);
				anim.SetFloat ("vSpeed", rigidbody2D.velocity.y);

				if (rigidbody2D.velocity.y < 0.0f) {

					gameObject.layer = LayerMask.NameToLayer ("Player");
				}

				if (sprintJumped) {

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

				gameObject.layer = LayerMask.NameToLayer ("JumpThroughPlayer");
			}

			sprintJumped = false;
		}

		if (running){

			if (jumpSpeed <= maxJumpSpeed) {
				jumpSpeed += (int)(startJumpSpeed * Time.deltaTime * 0.2f);
			}

			running = false;
		}

		if (jumped) {

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
				gameObject.layer = LayerMask.NameToLayer ("JumpThroughPlayer");
			}

			jumped = false;
		}
	}
	
	// Update is called once per frame
	void Update ()
	{
		//Sprinting
		if (charMove.Sprinting ()) {
			
			if (Input.GetButtonDown ("Jump")) {
				
				sprintJumped = true;
			}
		}
		
		if (Input.GetButton ("Run")) {
			
			running = true;
		}
		if (charMove.Right ()) {
			rectangleSize.x = groundChecker.position.x + 2.0f;
			rectangleSize.y = groundChecker.position.y + 0.2f;
		} 
		else if (!charMove.Right ()) {
			rectangleSize.x = groundChecker.position.x - 2.0f;
			rectangleSize.y = groundChecker.position.y + 0.2f;
		}
		//Not sprinting
		else if (!charMove.Sprinting ()) {
			
			if (Input.GetButtonDown ("Jump")) {
				
				jumped = true;
			}
		}
		
		//Reset jump speed if sprint button released, turns around or has a velocity.x less or equal to 7
		if (Input.GetButtonUp ("Run") || charMove.flipped || Mathf.Abs(rigidbody2D.velocity.x) <= 7f) {
			
			jumpSpeed = startJumpSpeed;
			charMove.flipped = false;
		}
	}
	
	public bool Grounded ()
	{

		return grounded;
	}
}
