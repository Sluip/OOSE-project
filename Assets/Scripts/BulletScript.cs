using UnityEngine;
using System.Collections;

public class BulletScript : MonoBehaviour
{
    //private GameObject playerHitbox;

    // Use this for initialization
    void Start()
    {
        //playerHitbox = GameObject.FindGameObjectWithTag("PlayerHitbox");
        Destroy(gameObject, 2);


    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //if (Physics2D.Raycast(transform.position, playerHitbox.transform.position, 0.1f)) {
        //	Destroy (gameObject);
        //	}

    }

}
