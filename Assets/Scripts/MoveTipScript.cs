using UnityEngine;
using System.Collections;

public class MoveTipScript : MonoBehaviour {
	
	private bool moveTip = false;
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
			moveTip = true;
			movementScript.maxMovementSpeed = 0f;
	}
	
	public bool MoveTip()
	{
		return moveTip;
	}
	
	public void MoveTipDisable()
	{
		moveTip = false;
	}
}
