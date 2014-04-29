using UnityEngine;
using System.Collections;

//We use this class to control the tip for the jumping, see DoubleJumpTipScript for further details
public class JumpTipScript : MonoBehaviour {

	private GameObject tips;
	public GameObject spaceBar;

	private TipAnimationTrigger tipAnimationTrigger;

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
		spaceBar.renderer.enabled = b;
	}
}
