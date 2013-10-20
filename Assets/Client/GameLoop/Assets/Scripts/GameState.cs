using UnityEngine;
using System.Collections;

public class GameState : MonoBehaviour {

    //Varibles Start ++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++

    float native_width = 1920.0f;
    float native_height = 1080.0f;

    private GUIStyle winStyle = new GUIStyle();
    GUIStyle promoStyle = new GUIStyle();

    bool gameOver;
    int win;

    public bool promotion;

    GameObject networkManager;
    GetInput getInput;

    float widthPromoButton = 100;
    float heightPromoButton = 50;
    float topPromoButton = 40;

    //Varibles End ++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++

	// Use this for initialization
	void Start () 
    {
        networkManager = GameObject.Find("NetworkManager");
        getInput = networkManager.GetComponent<GetInput>();

        winStyle.fontSize = 180;
        winStyle.normal.textColor = new Color(200,150,0);
        winStyle.fontStyle = FontStyle.Bold;
        winStyle.alignment = TextAnchor.MiddleCenter;

        promoStyle.fontSize = 30;
        winStyle.alignment = TextAnchor.MiddleCenter;



	}
	
	// Update is called once per frame
	void Update () 
    {
	

	}

    void OnGUI()
    {

        float rx = Screen.width / native_width;
        float ry = Screen.height / native_height;
        Vector3 scale = new Vector3(rx, ry, 1);
        GUI.matrix = Matrix4x4.TRS(Vector3.zero, Quaternion.identity, scale); 
        
        
        if (promotion)
        {
            Rect windowRect = new Rect(native_width/3, 20, 550, 100);
            
            windowRect = GUI.Window(0, windowRect, DoMyWindow, "Select promotion");
        }
    
        if (gameOver)
        {
            if (win == 1)
            {
                GUI.Box(new Rect(0, 0, native_width, native_height), "");

                GUI.Box(new Rect(0, 0, native_width, native_height), "Victory!", winStyle);
            }
        }
    }

    void DoMyWindow(int windowID)
    {
        if (GUI.Button(new Rect(10, topPromoButton, widthPromoButton, heightPromoButton), "Pawn", promoStyle))
        {
            Debug.Log("Pawn Chooesen");
            getInput.promotionChoice = 1;
            promotion = false;          
        }

        if (GUI.Button(new Rect(110, topPromoButton, widthPromoButton, heightPromoButton), "Rook", promoStyle))
        {
            Debug.Log("Rook");
            getInput.promotionChoice = 2;
            promotion = false;
        }

        if (GUI.Button(new Rect(220, topPromoButton, widthPromoButton, heightPromoButton), "Knight", promoStyle))
        {
            Debug.Log("Knight");
            getInput.promotionChoice = 3;
            promotion = false;
        }

        if (GUI.Button(new Rect(330, topPromoButton, widthPromoButton, heightPromoButton), "Bishop", promoStyle))
        {
            Debug.Log("Bishop");
            getInput.promotionChoice = 4;
            promotion = false;
        }

        if (GUI.Button(new Rect(440, topPromoButton, widthPromoButton, heightPromoButton), "Queen", promoStyle))
        {
            Debug.Log("Queen");
            getInput.promotionChoice = 5;
            promotion = false;
        }   

    }

    
    [RPC]
    void GameOver(int winner)
    {
        gameOver = true;
        win = winner;
    }
}
