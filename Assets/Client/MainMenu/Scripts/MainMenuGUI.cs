using UnityEngine;
using System.Collections;

/// <summary>
/// Main menu GUI.
/// 
/// This Script will be responsible for allowing our player to chose his gamemode. 
/// FindOpponent, Settings, Help, Exit. are the currently planned functions.
/// This Menu Will be loaded immediatly after the loginscreen on start up. 
/// 
/// </summary>

public class MainMenuGUI : MonoBehaviour {
	
	
	//Varibles Start --------------------------------------
	
	//These Varibles Will Define the button Layout of our GUI
	public float buttonLeft = 10;
	public float buttonTop = 300;
	public float buttonSpace = 10;
	public float buttonWidth = 400;
	public float buttonHeight = 180;
	
	//This is used in the scale script to make the UI work for all resolutions.
	float native_width = 1920.0f;
    float native_height = 1080.0f;
	
	Rect fullScreen = new Rect(0,0,1920,1080);
	public Texture2D backgroundImage;
	
	
	GUIStyle menuStyle = new GUIStyle();

    //These Varibles are used for the get function stuff so I can tell the game I am looking to find an opponent and to search for one online
    GameObject networkManager;
    FindOpponent findOpponent;

	
	
	//Varibles End ---------------------------------------
	// Use this for initialization
    void Awake()
    {

    }
	void Start () 
	{
		menuStyle.fontSize = 60;
        
        networkManager = GameObject.Find("NetworkManager");

        findOpponent = networkManager.GetComponent<FindOpponent>();


	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	void OnGUI()
	{
		//set up scaling
        //For Some reason when I pull the two formulas into the start function this thing fails to work properly. No Idea why but I think it has to do with the scale varible.
        float rx = Screen.width / native_width;
        float ry = Screen.height / native_height;
		Vector3 scale = new Vector3(rx, ry, 1);
		GUI.matrix = Matrix4x4.TRS (Vector3.zero, Quaternion.identity, scale); 
	
	
    	//now create your GUI normally, as if you were in your native resolution
    	//The GUI.matrix will scale everything automatically.
		
		GUI.DrawTexture(fullScreen, backgroundImage);
		
		if(GUI.Button(new Rect(buttonLeft, buttonTop, buttonWidth, buttonHeight), "Find Opponent", menuStyle))
		{
            Debug.Log("Button pressed");
			findOpponent.iWantToPlay = true;	
		}
		if(GUI.Button(new Rect(buttonLeft, buttonTop+ (buttonSpace + buttonHeight), buttonWidth, buttonHeight), "Settings", menuStyle))
		{
			print ("Do settings Stuff");
		}
		if(GUI.Button(new Rect (buttonLeft,  buttonTop+ (buttonSpace + buttonHeight)*2, buttonWidth, buttonHeight), "Learn to Play", menuStyle))
		{
			
		}
		if(GUI.Button(new Rect(buttonLeft, buttonTop+ (buttonSpace + buttonHeight)*3, buttonWidth, buttonHeight), "Quit", menuStyle))
		{
			Application.Quit();
		}
	}
}
