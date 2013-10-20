using UnityEngine;
using System.Collections;
using System.Collections.Generic;


/// <summary>
/// Find opponent.
/// This script will attempt to automaticly pair the player with another player so that they may play a match.
/// Once the Pair has been made the script will send the players details to the server so he can be added to the list.
/// </summary>
public class FindOpponent : MonoBehaviour 
{
	//Varibles Start ---------------------------------------------------------------------------------------
	public string GUID = "";
	public int msPort = 23466;
	public string msIP = "127.0.0.1";
	public string levelToLoad ="";
	
	//Toggle me when the find opponent button is pressed.
	public bool iWantToPlay = false;

    //These will be used to manualy set the load state when a opponent is found. 
    GameObject gameManager;
    LoadingScreen loadingScreen;

    string playerName;

    List<PlayerDataClass> playerList = new List<PlayerDataClass>();
	//Varibles End -----------------------------------------------------------------------------------------
	
    // Use this for initialization
	void Awake() 
	{

	}

	
	void Start () 
	{
        gameManager = GameObject.Find("GameManager");
        loadingScreen = gameManager.GetComponent<LoadingScreen>();
        playerName = PlayerPrefs.GetString("playerName");
	
	}
	
	// Update is called once per frame
	void Update () 
	{
		if(iWantToPlay == true)
		{

            
            //TODO Set I a "Searching for OPPONENT MESSAGE"


            Debug.Log("iwanttoplay set true");
			MasterServer.port = msPort;
			MasterServer.ipAddress = msIP;
			Network.natFacilitatorIP = "127.0.0.1";
			Network.natFacilitatorPort = 10001;
		
			
			MasterServer.RequestHostList(GUID);
			HostData[] data = MasterServer.PollHostList();
			// Go through all the hosts in the host list to find a game with a open slot. then we will connect to the game.	
			foreach(HostData element in data)
			{	
				//If server is not full. Connect!
				if(element.playerLimit != element.connectedPlayers)
				{
					Network.Connect(element);
					iWantToPlay = false;
					break;
				}
			}
			//todo
			//If they dont find anyone at this point We are probally going to have to do something to make this loop?
		}
	}
	
	void OnConnectedToServer()
	{
        //This RPC will tell the server His Network ID and his Choosen name. These will be added to the list.
        networkView.RPC("AddPlayerToTheList", RPCMode.Server, Network.player, playerName);
        Debug.Log("Sent RPC to add player to the list");


       
        //Lets save the loading for later. First we want to gain a actuall Opponent
        //loadingScreen.loading = true;
        //Application.LoadLevel(levelToLoad);
	}

   
    //This RPC will be used to set the Network ID and Choosen Name of the player to the servers list. 
    [RPC]
    void AddPlayerToTheList(NetworkPlayer nPlayer, string pName)
    {
        PlayerDataClass player = new PlayerDataClass();

        player.networkPlayer = nPlayer;

        player.playerName = pName;

        playerList.Add(player);
    } 
	[RPC]
	void GameStart()
	{
		Debug.Log ("gamestart called on client");
		Application.LoadLevel("_GameLoop");
	}
}
