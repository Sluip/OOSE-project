using UnityEngine;
using System.Collections;

public class EnemyAudioScript : MonoBehaviour {

	private Transform alarm;
	private Transform dying;
	private Transform gunShot;
	private Transform reload;
	private Transform cartridges;

	// Use this for initialization
	void Start () {
	
		for(int i = 0 ; i < transform.childCount ; i++)
		{
			if (transform.GetChild(i).name == "Alarm")
				alarm = transform.GetChild(i);

			else if (transform.GetChild(i).name == "Dying")
				dying = transform.GetChild(i);

			else if (transform.GetChild(i).name == "GunShot")
				gunShot = transform.GetChild(i);

			else if (transform.GetChild(i).name == "Reload")
				reload = transform.GetChild(i);

			else if (transform.GetChild(i).name == "Cartridges")
				cartridges = transform.GetChild(i);
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void AlarmSound()
	{
		alarm.audio.Play ();
	}

	public void DyingSound()
	{
		dying.audio.Play ();
	}

	public void GunshotSound()
	{
		gunShot.audio.Play ();
	}

	public void ReloadSound()
	{
		reload.audio.Play ();
	}
}
