using UnityEngine;
using System.Collections;

public class CameraScript : MonoBehaviour {
	public float moveSpeed = 0.20f;
	private Vector3 v = Vector3.zero;
	public Transform player;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (player) {
						Vector3 point = camera.WorldToViewportPoint (player.position);
						Vector3 delta = player.position - camera.ViewportToWorldPoint (new Vector3 (0.5f, 0.5f, point.z));
						Vector3 endpoint = transform.position + delta;
			if (point.y > 0.7f) {
				endpoint.y = 0.0f;

			}
		
						transform.position = Vector3.SmoothDamp (transform.position, endpoint, ref v, moveSpeed);
				}
	
	}
}
