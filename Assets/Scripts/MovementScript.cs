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

	private GameObject playerSound;
	private GameObject player;

	private PlayerAudioScript playerAudioScript;
	private JumpingScript jumpingScript;

    void Start()
    {
        //Setting initial bools for spawning
        right = true;
        flipped = false;
        sprinting = false;
        anim = bitch.GetComponent<Animator>();
		playerSound = GameObject.FindWithTag("PlayerSound");
		playerAudioScript = playerSound.GetComponent<PlayerAudioScript> ();
		player = GameObject.FindWithTag("Player");
		jumpingScript = player.GetComponent<JumpingScript> ();
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

			//If Sprinting sound is not already playing, play it
			if(!playerAudioScript.IsSprintSoundPlaying())
			{
				playerAudioScript.SprintSound();
			}
    	}

    	if (Input.GetButtonUp("Run"))
    	{
        	sprinting = false;

			//Stop sprinting sound
			playerAudioScript.SprintSoundStop();
    	}
		if(!jumpingScript.Grounded() || rigidbody2D.velocity.x == 0f)
			//Stop sprinting sound
			playerAudioScript.SprintSoundStop();

		if(Input.GetKey ("a") || Input.GetKey ("d"))
			if(!playerAudioScript.IsRunSoundPlaying())
				playerAudioScript.RunSound();

		if(Input.GetKeyUp ("a") || Input.GetKeyUp ("d") || !jumpingScript.Grounded() || rigidbody2D.velocity.x == 0f)
			playerAudioScript.RunSoundStop();

		if(playerAudioScript.IsSprintSoundPlaying())
			playerAudioScript.RunSoundStop();

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
