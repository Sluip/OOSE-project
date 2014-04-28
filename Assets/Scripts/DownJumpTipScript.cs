using UnityEngine;
using System.Collections;

public class DownJumpTipScript : MonoBehaviour {
	
	private bool downJumpTip = false;
	
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	
	void OnTriggerEnter2D(Collider2D other)
	{
		if(other.gameObject.tag == "Player")
			downJumpTip = true;
	}
	
	public bool DownJumpTip()
	{
		return downJumpTip;
	}
	
	public void DownJumpTipDisable()
	{
		downJumpTip = false;
	}
}
