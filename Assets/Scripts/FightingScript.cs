using UnityEngine;
using System.Collections;

public class FightingScript : MonoBehaviour {

	public float speed;
	public float range;
	public Transform player;
	public CharacterController controller;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

		Debug.Log (inRange());
	
	}

	bool inRange()
	{
		if(Vector2.Distance(transform.position, player.position)<range)
		{
			return true;
		}
		else
		{
			return false;
		}
	}
}
