using UnityEngine;
using System.Collections;

public class EnemyScript : MonoBehaviour
{
	
	public int HP;
	public Transform healthBar;
	private Transform player;
	public float meleeDistance;
	public float minDistance;
	public float maxMovementSpeed;
	public float shootingDistance;
	public GameObject bullet;
	public float move;
	private bool right;
	private float damageCooldown;
	private float shootingCooldown;
	public float shootingRate;
	public float hitRate;
	private bool canAttack = false;
	private HealthScript healthScript;
	public int damage;
	public int shootingDamage;
	public float bulletSpeed;
	private Transform bulletTarget;
	private Animator anim;
	public Transform enemy;
	private int enemyLayer;
	public GameObject visionStart, visionEnd, visionHigh, visionLow;
	private int playerLayer;
	private bool aiming;
	
	
	// Use this for initialization
	void Start()
	{
		player = GameObject.FindGameObjectWithTag("Player").transform;
		bulletTarget = GameObject.FindGameObjectWithTag("BulletTarget").transform;
		right = false;
		damageCooldown = 0f;
		healthScript = player.GetComponent<HealthScript>();
		healthBar.transform.renderer.material.color = Color.red;
		anim = enemy.GetComponent<Animator>();
		enemyLayer = 1 << 13;
		playerLayer = 1 << 8 | 1 << 14;
	}
	
	void FixedUpdate()
	{
		if (player != null)
		{
			
			float distance = Vector2.Distance(transform.position, player.position);
			
			if (distance <= shootingDistance && distance >= meleeDistance)
			{
				
				if (shootingCooldown <= 0 && LineOfSight())
				{
					anim.SetTrigger ("shoot");
					Shoot();
				}
			}
			else if (distance <= meleeDistance && distance >= minDistance)
			{
				aiming = false;
				//Making a relativity vector between player and enemy
				Vector2 direction = player.position - transform.position;
				//Making the enemy able to face the player depending on where the player is
				if (direction.x < 0 && right)
					Flip();
				else if (direction.x > 0 && !right)
					Flip();
				
				if (direction.x < 0)
				{
					rigidbody2D.velocity = new Vector2(move * -1, rigidbody2D.velocity.y);
				}
				else if (direction.x > 0)
				{
					rigidbody2D.velocity = new Vector2(move, rigidbody2D.velocity.y);
				}
			}
			else if (!LineOfSight())
				aiming = false;
			
			if (distance >= meleeDistance || distance <= minDistance)
			{
				rigidbody2D.velocity = new Vector2(0f, rigidbody2D.velocity.y);
				
			}
		}
	}
	
	// Update is called once per frame
	void Update()
	{
		Debug.DrawLine(visionStart.transform.position, visionEnd.transform.position, Color.red);
		Debug.DrawLine(visionStart.transform.position, visionHigh.transform.position, Color.red);
		Debug.DrawLine(visionStart.transform.position, visionLow.transform.position, Color.red);

		anim.SetBool("aim", aiming);

		// Death
		if (HP <= 0)
		{
			anim.SetTrigger("Death");
			Destroy(gameObject, 1);
		}
		
		if (damageCooldown > 0)
		{
			damageCooldown -= Time.deltaTime;
		}
		if (shootingCooldown > 0)
		{
			if (LineOfSight())
			aiming = true;

			shootingCooldown -= Time.deltaTime;
		}
		
		if (canAttack)
		{
			canAttack = false;
			anim.SetTrigger("hit");
			damageCooldown = hitRate;
			
			if (!anim.GetCurrentAnimatorStateInfo(0).IsName("Knock Back"))
				healthScript.Hit(damage);
		}
		
		float moveX = rigidbody2D.velocity.x;
		anim.SetFloat("speed", Mathf.Abs(moveX));
	}
	
	public void Hurt(int damage)
	{
		
		HP -= damage;
		
		// Scale health bar down
		Vector3 temp = healthBar.localScale;
		temp.z += 0.01f;
		healthBar.localScale = temp;
		
		anim.SetTrigger("hurt");
	}
	
	void Flip()
	{
		right = !right;
		Vector3 scale = transform.localScale;
		scale.x *= -1;
		transform.localScale = scale;
	}
	
	void OnTriggerStay2D(Collider2D other)
	{
		if (other.gameObject.tag == "PlayerHitbox")
		{
			if (damageCooldown <= 0f)
				canAttack = true;
		}
	}
	
	void Shoot()
	{
		shootingCooldown = shootingRate;
		GameObject Bullet;
		Quaternion bulletDirection = Quaternion.LookRotation(bulletTarget.position - transform.position);
		
		Bullet = Instantiate(bullet, transform.position, Quaternion.identity) as GameObject;
		bulletDirection.x = 0;
		bulletDirection.y = 0;
		Vector2 forceDirection = bulletTarget.position - transform.position;
		Bullet.transform.rotation = Quaternion.Slerp(transform.rotation, bulletDirection, 1f);
		//Bullet.rigidbody2D.AddForce(forceDirection*10);
		Bullet.rigidbody2D.AddForce(forceDirection.normalized * Time.deltaTime * bulletSpeed);
		if (right)
		{
			Vector3 scale = Bullet.transform.localScale;
			scale *= -1;
			Bullet.transform.localScale = scale;
		}
		
		
		
	}
	
	public bool Right()
	{
		return right;
	}
	
	bool LineOfSight()
	{
		
		bool playerSpotted = false;
		Vector2 viewDirection = bulletTarget.transform.position - visionStart.transform.position;
		RaycastHit2D hithigh = Physics2D.Linecast(visionStart.transform.position, visionHigh.transform.position, ~enemyLayer);
		RaycastHit2D hitmid = Physics2D.Linecast(visionStart.transform.position, visionEnd.transform.position, ~enemyLayer);
		RaycastHit2D hitlow = Physics2D.Linecast(visionStart.transform.position, visionLow.transform.position, ~enemyLayer);
		if (hithigh.collider != null)
		{
			if (hithigh.collider.tag == "PlayerDamagebox" || hithigh.collider.tag == "PlayerHitbox")
			{
				playerSpotted = true;
			}
		}
		if (hitmid.collider != null)
		{
			if (hitmid.collider.tag == "PlayerDamagebox" || hitmid.collider.tag == "PlayerHitbox")
			{
				playerSpotted = true;
			}
		}
		if (hitlow.collider != null)
		{
			if (hitlow.collider.tag == "PlayerDamagebox" || hitlow.collider.tag == "PlayerHitbox")
			{
				playerSpotted = true;
			}
		}
		
		
		
		
		return playerSpotted;
	}
	
	
}