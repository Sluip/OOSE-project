using UnityEngine;
using System.Collections;

public class DoubleJumpTipScript : MonoBehaviour {
	
	private bool doubleJumpTip = false;
	private MovementScript movementScript;
	private GameObject player;
	
	// Use this for initialization
	void Start () {

		player = GameObject.FindWithTag("Player");
		movementScript = player.GetComponent<MovementScript> ();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	
	void OnTriggerEnter2D(Collider2D other)
	{
		if(other.gameObject.tag == "Player")
			doubleJumpTip = true;
			movementScript.move = 0f;
	}
	
	public bool DoubleJumpTip()
	{
		return doubleJumpTip;
	}
	
	public void DoubleJumpTipDisable()
	{
		doubleJumpTip = false;
	}
}
