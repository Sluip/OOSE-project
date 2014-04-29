using UnityEngine;
using System.Collections;

public class DamageScript : MonoBehaviour
{

    public int damage = 1;
    public float hitRate;
    private bool canAttack = false;
    private float damageCooldown;
    private Animator anim;
    public Transform player;
    private float animCoolDown;
	private EnemyScript enemyScript;

    // Use this for initialization
    void Start()
    {
        
        damageCooldown = 0f;
        animCoolDown = 0f;
        anim = player.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    { 

        if (damageCooldown > 0f)
        {
            damageCooldown -= Time.deltaTime;
        }

        if (animCoolDown > 0f)
        {
            animCoolDown -= Time.deltaTime;
        }

        if (Input.GetKeyDown("j") && (animCoolDown <= 0f))
        {
			animCoolDown = hitRate;
			if (enemyScript == null)
			{
				anim.SetTrigger("hit");		
			}
			else {
            CanAttack();
			}
        }
    }


    // Melee system
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
        if (canAttack && enemyScript.IsSpotted())
        {
			anim.SetTrigger("hit");	
			canAttack = false;
            damageCooldown = hitRate;
		    enemyScript.Hurt(damage);
				
		}
        else if (canAttack && !enemyScript.IsSpotted())
        {
        	anim.SetTrigger ("stab");
        	damageCooldown = hitRate;
        	enemyScript.Hurt (100);
        }
    }
}

