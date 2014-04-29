using UnityEngine;
using System.Collections;
//We use this class to control the tip for double jump
public class DoubleJumpTipScript : MonoBehaviour {
	
	private TipAnimationTrigger tipAnimationTrigger;

	private GameObject tips;
	public GameObject doubleJump;
	
	// Use this for initialization
	void Start () {
		tips = GameObject.FindWithTag("Tips");
		tipAnimationTrigger = tips.GetComponent<TipAnimationTrigger> ();
	}
	
	//Displays a tip when player collides with the trigger and removes it when he leaves the trigger
	void OnTriggerEnter2D(Collider2D other)
	{
		if(other.gameObject.tag == "Player")
		{
			tipAnimationTrigger.Tip (true);
			TipRenderer(true);
		}	
	}
	
	void OnTriggerExit2D(Collider2D other)
	{
		if(other.gameObject.tag == "Player")
		{
			tipAnimationTrigger.Tip (false);
			TipRenderer(false);
		}
	}
	
	private void TipRenderer(bool b)
	{
		doubleJump.renderer.enabled = b;
	}
}
