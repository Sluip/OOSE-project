using UnityEngine;
using System.Collections;

public class TipAnimationTrigger : MonoBehaviour {

    private Animator anim;
    private bool tip;

	// Use this for initialization
	void Awake() {

		for (int i = 0; i < this.transform.GetChildCount(); ++i)
		{
			this.transform.GetChild(i).gameObject.SetActive(true);
			this.transform.GetChild (i).gameObject.renderer.enabled = false;
		}
	}

	void Start () {

        anim = gameObject.GetComponent<Animator>();
		tip = false;
	}
	
	// Update is called once per frame
	void Update () {

        anim.SetBool("Tip", tip);
	}

    public void Tip(bool a) 
    {
		tip = a;
    }
}
