using UnityEngine;
using System.Collections;

public class TileBehavior : MonoBehaviour {

    //Varibles Start *********************************************************************************

    GameObject networkManager;
    BoardState boardState;
    GetInput getInput;

    Color defaultColor;

    int x;
    int y;

    Color colorPlayerOne;
    Color colorPlayerTwo;

    //Varibles End ***********************************************************************************
	
    // Use this for initialization
	void Start () 
    {
        //This is so I can access the array and find out what the tile ought to be doing.
        networkManager = GameObject.Find("NetworkManager");
        boardState = networkManager.GetComponent<BoardState>();
        getInput = networkManager.GetComponent<GetInput>();

        defaultColor = this.renderer.material.color;

        x = (int)this.transform.position.x / 10;
        y = (int)this.transform.position.z / 10;

        //TODO Grab this info from the player / Network
        colorPlayerOne = Color.red;
        colorPlayerTwo = Color.blue;

	}
	
	// Update is called once per frame
	void Update () 
    {
        if (boardState.gameBoard[x, y].movePossible == false)
        {
            this.renderer.material.color = defaultColor;
        }

        if (boardState.gameBoard[x, y].movePossible == true)
        {
            this.renderer.material.color = Color.blue;
        }
        if (boardState.gameBoard[x, y].tileSelected == true)
        {
            this.renderer.material.color = Color.green;
        }

        if (boardState.gameBoard[x, y].threatPlayerOne == true
            && boardState.gameBoard[x, y].tileSelected == false && boardState.gameBoard[x, y].movePossible == false)
        {

            this.renderer.material.color = defaultColor/2 + (colorPlayerOne / 2);

        }

        if (boardState.gameBoard[x, y].threatPlayerTwo == true
            && boardState.gameBoard[x, y].tileSelected == false && boardState.gameBoard[x, y].movePossible == false)
        {

             this.renderer.material.color = defaultColor/2 + (colorPlayerTwo / 2); 
        }

        if (boardState.gameBoard[x, y].threatPlayerOne == true && boardState.gameBoard[x, y].threatPlayerTwo == true
            && boardState.gameBoard[x, y].tileSelected == false && boardState.gameBoard[x, y].movePossible == false)
        {

             this.renderer.material.color = defaultColor/4 + (colorPlayerTwo / 2) + (colorPlayerOne / 2);

        }
	}


    void OnMouseUpAsButton()
    {
        Debug.Log("Clicked tile");
        for (int i = 0; i < 8; i++) for (int j = 0; j < 8; j++)
        {
            boardState.gameBoard[i, j].tileSelected = false;
        }
        if(boardState.gameBoard[x,y].movePossible == true)
        {
            boardState.gameBoard[x, y].tileSelected = true;
            getInput.tileSelected = true;
            getInput.xDestination = (int)this.transform.position.x / 10;
            getInput.yDestination = (int)this.transform.position.z / 10;
        }
    }
}
