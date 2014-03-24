using UnityEngine;
using System.Collections;

public class CharacterMovementScript : MonoBehaviour
{
	public float maxMovementSpeed = 10.0f;
	bool right = true;
	public Transform bitch;
	private bool sprinting;
	private float move;
	Animator anim;
	private bool isSprinting;

	// Use this for initialization
	void Start ()
	{	
		sprinting = false;
		anim = bitch.GetComponent<Animator> ();
	}

	// Update is called once per frame
	void FixedUpdate ()
	{		

		if (!sprinting) {
			move = Input.GetAxis ("Horizontal");
			rigidbody2D.velocity = new Vector2 (move * maxMovementSpeed, rigidbody2D.velocity.y);
		} else if (sprinting) {
			move = Input.GetAxis ("Horizontal");
			rigidbody2D.velocity = new Vector2 (move * maxMovementSpeed * 1.5f, rigidbody2D.velocity.y);
		}

						
		if (move > 0 && !right) {
			Flip ();
		} else if (move < 0 && right) {
			Flip ();
		}
	}

	void Update ()
	{
		if(Mathf.Abs (rigidbody2D.velocity.x) > maxMovementSpeed)
			isSprinting = true;
		else
			isSprinting = false;

		if (Input.GetButton ("Run")) {
			sprinting = true;
		}
		if (Input.GetButtonUp ("Run")) {
			sprinting = false;
		}
		float moveX = Input.GetAxis ("Horizontal");
			
		anim.SetFloat ("speed", Mathf.Abs (moveX));
		anim.SetBool ("sprint", isSprinting);
	}

	void Flip ()
	{
		right = !right;
		Vector3 scale = transform.localScale;
		scale.x *= -1;
		transform.localScale = scale;
	}

	public bool Sprinting ()
	{
		return sprinting;
	}

}
