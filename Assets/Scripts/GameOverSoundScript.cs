using UnityEngine;
using System.Collections;

public class GameOverSoundScript : MonoBehaviour {

	private Transform gameOver;

	// Use this for initialization
	void Start () {
	
		for(int i = 0 ; i < transform.childCount ; i++)
		{
			if (transform.GetChild(i).name == "GameOver")
				gameOver = transform.GetChild(i);
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void GameOverSound()
	{
		gameOver.audio.Play ();
	}
}
