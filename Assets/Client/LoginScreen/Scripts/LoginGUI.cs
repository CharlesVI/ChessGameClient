using UnityEngine;
using System.Collections;
/// <summary>
/// This script will simpily allow a player to identify himself with a login name. 
/// Eventually this will include a password and a check to the login server.
/// This script will allow the player to devolop a profile that will contain individual statistics and settings. 
/// </summary>

public class LoginGUI : MonoBehaviour {

    //Varibles Start ---------------------------------------------------------------------------------------

    string playerName = "";

    //The level designated to be the next step after login.
    public string levelToLoad;

    //Scale Varibles
    float rx;
    float ry;

    float native_width = 1920.0f;
    float native_height = 1080.0f;

    Vector3 scale; 


    //Varibles End -----------------------------------------------------------------------------------------

	// Use this for initialization
	void Start () 
    {
        //Load the last used playerName from registry.
        //If the playerName is blank then use "Player"
        //as a default name.

        playerName = PlayerPrefs.GetString("playerName");

        if (playerName == "")
        {
            playerName = "Player";
        }


        float rx = Screen.width / native_width;
        float ry = Screen.height / native_height;
        Vector3 scale = new Vector3(rx, ry, 1);
	}
	
	// Update is called once per frame
	void Update () 
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            ConnectButton();
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
	}
    void OnGUI()
    {

        //Setup Scaleing 

        //GUI.matrix = Matrix4x4.TRS(Vector3.zero, Quaternion.identity, scale); 


        GUILayout.Label("Enter your player name");

        GUI.SetNextControlName("PlayerName");
        playerName = GUILayout.TextField(playerName);


        if (GUILayout.Button("Connect", GUILayout.Height(25)))
        {
            ConnectButton();
        }
        if (GUILayout.Button("Quit", GUILayout.Height(25)))
        {
            Application.Quit();
        }
        bool focused = false;
        if (focused == false)
        {
            if (GUI.GetNameOfFocusedControl() == string.Empty)
            {
                GUI.FocusControl("PlayerName");
            }
            focused = true;
        }
        
    }
    void ConnectButton()
    {
        //Ensure that the player cannot join without a name. 
        if (playerName == "")
        {
            playerName = "Player";
        }

        //if the player has a name than he may join the server.
        if (playerName != "")
        {
            PlayerPrefs.SetString("playerName", playerName);

            Application.LoadLevel(levelToLoad);
        }
    }
}
