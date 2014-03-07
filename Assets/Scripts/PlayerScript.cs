using UnityEngine;
using System.Collections;

public class PlayerScript : MonoBehaviour {

	public Vector2 speed = new Vector2(50,50);
	private Vector2 movement;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

		float inputX = Input.GetAxis("Horizontal");
		float inputY = Input.GetAxis("Vertical");

		movement = new Vector2 (speed.x * inputX, speed.y * inputY);

		bool shoot = Input.GetButtonDown("Fire1");
		shoot |= Input.GetButtonDown("Fire2");

		if (shoot) {

			WeaponScript weapon = GetComponent<WeaponScript>();
			if(weapon != null){

				weapon.Attack(false);
			}
		}
	
	}

	void FixedUpdate(){

		rigidbody2D.velocity = movement;
	}
}
