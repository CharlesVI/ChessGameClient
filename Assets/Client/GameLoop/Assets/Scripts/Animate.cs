using UnityEngine;
using System.Collections;
/// <summary>
/// Animate.
/// This Script will control the animation logic.
/// The Server send the animator the needed info to determin what to draw.
/// 
/// </summary>

public class Animate : MonoBehaviour
{
	
	//Varibles Start-------------------------------------------------------

    public GameObject lightTile;
    public GameObject darkTile;

    public GameObject pawn;
    public GameObject rook;
    public GameObject knight;
    public GameObject bishop;
    public GameObject queen;
    public GameObject king;

    bool setupDone = false;

	public bool p1Captured = false;
	public bool p2Captured = false;

    public bool readyToAnimateOne = false;
    public bool readyToAnimateTwo = false;
	
	public Vector3 p1Origin;
	public Vector3 p1Destination;
	
	public Vector3 p2Origin;
	public Vector3 p2Destination;

    public bool enPassantOne;
    public bool enPassantTwo;

    public Vector3 enPassantPositionOne;
    public Vector3 enPassantPositionTwo;

    public bool threatCheckReady;

    public bool p1Castle;
    public Vector3 p1CastleOrigin;
    public Vector3 p1CastleDestination;

    public bool p2Castle;
    public Vector3 p2CastleOrigin;
    public Vector3 p2CastleDestination;

    public bool promote;
    public Vector3 promoteLocationP1;
    public bool promoteReadyP1;
    public bool spawnPromotionP1;
    int promoteXP1;
    int promoteYP1;

    public Vector3 promoteLocationP2;
    public bool promoteReadyP2;
    public bool spawnPromotionP2;
    int promoteXP2;
    int promoteYP2;

    GameObject networkManager;
    BoardState boardState;
    GetInput getInput;
	
	//Varibles End---------------------------------------------------------

    //NOTE: Looks like this will run before the board is fully filled causing null refrences. This does not crash the game.
    //NOTE: Currently I am leaving it as is. This should be fixed later.

	// Use this for initialization
	void Start () 
    {

        networkManager = GameObject.Find("NetworkManager");
        boardState = networkManager.GetComponent<BoardState>();
        getInput = networkManager.GetComponent<GetInput>();

        threatCheckReady = true; // This is just so it will call on startup (threatened tiles)

        //Here I will go ahead and draw up the game board. 
        //Each tile will be drawn seprately to allow modification and skins.
        for (int i = 0; i < 8; i++) for (int j = 0; j < 8; j++)
        {
            if (i % 2 == 0)
            {
                if (j % 2 == 0)
                {
                    Instantiate(darkTile, new Vector3(i * 10, 0, j * 10), Quaternion.identity);
                }
                else
                {
                    Instantiate(lightTile, new Vector3(i * 10, 0, j * 10), Quaternion.identity);
                }
            }
            else
            {
                if (j % 2 == 0)
                {
                    Instantiate(lightTile, new Vector3(i * 10, 0, j * 10), Quaternion.identity);
                }
                else
                {
                    Instantiate(darkTile, new Vector3(i * 10, 0, j * 10), Quaternion.identity);
                }
            }//Draw the tile
        }//Draw Loop
	}

    // Update is called once per frame
    void Update()
    {

        #region PieceSetup
        if (boardState.boardStateReady == true && setupDone == false)
        {
            for (int x = 0; x < 8; x++) for (int y = 0; y < 8; y++)
            {

                if (boardState.gamePiece[x, y].type == 1)
                {
                    Instantiate(pawn, new Vector3(x * 10, 0.5f, y * 10), Quaternion.identity);
                }
                if (boardState.gamePiece[x, y].type == 2)
                {
                    Instantiate(rook, new Vector3(x * 10, 0.5f, y * 10), Quaternion.identity);
                }
                if (boardState.gamePiece[x, y].type == 3)
                {
                    Instantiate(knight, new Vector3(x * 10, 0.5f, y * 10), Quaternion.identity);
                }
                if (boardState.gamePiece[x, y].type == 4)
                {
                    Instantiate(bishop, new Vector3(x * 10, 0.5f, y * 10), Quaternion.identity);
                }
                if (boardState.gamePiece[x, y].type == 5)
                {
                    Instantiate(queen, new Vector3(x * 10, 0.5f, y * 10), Quaternion.identity);
                }
                if (boardState.gamePiece[x, y].type == 6)
                {
                    Instantiate(king, new Vector3(x * 10, 0.5f, y * 10), Quaternion.identity);
                }

            }//Draw Pieces  

            setupDone = true;

        }//If statement
        #endregion

        if (spawnPromotionP1)
        {
            Debug.Log("Spawn Promotion Ran");

            int x = promoteXP1;
            int y = promoteYP1;

            if (boardState.gamePiece[x, y].type == 1)
            {
                Instantiate(pawn, promoteLocationP1, Quaternion.identity);
            }
            if (boardState.gamePiece[x, y].type == 2)
            {
                Instantiate(rook, promoteLocationP1, Quaternion.identity);
            }
            if (boardState.gamePiece[x, y].type == 3)
            {
                Instantiate(knight, promoteLocationP1, Quaternion.identity);
            }
            if (boardState.gamePiece[x, y].type == 4)
            {
                Instantiate(bishop, promoteLocationP1, Quaternion.identity);
            }
            if (boardState.gamePiece[x, y].type == 5)
            {
                Instantiate(queen, promoteLocationP1, Quaternion.identity);
            }

            spawnPromotionP1 = false;
        }//Promote

        if (spawnPromotionP2)
        {
            Debug.Log("Spawn Promotion Ran");

            int x = promoteXP2;
            int y = promoteYP2;

            if (boardState.gamePiece[x, y].type == 1)
            {
                Instantiate(pawn, promoteLocationP2, Quaternion.identity);
            }
            if (boardState.gamePiece[x, y].type == 2)
            {
                Instantiate(rook, promoteLocationP2, Quaternion.identity);
            }
            if (boardState.gamePiece[x, y].type == 3)
            {
                Instantiate(knight, promoteLocationP2, Quaternion.identity);
            }
            if (boardState.gamePiece[x, y].type == 4)
            {
                Instantiate(bishop, promoteLocationP2, Quaternion.identity);
            }
            if (boardState.gamePiece[x, y].type == 5)
            {
                Instantiate(queen, promoteLocationP2, Quaternion.identity);
            }

            spawnPromotionP2 = false;
        }//Promote



    }//Update

