using UnityEngine;
using System.Collections;

public class WinScript : MonoBehaviour {

	public GameObject winScreen;

	// Use this for initialization
	void Start () {
	
		winScreen.renderer.enabled = false;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter2D(Collider2D other)
	{
		if(other.gameObject.tag == "Player")
		{
			Destroy (other.gameObject);
			winScreen.renderer.enabled = true;
			Time.timeScale = 0.0f;
		}
	}
}
