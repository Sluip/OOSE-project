using UnityEngine;
using System.Collections;

public class MenuScript : MonoBehaviour {

	public Texture2D buttonTexture;
	public Texture2D buttonTexture2;
	public Texture2D buttonTexture3;
 	private bool isPaused;
	public int buttonWidth;
	public int buttonHeight;

	// Use this for initialization
	void Start () {

		isPaused = false;
	}
	
	// Update is called once per frame
	void Update () {

		if(Input.GetButtonDown ("Escape"))
		{
			if(!isPaused)
			{
				Time.timeScale = 0f;
				isPaused = !isPaused;
			}
			
			else if (isPaused)
			{
				Time.timeScale = 1f;
				isPaused = !isPaused;
			}
		}
	}

	void OnGUI()
	{
		if(isPaused) {

			if(GUI.Button (new Rect ((Screen.width/2 - buttonWidth/2), (Screen.height/2 - buttonHeight/2) - 100, buttonWidth, buttonHeight), buttonTexture)) {

				Time.timeScale = 1f;
				isPaused = !isPaused;
			}
			if(GUI.Button (new Rect ((Screen.width/2 - buttonWidth/2), (Screen.height/2 - buttonHeight/2), buttonWidth, buttonHeight), buttonTexture2)) {

				Application.LoadLevel(Application.loadedLevel);
				Time.timeScale = 1f;
			}
			if(GUI.Button (new Rect ((Screen.width/2 - buttonWidth/2), (Screen.height/2 - buttonHeight/2) + 100, buttonWidth, buttonHeight), buttonTexture3)) {

				Application.Quit();
			}
		}
	}
}
