using UnityEngine;
using System.Collections;

public class JumpingScript : MonoBehaviour
{

    // Use this for initialization
    private bool grounded;
    public Transform groundChecker;
    public Transform player;
    [HideInInspector]
    public float startJumpSpeed;
    public float maxJumpSpeed;
    public float speedLimiter;
    public float deathThreshold;
	private float deathFall;
	private HealthScript healthScript;
    private bool doubleJumped = false;
    private MovementScript charMove;
    private int bitMask = 1 << 9 | 1 << 10;
    private Animator anim;
    private bool jumped;
    private bool running;
    private Vector2 rectangleSize;
   

    void Start()
    {
    	healthScript = GetComponent<HealthScript>();
        charMove = gameObject.GetComponent<MovementScript>();
        startJumpSpeed = (maxJumpSpeed / 1.3f);
        anim = player.GetComponent<Animator>();
        deathFall = 0;
    }

    void FixedUpdate()
    {
        if (rigidbody2D.velocity.y > speedLimiter)
        {
            rigidbody2D.velocity = new Vector2(rigidbody2D.velocity.x, speedLimiter);
        }
       	if (rigidbody2D.velocity.y < 0)
       	{
       		deathFall = rigidbody2D.velocity.y;
       	}
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

        grounded = Physics2D.OverlapArea(groundChecker.position, rectangleSize, bitMask);
        
        

        if (grounded)
        {	
        	if (deathFall < deathThreshold){
        		healthScript.Death ();
        	}
        	else if (deathFall > deathThreshold){
        		deathFall = 0;
        }
        	

            doubleJumped = false;
        }

        anim.SetBool("Ground", grounded);
        anim.SetFloat("vSpeed", rigidbody2D.velocity.y);
        anim.SetFloat("AbsVSpeed", Mathf.Abs(rigidbody2D.velocity.y));

        if (rigidbody2D.velocity.y < 0.0f && !Input.GetKey(KeyCode.S))
        {

            gameObject.layer = LayerMask.NameToLayer("Player");
        }

        if (jumped)
        {

            if (grounded)
            {

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

                rigidbody2D.AddForce(new Vector2(0, startJumpSpeed));
                doubleJumped = true;
            }

            jumped = false;
        }

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

    public bool Grounded()
    {

        return grounded;
    }
}