using UnityEngine;
using System.Collections;

public class JumpMeterScript : MonoBehaviour {

	private JumpingScript jumpingScript;
	private GameObject player;

	// Use this for initialization
	void Start () {

		player = GameObject.FindGameObjectWithTag("Player");

		jumpingScript = player.GetComponent<JumpingScript> ();
	}
	
	// Update is called once per frame
	void Update () {
		
		if(jumpingScript.grounded && Input.GetButtonDown ("Jump"))
		{
			Vector3 temp = gameObject.transform.localScale;
			temp.z -= 0.1f;
			gameObject.transform.localScale = temp;
		}

		if(Input.GetButtonUp ("Jump"))
		{
			Vector3 temp = gameObject.transform.localScale;
			temp.z = 0f;
			gameObject.transform.localScale = temp;
		}
	}
}
