using UnityEngine;
using System.Collections;

public class EnemyScript : MonoBehaviour {
		
	public int HP = 10;
	public Transform healthBar;

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {

		// Death
		if(HP <= 0)
		{
			Destroy(gameObject);
		}

		// Change color of health bar to yellow
		if(HP <= 5)
		{
			healthBar.transform.renderer.material.color = Color.yellow;
		}

		// Change color of health bar to red
		if(HP < 3)
		{
			healthBar.transform.renderer.material.color = Color.red;
		}
	}

	public void Hurt(int damage)
	{
		HP -= damage;
		Debug.Log (HP);

		// Scale health bar down
		Vector3 temp = healthBar.localScale;
		temp.z += 0.01f;
		healthBar.localScale = temp;
	}
}