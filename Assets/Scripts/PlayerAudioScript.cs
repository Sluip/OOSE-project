using UnityEngine;
using System.Collections;

public class PlayerAudioScript : MonoBehaviour {

	private Transform punch;
	private Transform swing;
	private Transform stab;
	private Transform run;
	private Transform runGrass;
	
	// Use this for initialization
	void Start () {
		
		for(int i = 0 ; i < transform.childCount ; i++)
		{
			if (transform.GetChild(i).name == "Punch")
				punch = transform.GetChild(i);
			
			else if (transform.GetChild(i).name == "Swing")
				swing = transform.GetChild(i);
			
			else if (transform.GetChild(i).name == "Stab")
				stab = transform.GetChild(i);
			
			else if (transform.GetChild(i).name == "Run")
				run = transform.GetChild(i);

			else if (transform.GetChild(i).name == "RunGrass")
				runGrass = transform.GetChild(i);
		}
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	
	public void PunchSound()
	{
		punch.audio.Play ();
	}
	
	public void SwingSound()
	{
		swing.audio.Play ();
	}
	
	public void StabSound()
	{
		stab.audio.Play ();
	}

	public void RunSound()
	{
		run.audio.Play ();
	}

	public void RunGrassSound()
	{
		runGrass.audio.Play ();
	}
}
