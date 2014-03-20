using UnityEngine;
using System.Collections;

public class CharacterMovementScript : MonoBehaviour
{
		private JumpingScript jumpScript;
		public float maxMovementSpeed = 10.0f;
		bool right = true;
		// Use this for initialization
		void Start ()
		{
			jumpScript = GetComponent<JumpingScript> ();
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

			
		

		

		void Flip ()
		{
				right = !right;
				Vector3 scale = transform.localScale;
				scale.x *= -1;
				transform.localScale = scale;
		}

}
