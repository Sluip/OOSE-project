using UnityEngine;
using System.Collections;

public class Switch2 : MonoBehaviour
{
	public GameObject button;
	private Animator anim;
	private GameObject[] doors;
	
	// Use this for initialization
	void Start ()
	{
		anim = button.GetComponent<Animator>();
		doors = GameObject.FindGameObjectsWithTag("Door2");
		
	}
	
	// Update is called once per frame
	void Update ()
	{
		
	}
	
	void OnTriggerEnter2D (Collider2D o)
	{
		if (o.gameObject.tag == "Player") {
			anim.SetTrigger("switch");
			
			for(int i = 0 ; i < doors.Length ; i++)
				Destroy (doors[i]);
		}
	}
	
}
