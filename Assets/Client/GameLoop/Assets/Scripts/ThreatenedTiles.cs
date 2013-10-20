using UnityEngine;
using System.Collections;

public class ThreatenedTiles : MonoBehaviour {

    /*
     *This Script is going to be used to check and see if a tile is threatened by any peice at the start of a round.
     *
     */

    //Varibles Start *************************************************************************

    BoardState boardState;
    GameObject networkManager;
    GetInput getInput;
    Animate animate;

    //Varibles End ***************************************************************************

	// Use this for initialization
	void Start () 
    {
        networkManager = GameObject.Find("NetworkManager");
        boardState = networkManager.GetComponent<BoardState>();
        getInput = networkManager.GetComponent<GetInput>();
        animate = networkManager.GetComponent<Animate>();

	}
	
	// Update is called once per frame 
    void Update()
    {
        //Runs once per turn to see 
        if (animate.threatCheckReady == true)
        {
            for (int x = 0; x < 8; x++) for (int y = 0; y < 8; y++)
                {
                    boardState.gameBoard[x, y].threatPlayerOne = false;
                    boardState.gameBoard[x, y].threatPlayerTwo = false;
                }
            for (int x = 0; x < 8; x++) for (int y = 0; y < 8; y++)
                {
                    if (boardState.gamePiece[x, y].team == 1)
                    {
                        int myTeam = 1;
                        #region Pawn Movement //TODO  Promotion
                        if (boardState.gamePiece[x, y].type == 1)
                        {
                            if (y < 7) // sloppy and needs improvement
                            {
                                if (x < 7)
                                {                                    
                                    boardState.gameBoard[x + 1, y + 1].threatPlayerOne = true;         
                                }

                                if (x > 0)
                                {
                                    boardState.gameBoard[x - 1, y + 1].threatPlayerOne = true;
                                }

                                if (y == 4)
                                {
                                    if (x < 7)
                                    {
                                        if (boardState.gamePiece[x + 1, y].type == 1 &&
                                            boardState.gamePiece[x + 1, y].moved == 1 &&
                                            getInput.p2YPreviousOrigin == 6)
                                        {
                                            boardState.gameBoard[x + 1, y + 1].threatPlayerOne = true;
                                        }
                                    }

                                    if (x > 0)
                                    {
                                        if (boardState.gamePiece[x - 1, y].type == 1 &&
                                            boardState.gamePiece[x - 1, y].moved == 1 &&
                                            getInput.p2YPreviousOrigin == 6)
                                        {
                                            boardState.gameBoard[x - 1, y + 1].threatPlayerOne = true;
                                        }
                                    }
                                }//if enPassant Possible
                            }//If < 7
                            //Add Ability to Promote Pawn Here. Further Methodize Legality Check ########################################################################
                        }//Pawn
                        #endregion

                        #region Rook Movement
                        if (boardState.gamePiece[x, y].type == 2)
                        {
                            for (int i = x + 1; i < 8; i++)
                            {
                                if (boardState.gamePiece[i, y].type == 0)
                                {
                                    boardState.gameBoard[i, y].threatPlayerOne = true;
                                }

                                if (boardState.gamePiece[i, y].team != myTeam && boardState.gamePiece[i, y].type != 0)
                                {
                                    boardState.gameBoard[i, y].threatPlayerOne = true;
                                    break;
                                }

                                if (boardState.gamePiece[i, y].team == myTeam)
                                {
                                    break;
                                }
                            }
                            for (int i = x - 1; i > -1; i--)
                            {
                                if (boardState.gamePiece[i, y].type == 0)
                                {
                                    boardState.gameBoard[i, y].threatPlayerOne = true;
                                }

                                if (boardState.gamePiece[i, y].team != myTeam && boardState.gamePiece[i, y].type != 0)
                                {
                                    boardState.gameBoard[i, y].threatPlayerOne = true;
                                    break;
                                }

                                if (boardState.gamePiece[i, y].team == myTeam)
                                {
                                    break;
                                }
                            }
                            for (int i = y + 1; i < 8; i++)
                            {
                                if (boardState.gamePiece[x, i].type == 0)
                                {
                                    boardState.gameBoard[x, i].threatPlayerOne = true;
                                }

                                if (boardState.gamePiece[x, i].team != myTeam && boardState.gamePiece[x, i].type != 0)
                                {
                                    boardState.gameBoard[x, i].threatPlayerOne = true;
                                    break;
                                }

                                if (boardState.gamePiece[x, i].team == myTeam)
                                {
                                    break;
                                }
                            }
                            for (int i = y - 1; i > -1; i--)
                            {
                                if (boardState.gamePiece[x, i].type == 0)
                                {
                                    boardState.gameBoard[x, i].threatPlayerOne = true;
                                }

                                if (boardState.gamePiece[x, i].team != myTeam && boardState.gamePiece[x, i].type != 0)
                                {
                                    boardState.gameBoard[x, i].threatPlayerOne = true;
                                    break;
                                }

                                if (boardState.gamePiece[x, i].team == myTeam)
                                {
                                    break;
                                }
                            }
                        }
                        #endregion

                        #region Knight Movement
                        if (boardState.gamePiece[x, y].type == 3)
                        {
                            //THIS cannot be the best way. 
                            if (x + 2 >= 0 && x + 2 <= 7 && y + 1 >= 0 && y + 1 <= 7 &&
                                boardState.gamePiece[x + 2, y + 1].team != myTeam)
                            {
                                boardState.gameBoard[x + 2, y + 1].threatPlayerOne = true;
                            }

                            if (x - 2 >= 0 && x - 2 <= 7 && y + 1 >= 0 && y + 1 <= 7 &&
                                boardState.gamePiece[x - 2, y + 1].team != myTeam)
                            {
                                boardState.gameBoard[x - 2, y + 1].threatPlayerOne = true;
                            }

                            if (x + 2 >= 0 && x + 2 <= 7 && y - 1 >= 0 && y - 1 <= 7 &&
                                boardState.gamePiece[x + 2, y - 1].team != myTeam)
                            {
                                boardState.gameBoard[x + 2, y - 1].threatPlayerOne = true;
                            }

                            if (x - 2 >= 0 && x - 2 <= 7 && y - 1 >= 0 && y - 1 <= 7 &&
                                boardState.gamePiece[x - 2, y - 1].team != myTeam)
                            {
                                boardState.gameBoard[x - 2, y - 1].threatPlayerOne = true;
                            }

                            if (x + 1 >= 0 && x + 1 <= 7 && y + 2 >= 0 && y + 2 <= 7 &&
                                boardState.gamePiece[x + 1, y + 2].team != myTeam)
                            {
                                boardState.gameBoard[x + 1, y + 2].threatPlayerOne = true;
                            }

                            if (x - 1 >= 0 && x - 1 <= 7 && y + 2 >= 0 && y + 2 <= 7 &&
                                boardState.gamePiece[x - 1, y + 2].team != myTeam)
                            {
                                boardState.gameBoard[x - 1, y + 2].threatPlayerOne = true;
                            }

                            if (x + 1 >= 0 && x + 1 <= 7 && y - 2 >= 0 && y - 2 <= 7 &&
                                boardState.gamePiece[x + 1, y - 2].team != myTeam)
                            {
                                boardState.gameBoard[x + 1, y - 2].threatPlayerOne = true;
                            }

                            if (x - 1 >= 0 && x - 1 <= 7 && y - 2 >= 0 && y - 2 <= 7 &&
                                boardState.gamePiece[x - 1, y - 2].team != myTeam)
                            {
                                boardState.gameBoard[x - 1, y - 2].threatPlayerOne = true;
                            }

                        }
                        #endregion

                        #region Bishop Movement
                        if (boardState.gamePiece[x, y].type == 4)
                        {
                            for (int i = 1; i < 8; i++)
                            {
                                int x1 = x + i;
                                int y1 = y + i;
                                if (y1 < 8 && x1 < 8)
                                {

                                    if (boardState.gamePiece[x1, y1].team != myTeam)
                                    {
                                        boardState.gameBoard[x1, y1].threatPlayerOne = true;
                                        if (boardState.gamePiece[x1, y1].team != myTeam && boardState.gamePiece[x1, y1].type != 0)
                                        {
                                            break;
                                        }
                                    }
                                    if (boardState.gamePiece[x1, y1].team == myTeam)
                                    {
                                        break;
                                    }
                                }
                            }
                            for (int i = 1; i < 8; i++)
                            {
                                int x1 = x + i;
                                int y1 = y - i;
                                if (y1 > -1 && x1 < 8)
                                {

                                    if (boardState.gamePiece[x1, y1].team != myTeam)
                                    {
                                        boardState.gameBoard[x1, y1].threatPlayerOne = true;
                                        if (boardState.gamePiece[x1, y1].team != myTeam && boardState.gamePiece[x1, y1].type != 0)
                                        {
                                            break;
                                        }
                                    }
                                    if (boardState.gamePiece[x1, y1].team == myTeam)
                                    {
                                        break;
                                    }
                                }
                            }
                            for (int i = 1; i < 8; i++)
                            {
                                int x1 = x - i;
                                int y1 = y - i;
                                if (x1 > -1 && y1 > -1)
                                {

                                    if (boardState.gamePiece[x1, y1].team != myTeam)
                                    {
                                        boardState.gameBoard[x1, y1].threatPlayerOne = true;
                                        if (boardState.gamePiece[x1, y1].team != myTeam && boardState.gamePiece[x1, y1].type != 0)
                                        {
                                            break;
                                        }
                                    }
                                    if (boardState.gamePiece[x1, y1].team == myTeam)
                                    {
                                        break;
                                    }
                                }

                            }
                            for (int i = 1; i < 8; i++)
                            {
                                int x1 = x - i;
                                int y1 = y + i;
                                if (y1 < 8 && x1 > -1)
                                {

                                    if (boardState.gamePiece[x1, y1].team != myTeam)
                                    {
                                        boardState.gameBoard[x1, y1].threatPlayerOne = true;
                                        if (boardState.gamePiece[x1, y1].team != myTeam && boardState.gamePiece[x1, y1].type != 0)
                                        {
                                            break;
                                        }
                                    }
                                    if (boardState.gamePiece[x1, y1].team == myTeam)
                                    {
                                        break;
                                    }
                                }

                            }
                        }
                        #endregion

                        #region Queen Movement
                        if (boardState.gamePiece[x, y].type == 5)
                        {
                            //orthaganols  
                            for (int i = x + 1; i < 8; i++)
                            {
                                if (boardState.gamePiece[i, y].type == 0)
                                {
                                    boardState.gameBoard[i, y].threatPlayerOne = true;
                                }

                                if (boardState.gamePiece[i, y].team != myTeam && boardState.gamePiece[i, y].type != 0)
                                {
                                    boardState.gameBoard[i, y].threatPlayerOne = true;
                                    break;
                                }

                                if (boardState.gamePiece[i, y].team == myTeam)
                                {
                                    break;
                                }
                            }
                            for (int i = x - 1; i > -1; i--)
                            {
                                if (boardState.gamePiece[i, y].type == 0)
                                {
                                    boardState.gameBoard[i, y].threatPlayerOne = true;
                                }

                                if (boardState.gamePiece[i, y].team != myTeam && boardState.gamePiece[i, y].type != 0)
                                {
                                    boardState.gameBoard[i, y].threatPlayerOne = true;
                                    break;
                                }

                                if (boardState.gamePiece[i, y].team == myTeam)
                                {
                                    break;
                                }
                            }
                            for (int i = y + 1; i < 8; i++)
                            {
                                if (boardState.gamePiece[x, i].type == 0)
                                {
                                    boardState.gameBoard[x, i].threatPlayerOne = true;
                                }

                                if (boardState.gamePiece[x, i].team != myTeam && boardState.gamePiece[x, i].type != 0)
                                {
                                    boardState.gameBoard[x, i].threatPlayerOne = true;
                                    break;
                                }

                                if (boardState.gamePiece[x, i].team == myTeam)
                                {
                                    break;
                                }
                            }
                            for (int i = y - 1; i > -1; i--)
                            {
                                if (boardState.gamePiece[x, i].type == 0)
                                {
                                    boardState.gameBoard[x, i].threatPlayerOne = true;
                                }

                                if (boardState.gamePiece[x, i].team != myTeam && boardState.gamePiece[x, i].type != 0)
                                {
                                    boardState.gameBoard[x, i].threatPlayerOne = true;
                                    break;
                                }

                                if (boardState.gamePiece[x, i].team == myTeam)
                                {
                                    break;
                                }
                            }
                            //Diagonals
                            {
                                for (int i = 1; i < 8; i++)
                                {
                                    int x1 = x + i;
                                    int y1 = y + i;
                                    if (y1 < 8 && x1 < 8)
                                    {

                                        if (boardState.gamePiece[x1, y1].team != myTeam)
                                        {
                                            boardState.gameBoard[x1, y1].threatPlayerOne = true;
                                            if (boardState.gamePiece[x1, y1].team != myTeam && boardState.gamePiece[x1, y1].type != 0)
                                            {
                                                break;
                                            }
                                        }
                                        if (boardState.gamePiece[x1, y1].team == myTeam)
                                        {
                                            break;
                                        }
                                    }
                                }
                                for (int i = 1; i < 8; i++)
                                {
                                    int x1 = x + i;
                                    int y1 = y - i;
                                    if (y1 > -1 && x1 < 8)
                                    {

                                        if (boardState.gamePiece[x1, y1].team != myTeam)
                                        {
                                            boardState.gameBoard[x1, y1].threatPlayerOne = true;
                                            if (boardState.gamePiece[x1, y1].team != myTeam && boardState.gamePiece[x1, y1].type != 0)
                                            {
                                                break;
                                            }
                                        }
                                        if (boardState.gamePiece[x1, y1].team == myTeam)
                                        {
                                            break;
                                        }
                                    }
                                }
                                for (int i = 1; i < 8; i++)
                                {
                                    int x1 = x - i;
                                    int y1 = y - i;
                                    if (x1 > -1 && y1 > -1)
                                    {

                                        if (boardState.gamePiece[x1, y1].team != myTeam)
                                        {
                                            boardState.gameBoard[x1, y1].threatPlayerOne = true;
                                            if (boardState.gamePiece[x1, y1].team != myTeam && boardState.gamePiece[x1, y1].type != 0)
                                            {
                                                break;
                                            }
                                        }
                                        if (boardState.gamePiece[x1, y1].team == myTeam)
                                        {
                                            break;
                                        }
                                    }

                                }
                                for (int i = 1; i < 8; i++)
                                {
                                    int x1 = x - i;
                                    int y1 = y + i;
                                    if (y1 < 8 && x1 > -1)
                                    {

                                        if (boardState.gamePiece[x1, y1].team != myTeam)
                                        {
                                            boardState.gameBoard[x1, y1].threatPlayerOne = true;
                                            if (boardState.gamePiece[x1, y1].team != myTeam && boardState.gamePiece[x1, y1].type != 0)
                                            {
                                                break;
                                            }
                                        }
                                        if (boardState.gamePiece[x1, y1].team == myTeam)
                                        {
                                            break;
                                        }
                                    }
                                }
                            }
                        }
                        #endregion

                        #region King Movement //Todo threat check
                        if (boardState.gamePiece[x, y].type == 6)
                        {                            
                            for (int i = -1; i < 2; i++)
                            {
                                for (int j = -1; j < 2; j++)
                                {
                                    int x1 = x + i;
                                    int y1 = y + j;
                                    if (x1 > -1 && x1 < 8 && y1 > -1 && y1 < 8)
                                    {
                                        if (boardState.gamePiece[x1, y1].team != myTeam)
                                        {
                                            boardState.gameBoard[x1, y1].threatPlayerOne = true;
                                        }
                                    }
                                }
                            }
                        }
                        #endregion
                    }
                    if (boardState.gamePiece[x, y].team == 2)
                    {
                        int myTeam = 2;
                        #region Pawn Movement //TODO Promotion
                        if (boardState.gamePiece[x, y].type == 1)
                        {
                            //Team 2 the Upper Team. *This is because pawns are not omni directional.
                            if (boardState.gamePiece[x, y].team == 2)
                            {
                                if (y > 0) // sloppy and needs improvement
                                {
                                    if (x < 7)
                                    {
                                        boardState.gameBoard[x + 1, y - 1].threatPlayerTwo = true;
                                    }
                                    
                                    if (x > 0)
                                    {
                                        boardState.gameBoard[x - 1, y - 1].threatPlayerTwo = true;
                                    }

                                    if (y == 3)
                                    {
                                        if (x < 7)
                                        {
                                            if (boardState.gamePiece[x + 1, y].type == 1 &&
                                                boardState.gamePiece[x + 1, y].moved == 1 &&
                                                getInput.p1YPreviousOrigin == 1)
                                            {
                                                boardState.gameBoard[x + 1, y - 1].threatPlayerTwo = true;
                                            }
                                        }

                                        if (x > 0)
                                        {
                                            if (boardState.gamePiece[x - 1, y].type == 1 &&
                                                boardState.gamePiece[x - 1, y].moved == 1 &&
                                                getInput.p1YPreviousOrigin == 1)
                                            {
                                                boardState.gameBoard[x - 1, y - 1].threatPlayerTwo = true;
                                            }
                                        }
                                    }//if enPassant Possible
                                }//If >0
                            }//If team 2
                            //Add Ability to Promote Pawn Here. Further Methodize Legality Check ########################################################################
                        }//Pawn
                        #endregion

                        #region Rook Movement
                        if (boardState.gamePiece[x, y].type == 2)
                        {
                            for (int i = x + 1; i < 8; i++)
                            {
                                if (boardState.gamePiece[i, y].type == 0)
                                {
                                    boardState.gameBoard[i, y].threatPlayerTwo = true;
                                }

                                if (boardState.gamePiece[i, y].team != myTeam && boardState.gamePiece[i, y].type != 0)
                                {
                                    boardState.gameBoard[i, y].threatPlayerTwo = true;
                                    break;
                                }

                                if (boardState.gamePiece[i, y].team == myTeam)
                                {
                                    break;
                                }
                            }
                            for (int i = x - 1; i > -1; i--)
                            {
                                if (boardState.gamePiece[i, y].type == 0)
                                {
                                    boardState.gameBoard[i, y].threatPlayerTwo = true;
                                }

                                if (boardState.gamePiece[i, y].team != myTeam && boardState.gamePiece[i, y].type != 0)
                                {
                                    boardState.gameBoard[i, y].threatPlayerTwo = true;
                                    break;
                                }

                                if (boardState.gamePiece[i, y].team == myTeam)
                                {
                                    break;
                                }
                            }
                            for (int i = y + 1; i < 8; i++)
                            {
                                if (boardState.gamePiece[x, i].type == 0)
                                {
                                    boardState.gameBoard[x, i].threatPlayerTwo = true;
                                }

                                if (boardState.gamePiece[x, i].team != myTeam && boardState.gamePiece[x, i].type != 0)
                                {
                                    boardState.gameBoard[x, i].threatPlayerTwo = true;
                                    break;
                                }

                                if (boardState.gamePiece[x, i].team == myTeam)
                                {
                                    break;
                                }
                            }
                            for (int i = y - 1; i > -1; i--)
                            {
                                if (boardState.gamePiece[x, i].type == 0)
                                {
                                    boardState.gameBoard[x, i].threatPlayerTwo = true;
                                }

                                if (boardState.gamePiece[x, i].team != myTeam && boardState.gamePiece[x, i].type != 0)
                                {
                                    boardState.gameBoard[x, i].threatPlayerTwo = true;
                                    break;
                                }

                                if (boardState.gamePiece[x, i].team == myTeam)
                                {
                                    break;
                                }
                            }
                        }
                        #endregion

                        #region Knight Movement
                        if (boardState.gamePiece[x, y].type == 3)
                        {
                            //THIS cannot be the best way. 
                            if (x + 2 >= 0 && x + 2 <= 7 && y + 1 >= 0 && y + 1 <= 7 &&
                                boardState.gamePiece[x + 2, y + 1].team != myTeam)
                            {
                                boardState.gameBoard[x + 2, y + 1].threatPlayerTwo = true;
                            }

                            if (x - 2 >= 0 && x - 2 <= 7 && y + 1 >= 0 && y + 1 <= 7 &&
                                boardState.gamePiece[x - 2, y + 1].team != myTeam)
                            {
                                boardState.gameBoard[x - 2, y + 1].threatPlayerTwo = true;
                            }

                            if (x + 2 >= 0 && x + 2 <= 7 && y - 1 >= 0 && y - 1 <= 7 &&
                                boardState.gamePiece[x + 2, y - 1].team != myTeam)
                            {
                                boardState.gameBoard[x + 2, y - 1].threatPlayerTwo = true;
                            }

                            if (x - 2 >= 0 && x - 2 <= 7 && y - 1 >= 0 && y - 1 <= 7 &&
                                boardState.gamePiece[x - 2, y - 1].team != myTeam)
                            {
                                boardState.gameBoard[x - 2, y - 1].threatPlayerTwo = true;
                            }

                            if (x + 1 >= 0 && x + 1 <= 7 && y + 2 >= 0 && y + 2 <= 7 &&
                                boardState.gamePiece[x + 1, y + 2].team != myTeam)
                            {
                                boardState.gameBoard[x + 1, y + 2].threatPlayerTwo = true;
                            }

                            if (x - 1 >= 0 && x - 1 <= 7 && y + 2 >= 0 && y + 2 <= 7 &&
                                boardState.gamePiece[x - 1, y + 2].team != myTeam)
                            {
                                boardState.gameBoard[x - 1, y + 2].threatPlayerTwo = true;
                            }

                            if (x + 1 >= 0 && x + 1 <= 7 && y - 2 >= 0 && y - 2 <= 7 &&
                                boardState.gamePiece[x + 1, y - 2].team != myTeam)
                            {
                                boardState.gameBoard[x + 1, y - 2].threatPlayerTwo = true;
                            }

                            if (x - 1 >= 0 && x - 1 <= 7 && y - 2 >= 0 && y - 2 <= 7 &&
                                boardState.gamePiece[x - 1, y - 2].team != myTeam)
                            {
                                boardState.gameBoard[x - 1, y - 2].threatPlayerTwo = true;
                            }

                        }
                        #endregion

                        #region Bishop Movement
                        if (boardState.gamePiece[x, y].type == 4)
                        {
                            for (int i = 1; i < 8; i++)
                            {
                                int x1 = x + i;
                                int y1 = y + i;
                                if (y1 < 8 && x1 < 8)
                                {

                                    if (boardState.gamePiece[x1, y1].team != myTeam)
                                    {
                                        boardState.gameBoard[x1, y1].threatPlayerTwo = true;
                                        if (boardState.gamePiece[x1, y1].team != myTeam && boardState.gamePiece[x1, y1].type != 0)
                                        {
                                            break;
                                        }
                                    }
                                    if (boardState.gamePiece[x1, y1].team == myTeam)
                                    {
                                        break;
                                    }
                                }
                            }
                            for (int i = 1; i < 8; i++)
                            {
                                int x1 = x + i;
                                int y1 = y - i;
                                if (y1 > -1 && x1 < 8)
                                {

                                    if (boardState.gamePiece[x1, y1].team != myTeam)
                                    {
                                        boardState.gameBoard[x1, y1].threatPlayerTwo = true;
                                        if (boardState.gamePiece[x1, y1].team != myTeam && boardState.gamePiece[x1, y1].type != 0)
                                        {
                                            break;
                                        }
                                    }
                                    if (boardState.gamePiece[x1, y1].team == myTeam)
                                    {
                                        break;
                                    }
                                }
                            }
                            for (int i = 1; i < 8; i++)
                            {
                                int x1 = x - i;
                                int y1 = y - i;
                                if (x1 > -1 && y1 > -1)
                                {

                                    if (boardState.gamePiece[x1, y1].team != myTeam)
                                    {
                                        boardState.gameBoard[x1, y1].threatPlayerTwo = true;
                                        if (boardState.gamePiece[x1, y1].team != myTeam && boardState.gamePiece[x1, y1].type != 0)
                                        {
                                            break;
                                        }
                                    }
                                    if (boardState.gamePiece[x1, y1].team == myTeam)
                                    {
                                        break;
                                    }
                                }

                            }
                            for (int i = 1; i < 8; i++)
                            {
                                int x1 = x - i;
                                int y1 = y + i;
                                if (y1 < 8 && x1 > -1)
                                {

                                    if (boardState.gamePiece[x1, y1].team != myTeam)
                                    {
                                        boardState.gameBoard[x1, y1].threatPlayerTwo = true;
                                        if (boardState.gamePiece[x1, y1].team != myTeam && boardState.gamePiece[x1, y1].type != 0)
                                        {
                                            break;
                                        }
                                    }
                                    if (boardState.gamePiece[x1, y1].team == myTeam)
                                    {
                                        break;
                                    }
                                }

                            }
                        }
                        #endregion

                        #region Queen Movement
                        if (boardState.gamePiece[x, y].type == 5)
                        {
                            //orthaganols  
                            for (int i = x + 1; i < 8; i++)
                            {
                                if (boardState.gamePiece[i, y].type == 0)
                                {
                                    boardState.gameBoard[i, y].threatPlayerTwo = true;
                                }

                                if (boardState.gamePiece[i, y].team != myTeam && boardState.gamePiece[i, y].type != 0)
                                {
                                    boardState.gameBoard[i, y].threatPlayerTwo = true;
                                    break;
                                }

                                if (boardState.gamePiece[i, y].team == myTeam)
                                {
                                    break;
                                }
                            }
                            for (int i = x - 1; i > -1; i--)
                            {
                                if (boardState.gamePiece[i, y].type == 0)
                                {
                                    boardState.gameBoard[i, y].threatPlayerTwo = true;
                                }

                                if (boardState.gamePiece[i, y].team != myTeam && boardState.gamePiece[i, y].type != 0)
                                {
                                    boardState.gameBoard[i, y].threatPlayerTwo = true;
                                    break;
                                }

                                if (boardState.gamePiece[i, y].team == myTeam)
                                {
                                    break;
                                }
                            }
                            for (int i = y + 1; i < 8; i++)
                            {
                                if (boardState.gamePiece[x, i].type == 0)
                                {
                                    boardState.gameBoard[x, i].threatPlayerTwo = true;
                                }

                                if (boardState.gamePiece[x, i].team != myTeam && boardState.gamePiece[x, i].type != 0)
                                {
                                    boardState.gameBoard[x, i].threatPlayerTwo = true;
                                    break;
                                }

                                if (boardState.gamePiece[x, i].team == myTeam)
                                {
                                    break;
                                }
                            }
                            for (int i = y - 1; i > -1; i--)
                            {
                                if (boardState.gamePiece[x, i].type == 0)
                                {
                                    boardState.gameBoard[x, i].threatPlayerTwo = true;
                                }

                                if (boardState.gamePiece[x, i].team != myTeam && boardState.gamePiece[x, i].type != 0)
                                {
                                    boardState.gameBoard[x, i].threatPlayerTwo = true;
                                    break;
                                }

                                if (boardState.gamePiece[x, i].team == myTeam)
                                {
                                    break;
                                }
                            }
                            //Diagonals
                            {
                                for (int i = 1; i < 8; i++)
                                {
                                    int x1 = x + i;
                                    int y1 = y + i;
                                    if (y1 < 8 && x1 < 8)
                                    {

                                        if (boardState.gamePiece[x1, y1].team != myTeam)
                                        {
                                            boardState.gameBoard[x1, y1].threatPlayerTwo = true;
                                            if (boardState.gamePiece[x1, y1].team != myTeam && boardState.gamePiece[x1, y1].type != 0)
                                            {
                                                break;
                                            }
                                        }
                                        if (boardState.gamePiece[x1, y1].team == myTeam)
                                        {
                                            break;
                                        }
                                    }
                                }
                                for (int i = 1; i < 8; i++)
                                {
                                    int x1 = x + i;
                                    int y1 = y - i;
                                    if (y1 > -1 && x1 < 8)
                                    {

                                        if (boardState.gamePiece[x1, y1].team != myTeam)
                                        {
                                            boardState.gameBoard[x1, y1].threatPlayerTwo = true;
                                            if (boardState.gamePiece[x1, y1].team != myTeam && boardState.gamePiece[x1, y1].type != 0)
                                            {
                                                break;
                                            }
                                        }
                                        if (boardState.gamePiece[x1, y1].team == myTeam)
                                        {
                                            break;
                                        }
                                    }
                                }
                                for (int i = 1; i < 8; i++)
                                {
                                    int x1 = x - i;
                                    int y1 = y - i;
                                    if (x1 > -1 && y1 > -1)
                                    {

                                        if (boardState.gamePiece[x1, y1].team != myTeam)
                                        {
                                            boardState.gameBoard[x1, y1].threatPlayerTwo = true;
                                            if (boardState.gamePiece[x1, y1].team != myTeam && boardState.gamePiece[x1, y1].type != 0)
                                            {
                                                break;
                                            }
                                        }
                                        if (boardState.gamePiece[x1, y1].team == myTeam)
                                        {
                                            break;
                                        }
                                    }

                                }
                                for (int i = 1; i < 8; i++)
                                {
                                    int x1 = x - i;
                                    int y1 = y + i;
                                    if (y1 < 8 && x1 > -1)
                                    {

                                        if (boardState.gamePiece[x1, y1].team != myTeam)
                                        {
                                            boardState.gameBoard[x1, y1].threatPlayerTwo = true;
                                            if (boardState.gamePiece[x1, y1].team != myTeam && boardState.gamePiece[x1, y1].type != 0)
                                            {
                                                break;
                                            }
                                        }
                                        if (boardState.gamePiece[x1, y1].team == myTeam)
                                        {
                                            break;
                                        }
                                    }
                                }
                            }
                        }
                        #endregion

                        #region King Movement //Todo threat check
                        if (boardState.gamePiece[x, y].type == 6)
                        {
                            for (int i = -1; i < 2; i++)
                            {
                                for (int j = -1; j < 2; j++)
                                {
                                    int x1 = x + i;
                                    int y1 = y + j;
                                    if (x1 > -1 && x1 < 8 && y1 > -1 && y1 < 8)
                                    {
                                        if (boardState.gamePiece[x1, y1].team != myTeam)
                                        {
                                            boardState.gameBoard[x1, y1].threatPlayerTwo = true;
                                        }
                                    }
                                }
                            }
                        }
                        #endregion
                    }
                }
        }
        animate.threatCheckReady = false;
    }//Update
}
