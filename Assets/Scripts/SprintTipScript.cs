using UnityEngine;
using System.Collections;

public class SprintTipScript : MonoBehaviour {
	
	private bool sprintTip = false;
	
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	
	void OnTriggerEnter2D(Collider2D other)
	{
		if(other.gameObject.tag == "Player")
			sprintTip = true;
	}
	
	public bool SprintTip()
	{
		return sprintTip;
	}
	
	public void SprintTipDisable()
	{
		sprintTip = false;
	}
}
