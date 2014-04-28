using UnityEngine;
using System.Collections;

public class JumpTipScript : MonoBehaviour {

	private bool spaceTip = false;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter2D(Collider2D other)
	{
		if(other.gameObject.tag == "Player")
			spaceTip = true;
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
