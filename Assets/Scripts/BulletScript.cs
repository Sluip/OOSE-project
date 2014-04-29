using UnityEngine;
using System.Collections;

//We use this class to control bullet behaviour
public class BulletScript : MonoBehaviour
{

	private GameObject playerHitbox, player;
	private HealthScript healthScript;
	
	private int playerLayer;

	

    // Use this for initialization
    void Start()
    {
    	//Finding the Player and PlayerHitbox GameObjects in stage when bullet is spawned
    	player = GameObject.FindGameObjectWithTag ("Player");
    	playerHitbox = GameObject.FindGameObjectWithTag("PlayerHitbox");
    	//Finding the HealthScript attached to the Player GameObject
    	healthScript = player.GetComponent<HealthScript>();
    	//Destroys the Bullet 5 seconds after being spawned, to avoid bullets flying infinitely
    	Destroy(gameObject, 5);
    	//Using bitwise operation to store an int of the possible LayerMasks the player can be on
    	playerLayer = 1 << 8 | 1 << 14;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
    	//Make sure the bullet instance exists to avoid null reference before we proceed
    	if(this.gameObject != null){
    		//Making a vector for the direction of the Raycast that the bullet uses to check for collisions
    		Vector2 rayDirection = playerHitbox.transform.position - transform.position;

    		//Shoot out a Raycast in direction of the player, normalized because we only need direction, the third argument controls length of the Vector
    		//We only need to check right in front of the bullet in the direction it's going.
    		//If it hits a layer with a collider that the player is on, deal 2 damage to the player.
    		if (Physics2D.Raycast(transform.position, rayDirection.normalized,1.0f, playerLayer)){
    			healthScript.Hit(2);
    			Destroy (gameObject);
    		}
    		//If it hits anything else, destroy it without dealing damage.
    		else if (Physics2D.Raycast(transform.position, rayDirection.normalized,1.0f)){
    			Destroy(gameObject);
    		}	
    	}
    }
    

}
