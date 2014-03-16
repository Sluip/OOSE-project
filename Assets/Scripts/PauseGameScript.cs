using UnityEngine;
using System.Collections;

public class PauseGameScript : MonoBehaviour {

	public bool paused;	

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

		if (Input.GetKeyDown ("p"))
			OnApplicationPause (true);
	}

	void OnApplicationPause(bool pauseStatus)
	{
		paused = pauseStatus;
	}
}
