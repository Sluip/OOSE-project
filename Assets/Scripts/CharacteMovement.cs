using UnityEngine;
using System.Collections;

public class CharacteMovement : MonoBehaviour {

	// Use this for initialization
	void Start () {
		Physics.gravity = new Vector3(0,-50.0f,0);
		
	}
	//Control movement speed
	public float speed;
	//Control Jump Height	
	public float jumpSpeed;
	//Control gravity
	public float gravity;
	//Control max speed
	public float maxVelocityChange;
	bool grounded;
	private Vector3 moveDirection = Vector3.zero;
	void Update() {
			//Player control instructions to engine
			moveDirection = new Vector3(Input.GetAxis("Horizontal"), moveDirection.y, 0);
			moveDirection = transform.TransformDirection(moveDirection);
			moveDirection *= speed;
			Vector3 v = rigidbody.velocity;
			Vector3 change = (moveDirection-v);
			
			change.z = Mathf.Clamp (change.z, -maxVelocityChange,  maxVelocityChange);
			change.y = 0;
			
			rigidbody.AddForce(change, ForceMode.VelocityChange);

	}
}