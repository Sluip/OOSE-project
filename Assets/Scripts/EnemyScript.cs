using UnityEngine;
using System.Collections;

public class EnemyScript : MonoBehaviour
{
	
	public int HP, damage, shootingDamage;
	private int enemyLayer;

	public float meleeDistance, minDistance, maxMovementSpeed, shootingDistance;
	public float move;
	public float shootingRate, hitRate, bulletSpeed;
	private float damageCooldown, shootingCooldown;

	private bool right, canAttack, aiming, spotted, alarmSoundPlayed, dyingSoundPlayed, reloadSoundPlayed;
	
	private HealthScript healthScript;
	private EnemyAudioScript enemyAudioScript;

	public GameObject visionStart, visionEnd, visionHigh, visionLow, bullet;
	private GameObject enemySound;

	private Animator anim;

	public Transform enemy, healthBar;
	private Transform player, bulletTarget;
	
	// Use this for initialization
	void Start()
	{
		//Starting not being able to attack since player not nearby and facing left.
		canAttack = false;
		right = false;
		//Puts transforms in the private transforms due to enemies being a prefab, this cannot be set manually for each instance
		player = GameObject.FindGameObjectWithTag("Player").transform;
		bulletTarget = GameObject.FindGameObjectWithTag("BulletTarget").transform;

		damageCooldown = 0f;
		//Finds HealthScript component and animator component
		healthScript = player.GetComponent<HealthScript>();
		healthBar.transform.renderer.material.color = Color.red;
		anim = enemy.GetComponent<Animator>();
		//Bitwise operations to make integers of the correct layers needed later
		enemyLayer = 1 << 13 | 1 << 15;
		//Call Patrol after 4 seconds and call it again every 4 seconds from then on.
		InvokeRepeating("Patrol",4f,4f);
		enemySound = gameObject.transform.FindChild("Sound").gameObject;
		enemyAudioScript = enemySound.GetComponent<EnemyAudioScript> ();
		alarmSoundPlayed = false;
	}
	
