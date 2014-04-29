using UnityEngine;
using System.Collections;

//We use this class to display the appropriate tip depending on which trigger you activate
public class TipAnimationTrigger : MonoBehaviour {

    private Animator anim;
    private bool tip;

	void Awake() 
	{
		//We run through the different tips to set them active but disable their visibillity
		for (int i = 0; i < this.transform.GetChildCount(); ++i)
		{
			this.transform.GetChild(i).gameObject.SetActive(true);
			this.transform.GetChild(i).gameObject.renderer.enabled = false;
		}
	}

	void Start () 
	{
        anim = gameObject.GetComponent<Animator>();
		tip = false;
	}
	
	// Update is called once per frame
	void Update () 
	{
        anim.SetBool("Tip", tip);
	}

    public void Tip(bool a) 
    {
		tip = a;
    }
}
