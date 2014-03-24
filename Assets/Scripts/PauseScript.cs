using UnityEngine;
using System.Collections;

public class PauseScript : MonoBehaviour {

	private bool isPaused;

	// Use this for initialization
	void Start () {

		isPaused = false;
	}

	void Awake () 
	{
		gameObject.renderer.enabled = false;
	}
	
	// Update is called once per frame
	void Update () {

		if(Input.GetButtonDown ("Escape"))
		{
			if(!isPaused)
			{
				Time.timeScale = 0f;
				gameObject.renderer.enabled = true;
				isPaused = !isPaused;
			}

			else if (isPaused)
			{
				Time.timeScale = 1f;
				isPaused = !isPaused;
				gameObject.renderer.enabled = false;
			}
		}
	}
}
