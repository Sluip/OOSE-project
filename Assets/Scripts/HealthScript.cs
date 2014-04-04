using UnityEngine;
using System.Collections;

public class HealthScript : MonoBehaviour {

	private float HP = 10f;
	public Transform healthBar;
	public bool something;

	// Use this for initialization
	void Start () {
			
	}
	
	// Update is called once per frame
	void Update () {

		if(HP <= 0) {

			something = true;
			Destroy(gameObject);
		}
	}

	public void Hit (int damage)
	{
		HP -= damage;

		// Scale health bar down
		Vector3 temp = healthBar.localScale;
		temp.z += 0.01f;
		healthBar.localScale = temp;
	}
}
