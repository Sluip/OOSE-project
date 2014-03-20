using UnityEngine;
using System.Collections;

public class JumpMeterScript : MonoBehaviour
{

		private JumpingScript jumpingScript;
		private CharacterMovementScript charMove;
		private GameObject player;
		private float gaugePower = 0f;
		private float fullWidth = 0.1f;
		public bool increasing = false;

		// Use this for initialization
		void Start ()
		{

				player = GameObject.FindGameObjectWithTag ("Player");

				jumpingScript = player.GetComponent<JumpingScript> ();
				charMove = player.GetComponent<CharacterMovementScript> ();
		}
	
		// Update is called once per frame
		void Update ()
		{
				if (charMove.Sprinting () && Mathf.Abs (player.rigidbody2D.velocity.x) > 1.0f) {
						if (jumpingScript.Grounded () && Input.GetButton ("Jump")) {
								increasing = true;
						}

						if (Input.GetButtonUp ("Jump")) {
								// Set jump meter scale on z-axis and gaugePower to 0
								Vector3 temp = gameObject.transform.localScale;
								temp.z = 0f;
								gameObject.transform.localScale = temp;

								gaugePower = 0f;
								increasing = false;
						}

						if (increasing) {
								// Increase gaugePower to 0.1 over the duration of 1 second
								gaugePower += Time.deltaTime * 0.1f;
								gaugePower = Mathf.Clamp (gaugePower, 0, fullWidth);

								// Set jump meter scale on z-axis equal to gaugePower negative
								Vector3 temp2 = gameObject.transform.localScale;
								temp2.z = -gaugePower;
								gameObject.transform.localScale = temp2;

						}
				}
				if (!charMove.Sprinting () || Mathf.Abs (player.rigidbody2D.velocity.x) < 1.0f) {
						Vector3 temp = gameObject.transform.localScale;
						temp.z = 0f;
						gameObject.transform.localScale = temp;
						gaugePower = 0f;
						increasing = false;
				}
				
		}
}
