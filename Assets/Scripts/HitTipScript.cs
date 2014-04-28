using UnityEngine;
using System.Collections;

public class HitTipScript : MonoBehaviour {
	
	private TipAnimationTrigger tipAnimationTrigger;
	private GameObject tips;
	public GameObject hitKey;
	
	// Use this for initialization
	void Start () {
		
		tips = GameObject.FindWithTag("Tips");
		tipAnimationTrigger = tips.GetComponent<TipAnimationTrigger> ();
	}
	
	// Update is called once per frame
	void Update () {
		
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
		hitKey.renderer.enabled = b;
	}
}
