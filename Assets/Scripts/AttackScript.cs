using UnityEngine;
using System.Collections;

public class AttackScript : MonoBehaviour {

	public int damage = 10;
	public RangeDetectionScript rangeReference;
	public HealthScript healthReference;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

		if (rangeReference.inRange() && Input.GetKeyDown ("space"))
		{
			healthReference.Damage(damage);
		}
	
	}
}
