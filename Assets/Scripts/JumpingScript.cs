using UnityEngine;
using System.Collections;

//We use this class to control jumping
public class JumpingScript : MonoBehaviour
{
    private float startJumpSpeed, deathFall;
    public float maxJumpSpeed, speedLimiter, deathThreshold;

    private HealthScript healthScript;
    private MovementScript charMove;

    private int groundLayer; 
    
    private bool jumped, running, doubleJumped, grounded;
    
    private Vector2 rectangleSize;
    
    private Animator anim;
    public Transform groundChecker, player;

    void Start()
    {
        //Sets variables for use later in the class
        doubleJumped = false;
        startJumpSpeed = (maxJumpSpeed / 1.3f);
        deathFall = 0;
        //Gets components from other classes
        healthScript = GetComponent<HealthScript>();
        charMove = gameObject.GetComponent<MovementScript>();
        anim = player.GetComponent<Animator>();
        //Sets an integer with layers of the ground using bitwise operations
        groundLayer = 1 << 9 | 1 << 10;
    }

    void FixedUpdate()
    {
        //Ensures that player cannot go above certain speed on the y axis
        if (rigidbody2D.velocity.y > speedLimiter)
        {
            rigidbody2D.velocity = new Vector2(rigidbody2D.velocity.x, speedLimiter);
        }
        //Checks the fallspeed to cause death when it is too high
        if (rigidbody2D.velocity.y < 0)
        {
           deathFall = rigidbody2D.velocity.y;
       }
        //We check if the player is grounded by using an OverlapArea which makes a rectangle below the player to collide with certain layers
        if (charMove.Right())
        {
            rectangleSize.x = groundChecker.position.x + 0.4f;
            rectangleSize.y = groundChecker.position.y + 0.2f;
        }
        else if (!charMove.Right())
        {
            rectangleSize.x = groundChecker.position.x - 0.4f;
            rectangleSize.y = groundChecker.position.y + 0.2f;
        }
        //Sets grounded to true if the OverlapArea collides with the groundLayer
        grounded = Physics2D.OverlapArea(groundChecker.position, rectangleSize, groundLayer);
        
        //Kills the player if it lands with too high speed
        if (grounded)
        {	
        	if (deathFall < deathThreshold){
        		healthScript.Death ();
        	}
        	else if (deathFall > deathThreshold){
        		deathFall = 0;
            }   
            //Makes the player able to doublejump again when landed
            doubleJumped = false;
        }
        //Animation control
        anim.SetBool("Ground", grounded);
        anim.SetFloat("vSpeed", rigidbody2D.velocity.y);
        anim.SetFloat("AbsVSpeed", Mathf.Abs(rigidbody2D.velocity.y));

        //Controls the downwards jumping through platforms while holding S
        if (rigidbody2D.velocity.y < 0.0f && !Input.GetKey(KeyCode.S))
        {

            gameObject.layer = LayerMask.NameToLayer("Player");
        }
        //During the jump, change the layer of the player to ensure that he can jump through the JumpThroughPlatforms
        if (jumped)
        {
            if (grounded)
            {
                //JumpThroughPlayer and JumpTHhroughPlatform do not collide, defined in the Unity layer matrix
                rigidbody2D.AddForce(new Vector2(0, startJumpSpeed));
                gameObject.layer = LayerMask.NameToLayer("JumpThroughPlayer");
            }

            if (!doubleJumped && !grounded)
            {
                // Set vertical velocity to zero before double jump
                Vector3 vel = rigidbody2D.velocity;
                vel.y = 0;
                rigidbody2D.velocity = vel;

                gameObject.layer = LayerMask.NameToLayer("JumpThroughPlayer");
                //Adds force to the doublejump
                rigidbody2D.AddForce(new Vector2(0, startJumpSpeed));
                doubleJumped = true;
            }
            jumped = false;
        }
        //Makes the player jump down if jump is pressed while holding S, similarly to jumping up we set the layer to JumpThroughPlayer as to not collide with 
        //JumpThroughPlatforms
        if (Input.GetButtonDown("Jump") && Input.GetKey(KeyCode.S))
        {
            gameObject.layer = LayerMask.NameToLayer("JumpThroughPlayer");
            rigidbody2D.AddForce(new Vector2(0, startJumpSpeed / 1.5f));
        }
    }
    // Update is called once per frame
    void Update()
    {

        if (Input.GetButtonDown("Jump") && !Input.GetKey(KeyCode.S))
        {
            jumped = true;
        }
    }
    //This method simply returns whether or not the player is currently grounded
    public bool Grounded()
    {
        return grounded;
    }
}