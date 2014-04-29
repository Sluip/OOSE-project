using UnityEngine;
using System.Collections;

//We use this class to control the tip for DownJump, see DoubleJumpTipScript for further details
public class DownJumpTipScript : MonoBehaviour {
	
	private TipAnimationTrigger tipAnimationTrigger;
	
	private GameObject tips;
	public GameObject downJump;
	
	// Use this for initialization
	void Start () {
		
		tips = GameObject.FindWithTag("Tips");
		tipAnimationTrigger = tips.GetComponent<TipAnimationTrigger> ();
	}
	
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
		downJump.renderer.enabled = b;
	}
}