	void FixedUpdate()
	{
		if (player != null)
		{
			//Setting a float for the distance between the player and the enemy
			float distance = Vector2.Distance(transform.position, player.position);
			//If you are within shooting distance but above melee distance, and if you get spotted it will proceed
			if (distance <= shootingDistance && distance >= meleeDistance && spotted)
			{
				//If the enemy shooting cooldown is 0 or less, and enemy has LineOfSight(), it will call Shoot
				if (shootingCooldown <= 0 && LineOfSight())
				{
					anim.SetTrigger ("shoot");
					Shoot();
					enemyAudioScript.GunshotSound();
					reloadSoundPlayed = false;
					enemyAudioScript.CartridgesSound();
				}
			}
			//If the enemy is within melee distance and it has spotted you, it will proceed. 
			else if (distance <= meleeDistance && distance >= minDistance && spotted)
			{
				aiming = false;
				//Making a relativity vector between player and enemy
				Vector2 direction = player.position - transform.position;
				//Making the enemy able to face the player depending on where the player is
				if (direction.x < 0 && right)
				Flip();
				else if (direction.x > 0 && !right)
				Flip();
				//The enemy will chase you down within melee distance to attack you.
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
			{
				aiming = false;
			}
			//Ensuring that the enemy will not proceed past the player
			if (distance >= meleeDistance || distance <= minDistance)
			{
				rigidbody2D.velocity = new Vector2(0f, rigidbody2D.velocity.y);
				
			}
		}
	}
	
	// Update is called once per frame
	void Update()
	{
		//Debugging to check enemy sight, will only be shown in stage view
		Debug.DrawLine(visionStart.transform.position, visionEnd.transform.position, Color.red);
		Debug.DrawLine(visionStart.transform.position, visionHigh.transform.position, Color.red);
		Debug.DrawLine(visionStart.transform.position, visionLow.transform.position, Color.red);
		//Call spotted on every frame to check if the player gets spotted
		Spotted();
		
		anim.SetBool("aim", aiming);

		// Death if HP goes to 0 or below
		if (HP <= 0)
		{
			anim.SetTrigger("Death");
			Destroy(gameObject, 1);
			if(!dyingSoundPlayed)
			{
				enemyAudioScript.DyingSound();
				dyingSoundPlayed = true;
			}

		}
		//Cooldown countdown on melee and shooting respectively
		if (damageCooldown > 0)
		{
			damageCooldown -= Time.deltaTime;
		}
		if (shootingCooldown > 0)
		{
			//Line of sight must be true for an enemy to aim
			if (LineOfSight())
			{
				aiming = true;
				if(!reloadSoundPlayed)
				enemyAudioScript.ReloadSound();
				reloadSoundPlayed = true;
			}
			shootingCooldown -= Time.deltaTime;
		}
		
		if (canAttack)
		{
			//We control the melee attack of the enemy and also makes him unable to attack while getting knocked back
			canAttack = false;
			anim.SetTrigger("hit");
			damageCooldown = hitRate;
			
			if (!anim.GetCurrentAnimatorStateInfo(0).IsName("Knock Back")){
				healthScript.Hit(damage);
			}
		}
		//Sending the current velocity to the animator
		float moveX = rigidbody2D.velocity.x;
		anim.SetFloat("speed", Mathf.Abs(moveX));
	}
	
	public void Hurt(int damage)
	{
		
		HP -= damage;
		// Scale health bar down when enemy takes damage
		Vector3 temp = healthBar.localScale;
		temp.z += 0.01f;
		healthBar.localScale = temp;
		
		anim.SetTrigger("hurt");
	}
	//Flips the character on the x axis to make him turn around when called
	void Flip()
	{
		right = !right;
		Vector3 scale = transform.localScale;
		scale.x *= -1;
		transform.localScale = scale;
	}
	//We make the enemy attack when he gets in range of the players hitbox
	void OnTriggerStay2D(Collider2D other)
	{
		if (other.gameObject.tag == "PlayerHitbox")
		{
			if (damageCooldown <= 0f)
			canAttack = true;
		}
	}
	//The enemy will shoot when this is called
	void Shoot()
	{
		shootingCooldown = shootingRate;
		//Makes a Quaternion to ensure bullets are rotated correctly towards player
		Quaternion bulletDirection = Quaternion.LookRotation(bulletTarget.position - transform.position);
		//Instantiates bullet that are rotated in a neutral state
		GameObject Bullet;		
		Bullet = Instantiate(bullet, transform.position, Quaternion.identity) as GameObject;
		//Rotates the bullet towards the player using Slerp and the stored Quaternion
		bulletDirection.x = 0;
		bulletDirection.y = 0;
		Vector2 forceDirection = bulletTarget.position - transform.position;
		Bullet.transform.rotation = Quaternion.Slerp(transform.rotation, bulletDirection, 1f);
		//Adds force to a bullet that is normalized to ensure the same speed no matter how far the player is
		Bullet.rigidbody2D.AddForce(forceDirection.normalized * Time.deltaTime * bulletSpeed);
		//If the enemy is facing to the right, rotate the bullet the opposite way as well
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
		//Casts 3 Raycasts from the enemy to create a cone view in the direction the enemy is facing
		RaycastHit2D hithigh = Physics2D.Linecast(visionStart.transform.position, visionHigh.transform.position, ~enemyLayer);
		RaycastHit2D hitmid = Physics2D.Linecast(visionStart.transform.position, visionEnd.transform.position, ~enemyLayer);
		RaycastHit2D hitlow = Physics2D.Linecast(visionStart.transform.position, visionLow.transform.position, ~enemyLayer);
		//Checks collision on all cast ray, and if the player enters any of the ray, set playerSpotted to true and return this, if not it will return false
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
	//Simple method to make the enemy "patrol" if it hasnt seen the player
	void Patrol() {
		if (!LineOfSight() && !spotted) {
			Flip();
		}
	}
	//If the player is spotted by an enemy, it will activate the alert mark above the enemy
	void Spotted() {
		if(LineOfSight()) {
			spotted = true;
			transform.FindChild("Alerted").gameObject.SetActive(true);

			if(!alarmSoundPlayed)
			enemyAudioScript.AlarmSound();
			alarmSoundPlayed = true;
		}
	}
	//Returns the current state of spotted to use in other classes
	public bool IsSpotted() {
		return spotted;
	}
}