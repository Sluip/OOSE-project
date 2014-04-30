using UnityEngine;
using System.Collections;

public class DamageScript : MonoBehaviour
{

    public int damage = 1;
    private int enemyLayer;
    
    public float hitRate, damageCooldown, animCoolDown;
    private bool canAttack;
    
    private Animator anim;
	private EnemyScript enemyScript;
	private PlayerAudioScript playerAudioScript;

    public Transform player, playerVisionStart, playerVisionEnd;

	private GameObject playerSound;

    // Use this for initialization
    void Start()
    {
        //The player doesn't start within range of an enemy so this is false
        canAttack = false;
        //Setting our initial variables to make the player start out being able to perform an attack
        damageCooldown = 0f;
        animCoolDown = 0f;
        //Finding the animator component to control the animation
        anim = player.GetComponent<Animator>();
		enemyLayer = 1 << 13;
        

		playerSound = GameObject.FindWithTag("PlayerSound");
		playerAudioScript = playerSound.GetComponent<PlayerAudioScript> ();

    }

    // Update is called once per frame
    void Update()
    { 
        //Controlling cooldowns, if they are above 0 they will count down using deltaTime
        if (damageCooldown > 0f)
        {
            damageCooldown -= Time.deltaTime;
        }

        if (animCoolDown > 0f)
        {
            animCoolDown -= Time.deltaTime;
        }
        //If attack is pressed, and there is no nearby enemy, play the attack animation, otherwise go to CanAttack
        if (Input.GetButtonDown("Attack") && (animCoolDown <= 0f))
        {
			animCoolDown = hitRate;
			if (enemyScript == null)
			{
				anim.SetTrigger("hit");
				playerAudioScript.SwingSound();
			}
			else {
            CanAttack();
			}
        }
    }


    // Melee systee, if you are standing close to an enemy, canAttack becomes true, and it makes sure that you get the enemyScript 
    //from the opponent you are currently fighting.
    void OnTriggerStay2D(Collider2D enemy)
    {
    	
        if (enemy.gameObject.tag == "Enemy")
        {
            if (damageCooldown <= 0f)
                canAttack = true;
        }
    }
	void OnTriggerEnter2D(Collider2D enemy)
	{
		
		if (enemy.gameObject.tag == "Enemy")
		{
			enemyScript = enemy.GetComponent<EnemyScript>();
		}
	}
    void CanAttack()
    {
        //If you are spotted by an enemy, play the normal hit animation and call Hurt to damage the enemy with normal damage.
        if (canAttack && enemyScript.IsSpotted())
        {
			anim.SetTrigger("hit");	
			canAttack = false;
            damageCooldown = hitRate;
            
			if (LineOfSight())
			{
				//If the enemy is in range, deal damage to him
		    	enemyScript.Hurt(damage);
                playerAudioScript.PunchSound();
			}
		}
        //If you are not spotted by the enemy, it means he is not facing you and you therefore backstab instead, dealing 100
        //See IsSpotted() and LineOfSight() declaration in EnemyScript for more details on how this is performed.
        else if (canAttack && !enemyScript.IsSpotted())
        {
        	anim.SetTrigger ("stab");
        	damageCooldown = hitRate;
        	enemyScript.Hurt (100);
			playerAudioScript.StabSound();
        }
    }
    private bool LineOfSight() {
		bool enemySpotted = false;
		//Casts 1 Raycast from the player to make sure he is in range of and facing the enemy
		RaycastHit2D hit = Physics2D.Linecast(playerVisionStart.transform.position, playerVisionEnd.transform.position, enemyLayer);
		//Checks collision on all cast ray, and if the player enters any of the ray, set playerSpotted to true and return this, if not it will return false
		if (hit.collider != null)
			{
				if (hit.collider.tag == "Enemy")
				{
					enemySpotted = true;
				}
			}
		return enemySpotted;
	}
}
