using UnityEngine;
using System.Collections;

//We use this class to control the pause menu
public class MenuScript : MonoBehaviour
{

    public Texture2D buttonTexture, buttonTexture2, buttonTexture3;

    private bool isPaused;
    public int buttonWidth, buttonHeight;

    private HealthScript healthScript;
    public Transform player;

    void Start()
    {
        //Starts in an unpaused state
        isPaused = false;
        healthScript = player.GetComponent<HealthScript>();
    }
    // Update is called once per frame
    void Update()
    {
        //If player is dead or presses escape, pause the game, can't unpause if player is dead
        if (Input.GetButtonDown("Escape") || healthScript.IsDead())
        {

            if (!isPaused)
            {
                Time.timeScale = 0f;
                isPaused = !isPaused;
            }

            else if (isPaused && !healthScript.IsDead())
            {
                Time.timeScale = 1f;
                isPaused = !isPaused;
            }
        }
    }

    void OnGUI()
    {
        //Brings up the menu during pause
        if (isPaused)
        {

            if (player != null)
            {
                //Resume button
                if (GUI.Button(new Rect((Screen.width / 2 - buttonWidth / 2), (Screen.height / 2 - buttonHeight / 2) - 100, buttonWidth, buttonHeight), buttonTexture))
                {
                    Time.timeScale = 1f;
                    isPaused = !isPaused;
                }
            }
            //Restart button
            if (GUI.Button(new Rect((Screen.width / 2 - buttonWidth / 2), (Screen.height / 2 - buttonHeight / 2), buttonWidth, buttonHeight), buttonTexture2))
            {
                Application.LoadLevel(Application.loadedLevel);
                Time.timeScale = 1f;
            }
            //Quit Game button
            if (GUI.Button(new Rect((Screen.width / 2 - buttonWidth / 2), (Screen.height / 2 - buttonHeight / 2) + 100, buttonWidth, buttonHeight), buttonTexture3))
            {
                Application.Quit();
            }
        }
    }
}
