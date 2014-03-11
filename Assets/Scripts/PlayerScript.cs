using UnityEngine;
using System.Collections;

public class PlayerScript : MonoBehaviour {

	public int damage = 1;
	public bool isHitting = false;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerStay2D(Collider2D other)
	{
		if(other.gameObject.tag == "Enemy" && Input.GetKeyDown ("r"))
		{
			isHitting = true;
		}
		else
		{
			isHitting = false;
		}

	}
}

