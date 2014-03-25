using UnityEngine;
using System.Collections;

public class EnemyScript : MonoBehaviour {
		
	public int HP = 10;
	public Transform healthBar;
	public Transform player;
	public float maxDistance = 10f;
	public float minDistance = 1.5f;
	public float maxMovementSpeed = 10f;
	public float move;
	bool right = false;
	
	// Use this for initialization
	void Start () {

		move = 5f;

	}
	
	// Update is called once per frame
	void Update () {

		// Death
		if(HP <= 0)
		{
			Destroy(gameObject);
		}

		// Change color of health bar to yellow
		if(HP <= 5)
		{
			healthBar.transform.renderer.material.color = Color.yellow;
		}

		// Change color of health bar to red
		if(HP < 3)
		{
			healthBar.transform.renderer.material.color = Color.red;
		}
	

		//------------------AI------------------//

		float distance = Vector2.Distance (transform.position, player.position);

		if(distance <= maxDistance && distance >= minDistance) {

			float step = move * Time.deltaTime;
			float tempY = transform.position.y;

			transform.position = Vector2.MoveTowards(transform.position, player.position, step);

			if (transform.position.y != tempY)
				transform.position = new Vector2(transform.position.x, tempY);
		}
		else

			Debug.Log ("Out of Range");

		//--------------------------------------//


	}

	public void Hurt(int damage)
	{
		HP -= damage;
		Debug.Log (HP);

		// Scale health bar down
		Vector3 temp = healthBar.localScale;
		temp.z += 0.01f;
		healthBar.localScale = temp;
	}

	void Flip ()
	{
		right = !right;
		Vector3 scale = transform.localScale;
		scale.x *= -1;
		transform.localScale = scale;
	}
}