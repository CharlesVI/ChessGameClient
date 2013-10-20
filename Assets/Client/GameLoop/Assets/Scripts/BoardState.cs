using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// This Script will be used to store the board state retrieved from the server.
/// This will be a glorified array. That will dictate many behaiors and the values of many inputs.
/// </summary>

public class BoardState : MonoBehaviour {

    //VARIBLES START *******************************************************************

    //This array will store the info about game peices.
    //The server should provide all this info.
    public GamePiece[,] gamePiece;

    //This Array will hold the info the tiles need. I have the standard Light and dark tiles drawn underneath. The current Idea is to just add textures over top.
    //However this will possibly be undone and this array will be responsible for light and dark tile drawing as well.
    public GameBoard[,] gameBoard;


    public bool boardStateReady = false;

    //This non existant array will store extra crap about the game board that is not important to the server.
    //GameBoard[,] gameBoard;


    //Varibles END *********************************************************************

	// Use this for initialization
	void Start () 
    {
       gamePiece = new GamePiece[8, 8];
       gameBoard = new GameBoard[8, 8];

        //Initializing GameBoard
       for (int x = 0; x < 8; x++) for (int y = 0; y < 8; y++)
       {
           gameBoard[x,y] = new GameBoard(false, false, false, false);
       }

       networkView.RPC("ReadyToPlay", RPCMode.Server, Network.player);

	}

    // Update is called once per frame
    void Update()
    {
	
	}

    [RPC]
    void ReadyToPlay(NetworkPlayer nPlayer)
    {      
    }

    [RPC]
    void ReadyToDraw()
    {
        Debug.Log("ready to draw called");
        boardStateReady = true;
    }

    [RPC]
    void SetUpBoard(int team, int x, int y, int type, int captured, int moved)
    {
        gamePiece[x, y] = new GamePiece(team, type, captured, moved);
    }
     
    [RPC]
    void UpdateGamePiece(int x, int y, int team, int type, int captured, int moved)
    {
        gamePiece[x, y] = new GamePiece(team, type, captured, moved);
        //set move confirmed to false.
    }


}
