using UnityEngine;
using System.Collections;

public class BulletScript : MonoBehaviour
{
    private GameObject playerHitbox;
	private int playerLayer;
	private HealthScript healthScript;

    // Use this for initialization
    void Start()
    {
        playerHitbox = GameObject.FindGameObjectWithTag("PlayerHitbox");
        Destroy(gameObject, 5);
		playerLayer = 1 << 8 | 1 << 14;
		healthScript = 	


    }

    // Update is called once per frame
    void FixedUpdate()
    {
		Vector2 rayDirection = playerHitbox.transform.position - transform.position;


	if (Physics2D.Raycast(transform.position, rayDirection.normalized,0.1f, playerLayer)){
        	Destroy (gameObject);
		}
        	}

    

}
