using UnityEngine;
using System.Collections;

public class EnemyScript : MonoBehaviour {

	public int HP = 10;
	public Transform player;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

		if(HP <= 0)
		{
			Destroy(gameObject);
		}
	
	}

	void OnTriggerStay2D(Collider2D other)
	{
		if (other.gameObject.tag == "Player" && player.gameObject.GetComponent<PlayerScript>().isHitting)
		{
			HP -= player.gameObject.GetComponent<PlayerScript>().damage;
			Debug.Log (HP);
		}
	}
}