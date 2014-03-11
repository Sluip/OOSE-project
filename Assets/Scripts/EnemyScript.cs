using UnityEngine;
using System.Collections;

public class EnemyScript : MonoBehaviour {

	public int HP = 10;
<<<<<<< HEAD
	public Transform player;

	// Use this for initialization
	void Start () {
	
=======

	// Use this for initialization
	void Start () {
		
>>>>>>> e0407fb9491604775191ad9ddc87bca025da1295
	}
	
	// Update is called once per frame
	void Update () {

		if(HP <= 0)
		{
			Destroy(gameObject);
		}
	
	}

<<<<<<< HEAD
	void OnTriggerStay2D(Collider2D other)
	{
		if (other.gameObject.tag == "Player" && player.gameObject.GetComponent<PlayerScript>().isHitting)
		{
			HP -= player.gameObject.GetComponent<PlayerScript>().damage;
			Debug.Log (HP);
		}
=======
	public void Hurt(int damage)
	{
		HP -= damage;
		Debug.Log (HP);
>>>>>>> e0407fb9491604775191ad9ddc87bca025da1295
	}
}