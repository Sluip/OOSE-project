using UnityEngine;
using System.Collections;

public class PlayerAudioScript : MonoBehaviour {

	private Transform punch;
	private Transform swing;
	private Transform stab;
	private Transform run;
	private Transform sprint;
	
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

			else if (transform.GetChild(i).name == "Sprint")
				sprint = transform.GetChild(i);
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

	public void RunSoundStop()
	{
		run.audio.Stop ();
	}

	public bool IsRunSoundPlaying()
	{
		if(run.audio.isPlaying)
			return true;
		else
			return false;
	}

	public void SprintSound()
	{
		sprint.audio.Play ();
	}

	public void SprintSoundStop()
	{
		sprint.audio.Stop ();
	}

	public bool IsSprintSoundPlaying()
	{
		if(sprint.audio.isPlaying)
			return true;
		else
			return false;
	}
}
