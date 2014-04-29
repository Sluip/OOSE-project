using UnityEngine;
using System.Collections;

public class HealthBarDestroyer : MonoBehaviour {

	private int length;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

	}

	public int DestroyHealthBar(int damage)
	{		if (damage >= 1) {
				if(transform.childCount > 0)
				Destroy(transform.GetChild(transform.childCount-damage).gameObject);
				return DestroyHealthBar (damage-1);
			}
		else {
			return 0;
		}
	}
}
