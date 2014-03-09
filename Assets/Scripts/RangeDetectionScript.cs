using UnityEngine;
using System.Collections;

public class RangeDetectionScript : MonoBehaviour {

	public float range;
	public Transform player;
	
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	
	public bool inRange()
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
