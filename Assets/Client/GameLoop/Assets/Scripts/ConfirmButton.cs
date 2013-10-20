using UnityEngine;
using System.Collections;

public class ConfirmButton : MonoBehaviour
{
    //**Varibles Start**********************************************

    GameObject networkManager;
    GetInput getInput;
    BoardState boardState;
    Animate animate;

    //**Varibles End************************************************

	// Use this for initialization
	void Start () 
    {
        networkManager = GameObject.Find("NetworkManager");
        getInput = networkManager.GetComponent<GetInput>();
        boardState = networkManager.GetComponent<BoardState>();
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (getInput.pieceSelected == true && getInput.tileSelected == true && getInput.moveConfirmed == false)
        {
            //Enable button
        }

	}

    void OnMouseUpAsButton()
    {
        if (getInput.tileSelected == true && getInput.moveConfirmed == false)
        {
            Debug.Log("Move confirmed button clicked");

            getInput.moveConfirmed = true;

            for (int i = 0; i < 8; i++) for (int j = 0; j < 8; j++)
            {
                boardState.gameBoard[i, j].tileSelected = false;
                boardState.gameBoard[i, j].movePossible = false;
            }
        }
    }
}
