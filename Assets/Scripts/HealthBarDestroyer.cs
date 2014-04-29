using UnityEngine;
using System.Collections;

//We use this class to control the players healthbar
public class HealthBarDestroyer : MonoBehaviour {

	private int length;
	//Calls itself recursively if damage argument is 1 or above, otherwise it will return 0 as to not destroy more healthbar than required
	//For every time it is called, it will destroy one child of healthbar, which corresponds a point of damage
	public int DestroyHealthBar(int damage)
	{		if (damage >= 1) 
		{
			if(transform.childCount > 0)
			Destroy(transform.GetChild(transform.childCount-damage).gameObject);
			return DestroyHealthBar (damage-1);
		}
		else
		{
			return 0;
		}
	}
}
