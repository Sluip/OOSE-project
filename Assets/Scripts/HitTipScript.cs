using UnityEngine;
using System.Collections;

public class HitTipScript : MonoBehaviour {
	
	private bool hitTip = false;
	
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	
	void OnTriggerEnter2D(Collider2D other)
	{
		if(other.gameObject.tag == "Player")
			hitTip = true;
	}
	
	public bool HitTip()
	{
		return hitTip;
	}
	
	public void HitTipDisable()
	{
		hitTip = false;
	}
}
