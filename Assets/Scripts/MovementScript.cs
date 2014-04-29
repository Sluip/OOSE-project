using UnityEngine;
using System.Collections;

//We use this class to control our movement for the player
public class MovementScript : MonoBehaviour
{
    public float maxMovementSpeed;
    private float move;

    private bool right, sprinting, isSprinting, flipped;
    
    private Animator anim;
    public Transform bitch;

    void Start()
    {
        //Setting initial bools for spawning
        right = true;
        flipped = false;
        sprinting = false;
        anim = bitch.GetComponent<Animator>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //Movement when running
        if (!sprinting)
        {
            move = Input.GetAxis("Horizontal");
            rigidbody2D.velocity = new Vector2(move * maxMovementSpeed, rigidbody2D.velocity.y);
        }

    //Movement when sprinting
    else if (sprinting)
    {
        move = Input.GetAxis("Horizontal");
        rigidbody2D.velocity = new Vector2(move * maxMovementSpeed * 1.5f, rigidbody2D.velocity.y);
    }
    //Flips the player depending on which way the he moves
    if (move > 0 && !right)
    {
        Flip();
    }
    else if (move < 0 && right)
    {
        Flip();
    }
}

void Update()
{

    //Animation control variables
    if (Mathf.Abs(rigidbody2D.velocity.x) > maxMovementSpeed)
    {
        isSprinting = true;
    }
    else
    {
        isSprinting = false;
    }
    if (Input.GetButton("Run"))
    {
        sprinting = true;
    }
    if (Input.GetButtonUp("Run"))
    {
        sprinting = false;
    }
    //More animation control variables
    float moveX = Input.GetAxis("Horizontal");
    anim.SetFloat("speed", Mathf.Abs(moveX));
    anim.SetBool("sprint", isSprinting);
}

    //Inverse character scale on x-axis
    void Flip()
    {
        right = !right;
        Vector3 scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;
        flipped = true;
    }
    //Returns whether the character is sprinting and which way he is facing through these methods
    public bool Sprinting()
    {
        return sprinting;
    }
    public bool Right()
    {
        return right;
    }

}
