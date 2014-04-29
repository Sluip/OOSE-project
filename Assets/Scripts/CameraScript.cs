using UnityEngine;
using System.Collections;

//We use this class to control camera behaviour
public class CameraScript : MonoBehaviour
{

    private Vector3 v = Vector3.zero;
    private Vector3 endpoint;
    private Vector3 delta;
    
    private bool startPosition;

    private MovementScript charMove;

    public float moveSpeed;

    public Transform player;
    // Use this for initialization

    void Start()
    {
      startPosition = false;
      charMove = player.GetComponent<MovementScript>();
    }

    // Update is called once per frame
    void Update()
    {
        //When starting the game, this makes sure the camera quickly focuses on the player
        if (!startPosition)
        transform.position = new Vector3(player.transform.position.x,player.transform.position.y,transform.position.z);
        if (player.transform.position.y == transform.position.y)
        startPosition = true;
    }
    void LateUpdate() {
        //If player exists and we have focused him, we can proceed with normal camera function
        if (player && startPosition)
        {
            //This method transforms the current camera view into vector points we can operate with
            Vector3 point = camera.WorldToViewportPoint(player.position);
            //Making a delta that changes depending on which way the player is facing, to ensure the player is positioned farthest from the side he is facing
            if (charMove.Right())
            {
                delta = player.position - camera.ViewportToWorldPoint(new Vector3(0.3f, 0.4f, point.z));
            }
            else if (!charMove.Right())
            {
                delta = player.position - camera.ViewportToWorldPoint(new Vector3(0.7f, 0.4f, point.z));
            }
            //The endpoint is where the camera will end up when moving, the cameras old position + the delta from before
            endpoint = transform.position + delta;
        }	
        Vector2 transformInverse;

        transformInverse.x = transform.position.x + 1f;
        transformInverse.y = transform.position.y;
        //If the player is moving left/right, or if the player is moving up or down, or if startPosition has been set to true, 
        //perform the SmoothDamp function which moves the camera according to moveSpeed
        if (Mathf.Abs (player.rigidbody2D.velocity.x) > 0 || Mathf.Abs(player.rigidbody2D.velocity.y) > 0 || startPosition)
        {
           transform.position = Vector3.SmoothDamp(transform.position, endpoint, ref v, moveSpeed);
        }     
    }
}

