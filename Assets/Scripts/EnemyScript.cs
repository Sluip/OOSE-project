using UnityEngine;
using System.Collections;

public class EnemyScript : MonoBehaviour {

	public int HP = 10;

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

	public void Hurt(int damage)
	{
		HP -= damage;
		Debug.Log (HP);
	}
}