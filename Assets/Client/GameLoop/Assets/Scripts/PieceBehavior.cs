using UnityEngine;
using System.Collections;

public class PieceBehavior : MonoBehaviour {


    //Varibles Start *************************************************************

    GameObject networkManager;
    GetInput getInput;
    BoardState boardState;
    Animate animate;

    Color myColor;

    public Renderer renderer;


    //Varibles End ***************************************************************
	// Use this for initialization
	void Start () 
    {
        networkManager = GameObject.Find("NetworkManager");
        getInput = networkManager.GetComponent<GetInput>();
        boardState = networkManager.GetComponent<BoardState>();
        animate = networkManager.GetComponent<Animate>();
	}
	
	// Update is called once per frame
	void Update ()
    {

        #region Coloring
        if (boardState.gamePiece[(int)this.transform.position.x / 10, (int)this.transform.position.z / 10].team == 1 && boardState.gamePiece[(int)this.transform.position.x / 10, (int)this.transform.position.z / 10].moved == 0)
        {
            renderer.material.color = Color.red;
        }

        if (boardState.gamePiece[(int)this.transform.position.x / 10, (int)this.transform.position.z / 10].team == 2 && boardState.gamePiece[(int)this.transform.position.x / 10, (int)this.transform.position.z / 10].moved == 0)
        {
            renderer.material.color = Color.blue;
        }

        if (boardState.gamePiece[(int)this.transform.position.x / 10, (int)this.transform.position.z / 10].moved == 1)
        {
            if (boardState.gamePiece[(int)this.transform.position.x / 10, (int)this.transform.position.z / 10].team == 2)
            {
                renderer.material.color = Color.grey;
                renderer.material.color = renderer.material.color + (Color.blue / 4);
            }

            if (boardState.gamePiece[(int)this.transform.position.x / 10, (int)this.transform.position.z / 10].team == 1)
            {
                renderer.material.color = Color.grey;
                renderer.material.color = renderer.material.color + (Color.red / 4);
            }
        }
        #endregion


        if (animate.p1Castle && animate.p1CastleOrigin == this.transform.position)
        {
            this.transform.position = animate.p1CastleDestination;
            animate.p1Castle = false;
        }

        if (animate.p2Castle && animate.p2CastleOrigin == this.transform.position)
        {
            this.transform.position = animate.p2CastleDestination;
            animate.p2Castle = false;
        }

        if (animate.enPassantOne && animate.enPassantPositionOne == this.transform.position)
        {
            Destroy(gameObject);
            animate.enPassantOne = false;
        }

        if (animate.enPassantTwo && animate.enPassantPositionTwo == this.transform.position)
        {
            Destroy(gameObject);
            animate.enPassantTwo = false;
        }


        //Capture
        if (animate.p1Captured == true && animate.p1Destination == this.transform.position)
        {
            Destroy(gameObject);

            animate.p1Captured = false;
        }

        if (animate.p2Captured == true && animate.p2Destination == this.transform.position)
        {
            Destroy(gameObject);

            animate.p2Captured = false;
        }

        //MoveMent
        if (animate.p1Origin == this.transform.localPosition && animate.p1Captured == false)
        {
            Debug.Log("Piece 1 moved");
            this.transform.position = animate.p1Destination;
            animate.readyToAnimateTwo = false;

        }

        if (animate.p2Origin == this.transform.localPosition && animate.p2Captured == false)
        {
            Debug.Log("Piece 2 moved");
            this.transform.position = animate.p2Destination;
            animate.readyToAnimateTwo = false;

        }

        if (animate.promoteReadyP1 == true && this.transform.position == animate.promoteLocationP1)
        {
            Debug.Log("Promote kill Attempted");
            Destroy(gameObject);
            animate.promoteReadyP1 = false;
            animate.spawnPromotionP1 = true;
        }

        if (animate.promoteReadyP2 == true && this.transform.position == animate.promoteLocationP2)
        {
            Debug.Log("Promote kill Attempted");
            Destroy(gameObject);
            animate.promoteReadyP2 = false;
            animate.spawnPromotionP2 = true;
        }

	}//Update


    void OnMouseUpAsButton() //Add ownership test.
    {
        if (boardState.gamePiece[(int)this.transform.position.x / 10, (int)this.transform.position.z / 10].team == getInput.myTeam &&
                boardState.gamePiece[(int)this.transform.position.x / 10, (int)this.transform.position.z / 10].moved == 0 && getInput.moveConfirmed == false)
        {
            getInput.pieceType = boardState.gamePiece[(int)this.transform.position.x / 10, (int)this.transform.position.z / 10].type;
            getInput.pieceSelected = true;
            getInput.tileSelected = false;
            getInput.xOrigin = (int)this.transform.position.x / 10;
            getInput.yOrigin = (int)this.transform.position.z / 10;;
            Debug.Log("Clicked " + boardState.gamePiece[(int)this.transform.position.x / 10, (int)this.transform.position.z / 10].type + "!");

            for (int i = 0; i < 8; i++) for (int j = 0; j < 8; j++)
            {
                boardState.gameBoard[i, j].tileSelected = false;
            }
        }
    }
}
