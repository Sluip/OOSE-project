using UnityEngine;
using System.Collections;

public class MovementScript : MonoBehaviour
{
    public float maxMovementSpeed = 10.0f;
    private bool right;
    public Transform bitch;
    private bool sprinting;
	[HideInInspector]
    public float move;
    Animator anim;
    private bool isSprinting;
    [HideInInspector]
    public bool flipped;

    // Use this for initialization
    void Start()
    {
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

        //Movement when sprinting
        }
        else if (sprinting)
        {
            move = Input.GetAxis("Horizontal");
            rigidbody2D.velocity = new Vector2(move * maxMovementSpeed * 1.5f, rigidbody2D.velocity.y);
        }

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

        if (Mathf.Abs(rigidbody2D.velocity.x) > maxMovementSpeed)
            isSprinting = true;
        else
            isSprinting = false;

        if (Input.GetButton("Run"))
        {
            sprinting = true;
        }
        if (Input.GetButtonUp("Run"))
        {
            sprinting = false;
        }
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

    public bool Sprinting()
    {
        return sprinting;
    }
    public bool Right()
    {
        return right;
    }

}
