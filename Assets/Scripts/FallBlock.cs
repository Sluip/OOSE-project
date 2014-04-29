using UnityEngine;
using System.Collections;

//We use this class to control falling blocks
public class FallBlock : MonoBehaviour {

	private float fallTimer;

	private bool fall;

	void Start () {
	//Starts in unfallen state
	fall = false;
	fallTimer = 0.15f;
	}
	
	// Update is called once per frame
	void Update () {
	//Countdown using fallTimer to ensure the platform does not fall down right away
		if (fall && fallTimer > 0) 
		{
			fallTimer -= Time.deltaTime;
		}
		if(fallTimer <= 0) 
		{
			transform.rigidbody2D.isKinematic = false;
			Destroy(gameObject,6);
		}
	}
	//When the player steps on a platform, make it fall down by setting fall to true
	void OnTriggerEnter2D (Collider2D o) {
		if (o.gameObject.tag == "Player") {
			fall = true;
		}	
	}
}
