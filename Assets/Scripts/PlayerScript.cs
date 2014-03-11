using UnityEngine;
using System.Collections;

public class PlayerScript : MonoBehaviour {

	public int damage = 1;
<<<<<<< HEAD
	public bool isHitting = false;
=======
	private bool isHitting = false;
>>>>>>> e0407fb9491604775191ad9ddc87bca025da1295

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
<<<<<<< HEAD
	
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

=======

		if(Input.GetKeyDown("r"))
		{
			isHitting = true;
		}
		else{
			isHitting = false;
		}
	}

	//Melee system
	void OnTriggerStay2D(Collider2D other)
	{
		if(other.gameObject.tag == "Enemy" && isHitting)
		{
			other.gameObject.GetComponent<EnemyScript>().Hurt(damage);
		}
>>>>>>> e0407fb9491604775191ad9ddc87bca025da1295
	}
}

