using UnityEngine;
using System.Collections;

public class CameraScript : MonoBehaviour
{
    public float moveSpeed;
    private Vector3 v = Vector3.zero;
    public Transform player;
    private Vector3 endpoint;
    private bool topScreen = false;
    private MovementScript charMove;
    private Vector3 delta;
    private bool startPosition;
    // Use this for initialization

    void Awake()
    {
        //camera.orthographicSize = ((float)((Screen.height / 2f) / 100f));
        
    }

    void Start()
    {
		startPosition = false;
        charMove = player.GetComponent<MovementScript>();
		

    }

    // Update is called once per frame
    void Update()
	    {
	    if (!startPosition)
			transform.position = new Vector3(player.transform.position.x,player.transform.position.y,transform.position.z);
	    if (player.transform.position.y == transform.position.y)
	    	startPosition = true;
	    	
	    }
	void LateUpdate() {

        if (player && startPosition)
        {

            Vector3 point = camera.WorldToViewportPoint(player.position);
            if (charMove.Right())
            {
                delta = player.position - camera.ViewportToWorldPoint(new Vector3(0.3f, 0.4f, point.z));
            }
            else if (!charMove.Right())
            {
                delta = player.position - camera.ViewportToWorldPoint(new Vector3(0.7f, 0.4f, point.z));
            }
            endpoint = transform.position + delta;
//            if (point.y > 0.85f)
//            {
//                topScreen = true;
//            }
//            else if (player.position.y < 1.0f)
//            {
//                topScreen = true;
//            }
//
//        }
//        if (!topScreen)
//        {
//            endpoint.y = 0.0f;
//        }
		}	
        Vector2 transformInverse;
        transformInverse.x = transform.position.x + 1f;
        transformInverse.y = transform.position.y;
        if (Mathf.Abs (player.rigidbody2D.velocity.x) > 0 || Mathf.Abs(player.rigidbody2D.velocity.y) > 0 || startPosition)
        transform.position = Vector3.SmoothDamp(transform.position, endpoint, ref v, moveSpeed);;
		
		
    }

}

