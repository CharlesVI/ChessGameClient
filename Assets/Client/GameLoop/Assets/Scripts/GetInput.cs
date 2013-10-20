using UnityEngine;
using System.Collections;
/// <summary>
/// Get input.
/// This script will be used to collect input from the player.
/// The collected input will then be sent to the server for processing.
/// Specificly we will collect the peices origin and destination.
/// 
/// Note: this script may also be used to highlight the various on mouseover type events. However.
/// This may however be compartmentalized in the spirit of unity. 
/// </summary>

public class GetInput : MonoBehaviour 
{
    //Varibles Start **********************************************************************************************
    
    //These Varibles will be set by the peice that is clicked on.
    public int pieceTeam;
    public int pieceType;

    public bool pieceSelected;

    public int xOrigin;
    public int yOrigin;


    public bool tileSelected;

    public int xDestination;
    public int yDestination;

    public bool moveConfirmed = false;

    public int myTeam;

    public int p1YPreviousOrigin;
    public int p2YPreviousOrigin;
	
    //Varibles for Castle Logic.
    public bool p1QueensRookMoved;
    public bool p1KingsRookMoved;
    public bool p1KingMoved;

    public bool p2QueensRookMoved;
    public bool p2KingsRookMoved;
    public bool p2KingMoved;

    public int promotionChoice= 0;

    GameObject networkManager;
    BoardState boardState;
    GameState gameState;

	
    //Varibles End ***********************************************************************************************
	
	// Use this for initialization
	void Start () 
    {
        networkManager = GameObject.Find("NetworkManager");
        boardState = networkManager.GetComponent<BoardState>();
        gameState = networkManager.GetComponent<GameState>();
	}
	
