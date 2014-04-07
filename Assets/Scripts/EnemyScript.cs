using UnityEngine;
using System.Collections;

public class EnemyScript : MonoBehaviour
{
	
	public int HP;
	public Transform healthBar;
	public Transform player;
	public float meleeDistance;
	public float minDistance;
	public float maxMovementSpeed;
	public float shootingDistance;
	public GameObject bullet;
	public float move;
	private bool right;
	private float damageCooldown;
	private float shootingCooldown;
	public float hitRate;
	private bool canAttack = false;
	private HealthScript healthScript;
	public int damage;
	public int shootingDamage;
	public float bulletSpeed;
	public Transform bulletTarget;
		
	
	// Use this for initialization
	void Start ()
	{	
		move = 3.5f;
		right = false;
		damageCooldown = 0f;
		healthScript = player.GetComponent<HealthScript> ();
		healthBar.transform.renderer.material.color = Color.red;
	}

	void FixedUpdate ()
	{
		if(player != null){

			float distance = Vector2.Distance (transform.position, player.position);

			if (distance <= shootingDistance && distance >= meleeDistance){
				if (shootingCooldown <= 0){
					Shoot();
				}
			}
			else if (distance <= meleeDistance && distance >= minDistance) {
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
		
				if (distance >= meleeDistance || distance <= minDistance) {
					rigidbody2D.velocity = new Vector2 (0f, rigidbody2D.velocity.y);
			
				}
			}
		}
	
		// Update is called once per frame
		void Update ()
		{

				// Death
				if (HP <= 0) {
						Destroy (gameObject);
				}

				if (damageCooldown > 0) {
					damageCooldown -= Time.deltaTime;
				}
				if (shootingCooldown > 0) {
					shootingCooldown -= Time.deltaTime;
				}

				if(canAttack) {
					canAttack = false;
					damageCooldown = hitRate;
					healthScript.Hit (damage);
				}
		}

		public void Hurt (int damage)
		{
				HP -= damage;

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

		void OnTriggerStay2D(Collider2D other)
		{
			if(other.gameObject.tag ==  "PlayerHitbox")
			{
				if(damageCooldown <= 0f)
					canAttack = true;
			}
			

			
	}
		void Shoot()
		{;
			GameObject Bullet;
			shootingCooldown = hitRate; ;
			Quaternion bulletDirection = Quaternion.LookRotation(bulletTarget.position - transform.position);
		    //Debug.Log (bulletDirection);
		    
		    Bullet = Instantiate(bullet, transform.position, Quaternion.identity) as GameObject;
		    bulletDirection.x = 0;
		    bulletDirection.y = 0;
		    Debug.Log (bulletDirection.z);
		    Vector2 forceDirection = bulletTarget.position - transform.position;
		    Bullet.transform.rotation = Quaternion.Slerp (transform.rotation,bulletDirection, 1f);
		    //Bullet.rigidbody2D.AddForce(forceDirection*10);
			Bullet.rigidbody2D.AddForce(forceDirection * Time.deltaTime * bulletSpeed);
		    if (right){
		    Vector3 scale = Bullet.transform.localScale;
		    scale *= -1;
		    Bullet.transform.localScale = scale;
		    }
		    
			
		}

	public bool Right()
	{
		return right;
	}
}