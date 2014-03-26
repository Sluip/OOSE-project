using UnityEngine;
using System.Collections;

public class EnemyScript : MonoBehaviour
{
	
		public int HP = 10;
		public Transform healthBar;
		public Transform player;
		public float maxDistance;
		public float minDistance;
		public float maxMovementSpeed;
		public float move;
		bool right;
	
		// Use this for initialization
		void Start ()
		{

	
				move = 5f;
				right = false;

		}
	
		// Update is called once per frame
		void Update ()
		{

				// Death
				if (HP <= 0) {
						Destroy (gameObject);
				}

				// Change color of health bar to yellow
				if (HP <= 5) {
						healthBar.transform.renderer.material.color = Color.yellow;
				}

				// Change color of health bar to red
				if (HP < 3) {
						healthBar.transform.renderer.material.color = Color.red;
				}
	

				//------------------AI------------------//

				float distance = Vector2.Distance (transform.position, player.position);

				if (distance <= maxDistance && distance >= minDistance) {
						//Making a relativity vector between player and enemy
						Vector2 direction = player.position - transform.position;
						//Making the enemy able to face the player depending on where the player is
						if (direction.x < 0 && right) 
								Flip ();
						else if (direction.x > 0 && !right)
								Flip ();

						if (direction.x < 0) 
								rigidbody2D.velocity = new Vector2 (move * -1, rigidbody2D.velocity.y);
						else if (direction.x > 0)
								rigidbody2D.velocity = new Vector2 (move, rigidbody2D.velocity.y);
				}


				if (distance >= maxDistance || distance <= minDistance) {
						rigidbody2D.velocity = new Vector2 (0f, rigidbody2D.velocity.y);

				}

				///--------------------------------------//
		}

		public void Hurt (int damage)
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