using UnityEngine;
using System.Collections;

public class JumpTipScript : MonoBehaviour {

	private bool spaceTip = false;
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
			spaceTip = true;
			movementScript.maxMovementSpeed = 0f;
	}

	public bool SpaceTip()
	{
		return spaceTip;
	}

	public void SpaceTipDisable()
	{
		spaceTip = false;
	}
}
