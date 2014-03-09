using UnityEngine;
using System.Collections;

public class DialogueBoxScript : MonoBehaviour {
	
	public Transform sign;

	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter2D(Collider2D other)
	{
		Debug.Log ("gay");
		sign.gameObject.SetActive(true);
	}
}
