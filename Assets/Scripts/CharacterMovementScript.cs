using UnityEngine;
using System.Collections;

public class CharacterMovementScript : MonoBehaviour
{
		public float maxMovementSpeed = 10.0f;
		bool right = true;
		public Transform bitch;
		Animator anim;

		// Use this for initialization
		void Start ()
		{
				anim = bitch.GetComponent<Animator> ();
		}

		// Update is called once per frame
		void FixedUpdate ()
		{
				float move = Input.GetAxis ("Horizontal");
				rigidbody2D.velocity = new Vector2 (move * maxMovementSpeed, rigidbody2D.velocity.y);
						
				if (move > 0 && !right) {
						Flip ();
				} else if (move < 0 && right) {
						Flip ();
				}
		}

		void Update ()
		{
				float moveX = Input.GetAxis ("Horizontal");
			
				anim.SetFloat ("speed", Mathf.Abs (moveX));
		}

		void Flip ()
		{
				right = !right;
				Vector3 scale = transform.localScale;
				scale.x *= -1;
				transform.localScale = scale;
		}

}
