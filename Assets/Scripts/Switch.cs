using UnityEngine;
using System.Collections;

//We use this class to control the interactable switches
public class Switch : MonoBehaviour
{
	public GameObject button;
	private GameObject[] doors;

	private Animator anim;

	void Start ()
	{
		//Finding the components needed for later in the script
		anim = button.GetComponent<Animator>();
		doors = GameObject.FindGameObjectsWithTag("Door");
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
		}
	}
}
