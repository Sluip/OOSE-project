using UnityEngine;
using System.Collections;

public class JumpMeterScript : MonoBehaviour
{

		private JumpingScript jumpingScript;
		private GameObject player;
		private float gaugePower = 0f;
		public bool increasing = false;
		private float lastJumpSpeed;

		// Use this for initialization
		void Start ()
		{

				player = GameObject.FindGameObjectWithTag ("Player");
				jumpingScript = player.GetComponent<JumpingScript> ();
		}
	
		// Update is called once per frame
		void Update ()
		{

//						if (Input.GetButton ("Run")) {
//								increasing = true;
//						}

						if (jumpingScript.jumpSpeed <= jumpingScript.startJumpSpeed) {

								// Set jump meter scale on z-axis and gaugePower to 0
								Vector3 temp = gameObject.transform.localScale;
								temp.z = 0f;
								gameObject.transform.localScale = temp;

								gaugePower = 0f;

								lastJumpSpeed = jumpingScript.startJumpSpeed;
						}


						if (jumpingScript.jumpSpeed > lastJumpSpeed) {

								// Increase gaugePower over the duration of 1 second
								gaugePower += Time.deltaTime * 0.05f;

								// Set jump meter scale on z-axis equal to gaugePower negative
								Vector3 temp2 = gameObject.transform.localScale;
								temp2.z = -gaugePower;
								gameObject.transform.localScale = temp2;

								lastJumpSpeed = jumpingScript.jumpSpeed;
						}
				}
		}

