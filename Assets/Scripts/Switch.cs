using UnityEngine;
using System.Collections;

public class Switch : MonoBehaviour
{

		private GameObject Door;

		// Use this for initialization
		void Start ()
		{
	
				Door = GameObject.Find ("Door1");
	
		}
	
		// Update is called once per frame
		void Update ()
		{
	
		}
	
		void OnTriggerEnter2D (Collider2D o)
		{
				if (o.gameObject.tag == "Player") {
						Debug.Log ("OK");
						Destroy (Door);
				}
		}
}
