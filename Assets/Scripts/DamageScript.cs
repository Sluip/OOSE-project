using UnityEngine;
using System.Collections;

public class DamageScript : MonoBehaviour {

	public int damage = 1;
	public float hitRate;
	private bool canAttack = false;
	private float damageCooldown;
	public Transform enemy;


	// Use this for initialization
	void Start () {
	
		damageCooldown = 0f;
	}
	
	// Update is called once per frame
	void Update () {

		if (damageCooldown > 0) {
			damageCooldown -= Time.deltaTime;
		}

		if(canAttack && Input.GetKeyDown("r")) {
			canAttack = false;
			damageCooldown = hitRate;
			enemy.gameObject.GetComponent<EnemyScript>().Hurt(damage);
		}
	}

	// Melee system
	void OnTriggerStay2D(Collider2D other)
	{
		if(other.gameObject.tag == "Enemy")
		{
			if(damageCooldown <= 0f)
				canAttack = true;
		}
	}
}

