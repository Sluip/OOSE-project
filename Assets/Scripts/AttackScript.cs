using UnityEngine;
using System.Collections;

public class AttackScript : MonoBehaviour {
	
	private int damageCount = 10;
	public GameObject enemy;
	
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
		if (HealthScript.inRange && Input.GetKeyDown ("r"))
		{
			Debug.Log (HealthScript.health);
			HealthScript.health -= damageCount;
			
			if(HealthScript.health <= 0)
			{
				Destroy(enemy.gameObject);
			}
		}
		
	}
}