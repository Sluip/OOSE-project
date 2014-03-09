using UnityEngine;
using System.Collections;

public class HealthScript : MonoBehaviour {

	public int health = 100;
	public bool isEnemy = true;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

		Debug.Log (health);
	
	}

	public void Damage(int damageCount)
	{
		health -= damageCount;

		if(health <= 0)
		{
			Destroy(gameObject);
		}
	}
}
