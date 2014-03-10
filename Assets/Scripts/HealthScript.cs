using UnityEngine;
using System.Collections;

public class HealthScript : MonoBehaviour {

public static int health = 100;
public float range;
public Transform player;
public static bool inRange = false;

// Use this for initialization
void Start () {

}

// Update is called once per frame
void Update () {

if(Vector2.Distance(transform.position, player.position)<range)
{
inRange = true;
Debug.Log (health);
}
else
{
inRange = false;
}
}
}