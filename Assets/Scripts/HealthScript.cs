using UnityEngine;
using System.Collections;

//We use this class to control Health Points
public class HealthScript : MonoBehaviour
{

    private float HP;
    private bool isDead;

	private HealthBarDestroyer healthBarDestroyer;
	private GameOverSoundScript gameOverSoundScript;
	
    private GameObject healthBarPlayer, gameOverSound;

    void Start()
    {
        //Player starts with 10 HP. We set components that control the healthbar
		HP = 10f;
		healthBarPlayer = GameObject.FindWithTag("HealthBarPlayer");
		healthBarDestroyer = healthBarPlayer.GetComponent<HealthBarDestroyer>();
		gameOverSound = GameObject.FindWithTag("Foreground");
		gameOverSoundScript = gameOverSound.GetComponent<GameOverSoundScript> ();
    }

    // Update is called once per frame
    void Update()
    {   
        // If HP is 0 or less, destroy Player.
        if (HP <= 0)
        {
            isDead = true;
			gameOverSoundScript.GameOverSound();
            Destroy(gameObject);
        }
        //Call Death() if player falls below a certain point
        if (transform.position.y < 0.0f) 
        {
        Death();
        }
    }
    //Gives damage to the player through this method
    public void Hit(int damage)
    {
        HP -= damage;
        // Scale health bar down
		healthBarDestroyer.DestroyHealthBar (damage);
    }
    //Kills the player when called
    public void Death() 
    {
    	this.Hit(1000);
    }
    //Returns the current state of dead or alive
    public bool IsDead()
    {
        return isDead;
    }
}
