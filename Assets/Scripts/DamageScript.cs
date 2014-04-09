using UnityEngine;
using System.Collections;

public class DamageScript : MonoBehaviour
{

    public int damage = 1;
    public float hitRate;
    private bool canAttack = false;
    private float damageCooldown;
    public Transform enemy;
    private Animator anim;
    public Transform player;
    private float animCoolDown;

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

        if (Input.GetKeyDown("r") && (animCoolDown <= 0f))
        {
            anim.SetTrigger("hit");
            animCoolDown = hitRate;

            CanAttack();

        }
    }


    // Melee system
    void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            if (damageCooldown <= 0f)
                canAttack = true;
        }
    }

    void CanAttack()
    {
        if (canAttack)
        {
            canAttack = false;
            damageCooldown = hitRate;
            enemy.gameObject.GetComponent<EnemyScript>().Hurt(damage);
        }
    }
}