	// Update is called once per frame
	void Update ()
    {
        //Ownership is checked by the piece itself.
        if (pieceSelected == true)
        {
            for (int x = 0; x < 8; x++) for (int y = 0; y < 8; y++)
                {
                    boardState.gameBoard[x, y].movePossible = false;
                }
            pieceSelected = false; // Right now I'm using this from running this whole loop constantly.
            #region Pawn Movement //TODO Capture Logic and Promotion
            if (pieceType == 1)
            {
                //team 1 is the lower player
                if (boardState.gamePiece[xOrigin, yOrigin].team == 1)
                {
                    if (yOrigin < 7) // sloppy and needs improvement
                    {
                        if (xOrigin < 7)
                        {
                            if (boardState.gamePiece[xOrigin + 1, yOrigin + 1].team == 2)
                            {
                                boardState.gameBoard[xOrigin + 1, yOrigin + 1].movePossible = true;
                            }
                        }

                        if (xOrigin > 0)
                        {
                            if (boardState.gamePiece[xOrigin - 1, yOrigin + 1].team == 2)
                            {
                                boardState.gameBoard[xOrigin - 1, yOrigin + 1].movePossible = true;
                            }
                        }

                        if (boardState.gamePiece[xOrigin, yOrigin + 1].type == 0)
                        {
                            boardState.gameBoard[xOrigin, yOrigin + 1].movePossible = true;
                            if (yOrigin == 1)
                            {
                                if (boardState.gamePiece[xOrigin, yOrigin + 2].type == 0)
                                {
                                    boardState.gameBoard[xOrigin, yOrigin + 2].movePossible = true;
                                }
                            }
                        }

                        if (yOrigin == 4)
                        {
                            if (xOrigin < 7)
                            {
                                if (boardState.gamePiece[xOrigin + 1, yOrigin].type == 1 &&
                                    boardState.gamePiece[xOrigin + 1, yOrigin].moved == 1 &&
                                    p2YPreviousOrigin == 6)
                                {
                                    boardState.gameBoard[xOrigin + 1, yOrigin + 1].movePossible = true;
                                }
                            }

                            if (xOrigin > 0)
                            {
                                if (boardState.gamePiece[xOrigin - 1, yOrigin].type == 1 &&
                                    boardState.gamePiece[xOrigin - 1, yOrigin].moved == 1 &&
                                    p2YPreviousOrigin == 6)
                                {
                                    boardState.gameBoard[xOrigin - 1, yOrigin + 1].movePossible = true;
                                }
                            }
                        }//if enPassant Possible
                    }//If < 7
                }//If team 1

                //Team 2 the Upper Team. *This is because pawns are not omni directional.
                if (boardState.gamePiece[xOrigin, yOrigin].team == 2)
                {
                    if (yOrigin > 0) // sloppy and needs improvement
                    {
                        if (xOrigin < 7)
                        {
                            if (boardState.gamePiece[xOrigin + 1, yOrigin - 1].team == 1)
                            {
                                boardState.gameBoard[xOrigin + 1, yOrigin - 1].movePossible = true;
                            }
                        }
                        if (xOrigin > 0)
                        {
                            if (boardState.gamePiece[xOrigin - 1, yOrigin - 1].team == 1)
                            {
                                boardState.gameBoard[xOrigin - 1, yOrigin - 1].movePossible = true;
                            }
                        }

                        if (boardState.gamePiece[xOrigin, yOrigin - 1].type == 0)
                        {
                            boardState.gameBoard[xOrigin, yOrigin - 1].movePossible = true;
                            if (yOrigin == 6)
                            {
                                if (boardState.gamePiece[xOrigin, yOrigin - 2].type == 0)
                                {
                                    boardState.gameBoard[xOrigin, yOrigin - 2].movePossible = true;
                                }
                            }
                        }

                        if (yOrigin == 3)
                        {
                            if (xOrigin < 7)
                            {
                                if (boardState.gamePiece[xOrigin + 1, yOrigin].type == 1 &&
                                    boardState.gamePiece[xOrigin + 1, yOrigin].moved == 1 &&
                                    p1YPreviousOrigin == 1)
                                {
                                    boardState.gameBoard[xOrigin + 1, yOrigin - 1].movePossible = true;
                                }
                            }

                            if (xOrigin > 0)
                            {
                                if (boardState.gamePiece[xOrigin - 1, yOrigin].type == 1 &&
                                    boardState.gamePiece[xOrigin - 1, yOrigin].moved == 1 &&
                                    p1YPreviousOrigin == 1)
                                {
                                    boardState.gameBoard[xOrigin - 1, yOrigin - 1].movePossible = true;
                                }
                            }
                        }//if enPassant Possible
                    }//If >0
                }//If team 2
                //Add Ability to Promote Pawn Here. Further Methodize Legality Check ########################################################################
            }//Pawn
            #endregion

            #region Rook Movement
            if (pieceType == 2)
            {
                for (int i = xOrigin + 1; i < 8; i++)
                {
                    if (boardState.gamePiece[i, yOrigin].type == 0)
                    {
                        boardState.gameBoard[i, yOrigin].movePossible = true;
                    }

                    if (boardState.gamePiece[i, yOrigin].team != myTeam && boardState.gamePiece[i, yOrigin].type != 0)
                    {
                        boardState.gameBoard[i, yOrigin].movePossible = true;
                        break;
                    }

                    if (boardState.gamePiece[i, yOrigin].team == myTeam)
                    {
                        break;
                    }
                }
                for (int i = xOrigin - 1; i > -1; i--)
                {
                    if (boardState.gamePiece[i, yOrigin].type == 0)
                    {
                        boardState.gameBoard[i, yOrigin].movePossible = true;
                    }

                    if (boardState.gamePiece[i, yOrigin].team != myTeam && boardState.gamePiece[i, yOrigin].type != 0)
                    {
                        boardState.gameBoard[i, yOrigin].movePossible = true;
                        break;
                    }

                    if (boardState.gamePiece[i, yOrigin].team == myTeam)
                    {
                        break;
                    }
                }
                for (int i = yOrigin + 1; i < 8; i++)
                {
                    if (boardState.gamePiece[xOrigin, i].type == 0)
                    {
                        boardState.gameBoard[xOrigin, i].movePossible = true;
                    }

                    if (boardState.gamePiece[xOrigin, i].team != myTeam && boardState.gamePiece[xOrigin, i].type != 0)
                    {
                        boardState.gameBoard[xOrigin, i].movePossible = true;
                        break;
                    }

                    if (boardState.gamePiece[xOrigin, i].team == myTeam)
                    {
                        break;
                    }
                }
                for (int i = yOrigin - 1; i > -1; i--)
                {
                    if (boardState.gamePiece[xOrigin, i].type == 0)
                    {
                        boardState.gameBoard[xOrigin, i].movePossible = true;
                    }

                    if (boardState.gamePiece[xOrigin, i].team != myTeam && boardState.gamePiece[xOrigin, i].type != 0)
                    {
                        boardState.gameBoard[xOrigin, i].movePossible = true;
                        break;
                    }

                    if (boardState.gamePiece[xOrigin, i].team == myTeam)
                    {
                        break;
                    }
                }
            }
            #endregion

            #region Knight Movement
            if (pieceType == 3)
            {
                //THIS cannot be the best way. 
                if (xOrigin + 2 >= 0 && xOrigin + 2 <= 7 && yOrigin + 1 >= 0 && yOrigin + 1 <= 7 &&
                    boardState.gamePiece[xOrigin + 2, yOrigin + 1].team != myTeam)
                {
                    boardState.gameBoard[xOrigin + 2, yOrigin + 1].movePossible = true;
                }

                if (xOrigin - 2 >= 0 && xOrigin - 2 <= 7 && yOrigin + 1 >= 0 && yOrigin + 1 <= 7 &&
                    boardState.gamePiece[xOrigin - 2, yOrigin + 1].team != myTeam)
                {
                    boardState.gameBoard[xOrigin - 2, yOrigin + 1].movePossible = true;
                }

                if (xOrigin + 2 >= 0 && xOrigin + 2 <= 7 && yOrigin - 1 >= 0 && yOrigin - 1 <= 7 &&
                    boardState.gamePiece[xOrigin + 2, yOrigin - 1].team != myTeam)
                {
                    boardState.gameBoard[xOrigin + 2, yOrigin - 1].movePossible = true;
                }

                if (xOrigin - 2 >= 0 && xOrigin - 2 <= 7 && yOrigin - 1 >= 0 && yOrigin - 1 <= 7 &&
                    boardState.gamePiece[xOrigin - 2, yOrigin - 1].team != myTeam)
                {
                    boardState.gameBoard[xOrigin - 2, yOrigin - 1].movePossible = true;
                }

                if (xOrigin + 1 >= 0 && xOrigin + 1 <= 7 && yOrigin + 2 >= 0 && yOrigin + 2 <= 7 &&
                    boardState.gamePiece[xOrigin + 1, yOrigin + 2].team != myTeam)
                {
                    boardState.gameBoard[xOrigin + 1, yOrigin + 2].movePossible = true;
                }

                if (xOrigin - 1 >= 0 && xOrigin - 1 <= 7 && yOrigin + 2 >= 0 && yOrigin + 2 <= 7 &&
                    boardState.gamePiece[xOrigin - 1, yOrigin + 2].team != myTeam)
                {
                    boardState.gameBoard[xOrigin - 1, yOrigin + 2].movePossible = true;
                }

                if (xOrigin + 1 >= 0 && xOrigin + 1 <= 7 && yOrigin - 2 >= 0 && yOrigin - 2 <= 7 &&
                    boardState.gamePiece[xOrigin + 1, yOrigin - 2].team != myTeam)
                {
                    boardState.gameBoard[xOrigin + 1, yOrigin - 2].movePossible = true;
                }

                if (xOrigin - 1 >= 0 && xOrigin - 1 <= 7 && yOrigin - 2 >= 0 && yOrigin - 2 <= 7 &&
                    boardState.gamePiece[xOrigin - 1, yOrigin - 2].team != myTeam)
                {
                    boardState.gameBoard[xOrigin - 1, yOrigin - 2].movePossible = true;
                }

            }
            #endregion

            #region Bishop Movement
            if (pieceType == 4)
            {
                for (int i = 1; i < 8; i++)
                {
                    int x = xOrigin + i;
                    int y = yOrigin + i;
                    if (y < 8 && x < 8)
                    {

                        if (boardState.gamePiece[x, y].team != myTeam)
                        {
                            boardState.gameBoard[x, y].movePossible = true;
                            if (boardState.gamePiece[x, y].team != myTeam && boardState.gamePiece[x, y].type != 0)
                            {
                                break;
                            }
                        }
                        if (boardState.gamePiece[x, y].team == myTeam)
                        {
                            break;
                        }
                    }
                }
                for (int i = 1; i < 8; i++)
                {
                    int x = xOrigin + i;
                    int y = yOrigin - i;
                    if (y > -1 && x < 8)
                    {

                        if (boardState.gamePiece[x, y].team != myTeam)
                        {
                            boardState.gameBoard[x, y].movePossible = true;
                            if (boardState.gamePiece[x, y].team != myTeam && boardState.gamePiece[x, y].type != 0)
                            {
                                break;
                            }
                        }
                        if (boardState.gamePiece[x, y].team == myTeam)
                        {
                            break;
                        }
                    }
                }
                for (int i = 1; i < 8; i++)
                {
                    int x = xOrigin - i;
                    int y = yOrigin - i;
                    if (x > -1 && y > -1)
                    {

                        if (boardState.gamePiece[x, y].team != myTeam)
                        {
                            boardState.gameBoard[x, y].movePossible = true;
                            if (boardState.gamePiece[x, y].team != myTeam && boardState.gamePiece[x, y].type != 0)
                            {
                                break;
                            }
                        }
                        if (boardState.gamePiece[x, y].team == myTeam)
                        {
                            break;
                        }
                    }

                }
                for (int i = 1; i < 8; i++)
                {
                    int x = xOrigin - i;
                    int y = yOrigin + i;
                    if (y < 8 && x > -1)
                    {

                        if (boardState.gamePiece[x, y].team != myTeam)
                        {
                            boardState.gameBoard[x, y].movePossible = true;
                            if (boardState.gamePiece[x, y].team != myTeam && boardState.gamePiece[x, y].type != 0)
                            {
                                break;
                            }
                        }
                        if (boardState.gamePiece[x, y].team == myTeam)
                        {
                            break;
                        }
                    }

                }
            }
            #endregion

            #region Queen Movement
            if (pieceType == 5)
            {
                //orthaganols  
                for (int i = xOrigin + 1; i < 8; i++)
                {
                    if (boardState.gamePiece[i, yOrigin].type == 0)
                    {
                        boardState.gameBoard[i, yOrigin].movePossible = true;
                    }

                    if (boardState.gamePiece[i, yOrigin].team != myTeam && boardState.gamePiece[i, yOrigin].type != 0)
                    {
                        boardState.gameBoard[i, yOrigin].movePossible = true;
                        break;
                    }

                    if (boardState.gamePiece[i, yOrigin].team == myTeam)
                    {
                        break;
                    }
                }
                for (int i = xOrigin - 1; i > -1; i--)
                {
                    if (boardState.gamePiece[i, yOrigin].type == 0)
                    {
                        boardState.gameBoard[i, yOrigin].movePossible = true;
                    }

                    if (boardState.gamePiece[i, yOrigin].team != myTeam && boardState.gamePiece[i, yOrigin].type != 0)
                    {
                        boardState.gameBoard[i, yOrigin].movePossible = true;
                        break;
                    }

                    if (boardState.gamePiece[i, yOrigin].team == myTeam)
                    {
                        break;
                    }
                }
                for (int i = yOrigin + 1; i < 8; i++)
                {
                    if (boardState.gamePiece[xOrigin, i].type == 0)
                    {
                        boardState.gameBoard[xOrigin, i].movePossible = true;
                    }

                    if (boardState.gamePiece[xOrigin, i].team != myTeam && boardState.gamePiece[xOrigin, i].type != 0)
                    {
                        boardState.gameBoard[xOrigin, i].movePossible = true;
                        break;
                    }

                    if (boardState.gamePiece[xOrigin, i].team == myTeam)
                    {
                        break;
                    }
                }
                for (int i = yOrigin - 1; i > -1; i--)
                {
                    if (boardState.gamePiece[xOrigin, i].type == 0)
                    {
                        boardState.gameBoard[xOrigin, i].movePossible = true;
                    }

                    if (boardState.gamePiece[xOrigin, i].team != myTeam && boardState.gamePiece[xOrigin, i].type != 0)
                    {
                        boardState.gameBoard[xOrigin, i].movePossible = true;
                        break;
                    }

                    if (boardState.gamePiece[xOrigin, i].team == myTeam)
                    {
                        break;
                    }
                }
                //Diagonals
                {
                    for (int i = 1; i < 8; i++)
                    {
                        int x = xOrigin + i;
                        int y = yOrigin + i;
                        if (y < 8 && x < 8)
                        {

                            if (boardState.gamePiece[x, y].team != myTeam)
                            {
                                boardState.gameBoard[x, y].movePossible = true;
                                if (boardState.gamePiece[x, y].team != myTeam && boardState.gamePiece[x, y].type != 0)
                                {
                                    break;
                                }
                            }
                            if (boardState.gamePiece[x, y].team == myTeam)
                            {
                                break;
                            }
                        }
                    }
                    for (int i = 1; i < 8; i++)
                    {
                        int x = xOrigin + i;
                        int y = yOrigin - i;
                        if (y > -1 && x < 8)
                        {

                            if (boardState.gamePiece[x, y].team != myTeam)
                            {
                                boardState.gameBoard[x, y].movePossible = true;
                                if (boardState.gamePiece[x, y].team != myTeam && boardState.gamePiece[x, y].type != 0)
                                {
                                    break;
                                }
                            }
                            if (boardState.gamePiece[x, y].team == myTeam)
                            {
                                break;
                            }
                        }
                    }
                    for (int i = 1; i < 8; i++)
                    {
                        int x = xOrigin - i;
                        int y = yOrigin - i;
                        if (x > -1 && y > -1)
                        {

                            if (boardState.gamePiece[x, y].team != myTeam)
                            {
                                boardState.gameBoard[x, y].movePossible = true;
                                if (boardState.gamePiece[x, y].team != myTeam && boardState.gamePiece[x, y].type != 0)
                                {
                                    break;
                                }
                            }
                            if (boardState.gamePiece[x, y].team == myTeam)
                            {
                                break;
                            }
                        }
                        
                    }
                    for (int i = 1; i < 8; i++)
                    {
                        int x = xOrigin - i;
                        int y = yOrigin + i;
                        if (y < 8 && x > -1)
                        {

                            if (boardState.gamePiece[x, y].team != myTeam)
                            {
                                boardState.gameBoard[x, y].movePossible = true;
                                if (boardState.gamePiece[x, y].team != myTeam && boardState.gamePiece[x, y].type != 0)
                                {
                                    break;
                                }
                            }
                            if (boardState.gamePiece[x, y].team == myTeam)
                            {
                                break;
                            }
                        }
                    }
                }
            }
            #endregion

            #region King Movement
            if (pieceType == 6)
            {
                for (int i = -1; i < 2; i++)
                {
                    for (int j = -1; j < 2; j++)
                    {
                        int x = xOrigin + i;
                        int y = yOrigin + j;
                        if (x > -1 && x < 8 && y > -1 && y < 8)
                        {
                            if (boardState.gamePiece[x, y].team != myTeam)
                            {
                                if (myTeam == 1 &&  boardState.gameBoard[x,y].threatPlayerTwo == false)
                                {
                                    boardState.gameBoard[x, y].movePossible = true;
                                }
                                if (myTeam == 2 && boardState.gameBoard[x, y].threatPlayerOne == false)
                                {
                                    boardState.gameBoard[x, y].movePossible = true;
                                }
                            }
                        }
                    }
                }//Movement

                //Castle Logic
                if (myTeam == 1)
                {
                    if (yOrigin == 0)
                    {
                        if (p1KingMoved == false)
                        {
                            if (p1QueensRookMoved == false)
                            {
                                if (boardState.gamePiece[1, 0].type == 0 && boardState.gamePiece[2, 0].type == 0 && boardState.gamePiece[3, 0].type == 0 &&
                                    boardState.gameBoard[4, 0].threatPlayerTwo == false && boardState.gameBoard[2, 0].threatPlayerTwo == false && boardState.gameBoard[3, 0].threatPlayerTwo == false)
                                {
                                    boardState.gameBoard[2, 0].movePossible = true;
                                }
                            }

                            if (p1KingsRookMoved == false)
                            {
                                if (boardState.gamePiece[5, 0].type == 0 && boardState.gamePiece[6, 0].type == 0 &&
                                    boardState.gameBoard[4, 0].threatPlayerTwo == false && boardState.gameBoard[5, 0].threatPlayerTwo == false && boardState.gameBoard[6, 0].threatPlayerTwo == false)
                                {
                                    boardState.gameBoard[6, 0].movePossible = true;
                                }
                            }
                        }
                    }
                }//Team 1 Castle

                if (myTeam == 2)
                {
                    if (yOrigin == 7)
                    {
                        if (p2KingMoved == false)
                        {
                            if (p2QueensRookMoved == false)
                            {
                                if (boardState.gamePiece[1, 7].type == 0 && boardState.gamePiece[2, 7].type == 0 && boardState.gamePiece[3, 7].type == 0 &&
                                    boardState.gameBoard[4, 7].threatPlayerOne == false && boardState.gameBoard[2, 7].threatPlayerOne == false && boardState.gameBoard[3, 7].threatPlayerOne == false)
                                {
                                    boardState.gameBoard[2, 7].movePossible = true;
                                }
                            }

                            if (p2KingsRookMoved == false)
                            {
                                if (boardState.gamePiece[5, 7].type == 0 && boardState.gamePiece[6, 7].type == 0 &&
                                    boardState.gameBoard[4, 7].threatPlayerOne == false && boardState.gameBoard[5, 7].threatPlayerOne == false && boardState.gameBoard[6, 7].threatPlayerOne == false)
                                {
                                    boardState.gameBoard[6, 7].movePossible = true;
                                }
                            }
                        }
                    }
                }//Team 2 Castle
            }//King
            #endregion
        }//Piece Selected = true;

        if (moveConfirmed == true)
        {
            if (boardState.gamePiece[xOrigin, yOrigin].type == 1)
            {
                if (myTeam == 1 && yDestination == 7 && promotionChoice == 0)
                { gameState.promotion = true; }
                if (myTeam == 2 && yDestination == 0 && promotionChoice == 0)
                { gameState.promotion = true; }
            }

            if (gameState.promotion == false)
            {
                if (promotionChoice != 0)
                {
                    Debug.Log("Promotion Called");
                    networkView.RPC("PromotionChoice", RPCMode.Server, Network.player, promotionChoice);
                    promotionChoice = 0;
                }
                Debug.Log("ready to move called");
                networkView.RPC("ReadyToMove", RPCMode.Server, Network.player, xOrigin, yOrigin, xDestination, yDestination);
                moveConfirmed = false;
            }
        }

    }//Update

    [RPC]
    void AssignPlayerTeam(NetworkPlayer nPlayer, int team)
    {
        if (Network.player == nPlayer)
        {
            myTeam = team;
            Debug.Log("My team is " + myTeam);
        }
    }

    [RPC]
    void ReadyToMove(NetworkPlayer nPlayer, int xOrigin, int yOrigin, int xDestination, int yDestination)
    {
        Debug.Log("ready to move called");
    }

    [RPC]
    void PromotionChoice(NetworkPlayer nPlayer, int choice)
    { Debug.Log("Promotion Called");  }


}//Class
