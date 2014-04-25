using UnityEngine;
using System.Collections;

public class FallBlock : MonoBehaviour {

	private float fallTimer;
	private bool fall;



	// Use this for initialization
	void Start () {
	fall = false;
	fallTimer = 0.15f;
	}
	
	// Update is called once per frame
	void Update () {
	
		if (fall && fallTimer > 0) 
			fallTimer -= Time.deltaTime;
		if(fallTimer <= 0) {
			transform.rigidbody2D.isKinematic = false;
			}
			 
	
	
	
	}
	
	void OnTriggerEnter2D (Collider2D o) {
		if (o.gameObject.tag == "Player") {
			fall = true;
		}	
	}

	
	
}
