using UnityEngine;
using System.Collections;

public class PlayerScript : MonoBehaviour {

	public int damage = 1;
	private bool isHitting = false;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

		if(Input.GetKeyDown("r"))
		{
			isHitting = true;
		}
	}

	// Melee system
	void OnTriggerStay2D(Collider2D other)
	{
		if(other.gameObject.tag == "Enemy" && isHitting)
		{
			other.gameObject.GetComponent<EnemyScript>().Hurt(damage);
			isHitting = false;
		}
	}
}

