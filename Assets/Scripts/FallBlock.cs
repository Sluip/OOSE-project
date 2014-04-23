using UnityEngine;
using System.Collections;

public class FallBlock : MonoBehaviour {



	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	void OnTriggerEnter2D (Collider2D o) {
		if (o.gameObject.tag == "Player") {
			rigidbody2D.isKinematic = false;
		}	
	}
}
