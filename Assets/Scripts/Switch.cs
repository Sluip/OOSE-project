using UnityEngine;
using System.Collections;

//We use this class to control the interactable switches
public class Switch : MonoBehaviour
{
	public GameObject button;
	private GameObject[] doors;
	private Transform switchSound;
	private bool soundPlayed;

	private Animator anim;

	void Start ()
	{
		//Finding the components needed for later in the script
		anim = button.GetComponent<Animator>();
		doors = GameObject.FindGameObjectsWithTag("Door");
		soundPlayed = false;

		for(int i = 0 ; i < transform.childCount ; i++)
		{
			if (transform.GetChild(i).name == "Sound")
				switchSound = transform.GetChild(i);
		}
	}
	//Trigger to activates switches and destroy the respctive door
	void OnTriggerEnter2D (Collider2D o)
	{
		if (o.gameObject.tag == "Player") {
			anim.SetTrigger("switch");
			for(int i = 0 ; i < doors.Length ; i++)
			{
			Destroy (doors[i]);
			}

			if(!soundPlayed)
			{
				for (int i = 0 ; i < switchSound.transform.childCount ; ++i)
				{
					switchSound.transform.GetChild(i).audio.Play();
				}

				soundPlayed = true;
			}
		}
	}
}
