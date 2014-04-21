using UnityEngine;
using System.Collections;

public class JumpingScript : MonoBehaviour
{

    // Use this for initialization
    private bool grounded;
    public Transform groundChecker;
    public Transform jumpMeter;
    public Transform player;
    [HideInInspector]
    public float jumpSpeed;
    [HideInInspector]
    public float startJumpSpeed;
    public float maxJumpSpeed;
    public float speedLimiter;
    private bool doubleJumped = false;
    public int chargeTime;
    private MovementScript charMove;
    private int bitMask = 1 << 9 | 1 << 10;
    private Animator anim;
    private bool sprintJumped;
    private bool jumped;
    private bool running;
    private Vector2 rectangleSize;

    void Start()
    {
        charMove = gameObject.GetComponent<MovementScript>();
        startJumpSpeed = (maxJumpSpeed / 1.3f);
        anim = player.GetComponent<Animator>();
        jumpSpeed = startJumpSpeed;
    }

    void FixedUpdate()
    {
        if (rigidbody2D.velocity.y > speedLimiter)
        {
            rigidbody2D.velocity = new Vector2(rigidbody2D.velocity.x, speedLimiter);
        }
        if (charMove.Right())
        {
            rectangleSize.x = groundChecker.position.x + 2.5f;
            rectangleSize.y = groundChecker.position.y + 0.2f;
        }
        else if (!charMove.Right())
        {
            rectangleSize.x = groundChecker.position.x - 2.5f;
            rectangleSize.y = groundChecker.position.y + 0.2f;
        }
        grounded = Physics2D.OverlapArea(groundChecker.position, rectangleSize, bitMask);

        if (grounded)
        {

            doubleJumped = false;
        }

        anim.SetBool("Ground", grounded);
        anim.SetFloat("vSpeed", rigidbody2D.velocity.y);
        anim.SetFloat("AbsVSpeed", Mathf.Abs(rigidbody2D.velocity.y));

        if (rigidbody2D.velocity.y < 0.0f && !Input.GetKey(KeyCode.S))
        {

            gameObject.layer = LayerMask.NameToLayer("Player");
        }

        if (sprintJumped)
        {

            if (!doubleJumped && !grounded)
            {

                // Set vertical velocity to zero before double jump
                Vector3 vel = rigidbody2D.velocity;
                vel.y = 0;
                rigidbody2D.velocity = vel;

                rigidbody2D.AddForce(new Vector2(0, startJumpSpeed));
                gameObject.layer = LayerMask.NameToLayer("JumpThroughPlayer");

                // Double jump disabled until player grounded
                doubleJumped = true;
            }


            if (grounded)
            {

                rigidbody2D.AddForce(new Vector2(0, jumpSpeed));

                gameObject.layer = LayerMask.NameToLayer("JumpThroughPlayer");
            }

            sprintJumped = false;
        }



        if (jumped)
        {

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

            if (grounded)
            {

                rigidbody2D.AddForce(new Vector2(0, startJumpSpeed));
                gameObject.layer = LayerMask.NameToLayer("JumpThroughPlayer");
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

        //Sprinting
        if (charMove.Sprinting())
        {

			if (jumpSpeed <= maxJumpSpeed)
			{
				jumpSpeed += (int)(chargeTime * Time.deltaTime);
			}
			

            if (Input.GetButtonDown("Jump"))
            {

                sprintJumped = true;
            }
        }

        else if (!charMove.Sprinting())
        {
      

            if (Input.GetButtonDown("Jump") && !Input.GetKey(KeyCode.S))
            {	

                jumped = true;
            }
        }







        //Reset jump speed if sprint button released, turns around or has a velocity.x less or equal to 6
        if (Input.GetButtonUp("Run") || charMove.flipped || Mathf.Abs(rigidbody2D.velocity.x) <= 6f)
        {
            jumpSpeed = startJumpSpeed;
            charMove.flipped = false;
        }
    }

    public bool Grounded()
    {

        return grounded;
    }
}
