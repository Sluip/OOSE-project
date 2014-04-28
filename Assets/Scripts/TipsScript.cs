using UnityEngine;
using System.Collections;

public class TipsScript : MonoBehaviour {

	private Animator anim;
	private bool tip;
	public GameObject WASD;
	public GameObject space;
	public GameObject spaceColl;
	private JumpTipScript jumpTip;
	private GameObject tipSpace;

	// Use this for initialization
	void Start () {

		tipSpace = GameObject.FindWithTag("TipSpace");
		jumpTip = tipSpace.GetComponent<JumpTipScript> ();
		anim = gameObject.GetComponent<Animator> ();
		tip = true;
		WASD.SetActive(true);

	}
	
	// Update is called once per frame
	void Update () {

		anim.SetBool ("Tip", tip);

		if(Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.W))
		{
			tip = false;
			WASD.SetActive(false);
		}

		if(jumpTip.SpaceTip())
		{
			jumpTip.SpaceTipDisable();
			tip = true;
			space.SetActive (true);
		}

		if(Input.GetButtonDown("Jump"))
		{
			tip = false;
			space.SetActive(false);
			tipSpace.SetActive(false);
		}	
			
	}
}