    [RPC]
    void AnimationCoordinates(int p1XOrigin, int p1YOrigin, int p1XDestination, int p1YDestination, int p1Capture,
        int p2XOrigin, int p2YOrigin, int p2XDestination, int p2YDestination, int p2Capture)
    {
        p1Origin.x = p1XOrigin * 10; p1Origin.y = 0.5f; p1Origin.z = p1YOrigin * 10;
        p1Destination.x = p1XDestination * 10; p1Destination.y = 0.5f; p1Destination.z = p1YDestination * 10;
        if (p1Capture == 1)
        {
            p1Captured = true;
        }

        p2Origin.x = p2XOrigin * 10; p2Origin.y = 0.5f; p2Origin.z = p2YOrigin * 10;
        p2Destination.x = p2XDestination * 10; p2Destination.y = 0.5f; p2Destination.z = p2YDestination * 10;

        if (p2Capture == 1)
        {
            p2Captured = true;
        }

        if (p1YOrigin == 0)
        {
            if (p1XOrigin == 0)
            { getInput.p1QueensRookMoved = true; }

            if (p1XOrigin == 7)
            { getInput.p1KingsRookMoved = true; }

            if (p1XOrigin == 4)
            { getInput.p1KingMoved = true; }
        }

        if(p2YOrigin == 7)
        {
            if (p2XOrigin == 0)
            { getInput.p2QueensRookMoved = true; }

            if (p2XOrigin == 7)
            { getInput.p2KingsRookMoved = true; }

            if (p2XOrigin == 4)
            { getInput.p2KingMoved = true; }
        }

        readyToAnimateOne = true;
        readyToAnimateTwo = true;

        getInput.p1YPreviousOrigin = p1YOrigin;
        getInput.p2YPreviousOrigin = p2YOrigin;

        threatCheckReady = true;

        Debug.Log("player 1" + getInput.p1YPreviousOrigin + "player 2 " + getInput.p2YPreviousOrigin);

        Debug.Log("Animate Coordinates RPC was called");

    }

    [RPC]
    void EnPassant(int x, int y, int player)
    {
        if(player == 1)
        {
            enPassantOne = true;
            enPassantPositionOne = new Vector3(x * 10, 0.5f, y * 10);
        }

        if (player == 2)
        {
            enPassantTwo = true;
            enPassantPositionTwo = new Vector3(x * 10, 0.5f, y * 10);
        }
    }

    [RPC]
    void Castle(int xOrigin, int yOrigin, int xDestination, int yDestination, int team)
    {
        if (team == 1)
        {
            p1CastleOrigin = new Vector3(xOrigin * 10, 0.5f, yOrigin * 10);
            p1CastleDestination = new Vector3(xDestination * 10, 0.5f, yDestination * 10);
            p1Castle = true;
        }

        if(team == 2)
        {
            p2CastleOrigin = new Vector3(xOrigin * 10, 0.5f, yOrigin * 10);
            p2CastleDestination = new Vector3(xDestination * 10, 0.5f, yDestination * 10);
            p2Castle = true;
        }
    }

    [RPC]
    void Promote(int x, int y, int player)
    {
        if (player == 1)
        {
            promoteReadyP1 = true;
            promoteLocationP1 = new Vector3(x * 10, 0.5f, y * 10);
            promoteXP1 = x;
            promoteYP1 = y;
        }

        if (player == 2)
        {
            promoteReadyP2 = true;
            promoteLocationP2 = new Vector3(x * 10, 0.5f, y * 10);
            promoteXP2 = x;
            promoteYP2 = y;
        }
    }


}
