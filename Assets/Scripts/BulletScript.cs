using UnityEngine;
using System.Collections;

public class BulletScript : MonoBehaviour
{
    private GameObject playerHitbox;
	private int playerLayer;
	private GameObject player;
	private HealthScript healthScript;

    // Use this for initialization
    void Start()
    {
    	player = GameObject.FindGameObjectWithTag ("Player");
        playerHitbox = GameObject.FindGameObjectWithTag("PlayerHitbox");
        healthScript = player.GetComponent<HealthScript>();
        Destroy(gameObject, 5);
		playerLayer = 1 << 8 | 1 << 14;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
		if(this.gameObject != null){
		Vector2 rayDirection = playerHitbox.transform.position - transform.position;
		

		if (Physics2D.Raycast(transform.position, rayDirection.normalized,1.0f, playerLayer)){
			healthScript.Hit(2);
        	
        		Destroy (gameObject);
		}
        	}
}
    

}
