using UnityEngine;
using System.Collections;

public class HealthScript : MonoBehaviour
{

    private float HP;
    public Transform healthBar;
    [HideInInspector]
    public bool something;
	private HealthBarDestroyer healthBarDestroyer;
	private GameObject healthBarPlayer;

    // Use this for initialization
    void Start()
    {
		HP = 10f;
		healthBarPlayer = GameObject.FindWithTag("HealthBarPlayer");
		healthBarDestroyer = healthBarPlayer.GetComponent<HealthBarDestroyer>();
    }

    // Update is called once per frame
    void Update()
    {

        // If HP is 0 or less, destroy Player.
        if (HP <= 0)
        {

            something = true;
            Destroy(gameObject);
        }
        if (transform.position.y < 0.0f) 
        Death();
    }

    public void Hit(int damage)
    {
        HP -= damage;

        // Scale health bar down
		healthBarDestroyer.DestroyHealthBar (damage);
    }
    public void Death() {
    	this.Hit(1000);
    }
}
