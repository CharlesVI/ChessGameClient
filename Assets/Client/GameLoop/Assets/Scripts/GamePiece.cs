using UnityEngine;
public class GamePiece
{
    //This will let the server know who may move these peices
    public int team;

    public int type = 0;


    //This will let me know if the peice is captured
    public int captured = 0;
    public int moved = 0;


    public GamePiece(int team, int type, int captured, int moved)
    {
        /**First the line GamePiece bla = new GamePiece();
            that create an instance of this class. Having the class
         * create itelf it a bit silly. in fact if you look at the end of the initialiser
         * GamePiece() you will see it is the same as the constructor. that is because you are CALLING
         * the constructor method. so basically that is done OUTSIDE of this class.
         * here your only job is to take in the parameters given above and put those temporary variables into the 
         * perminant ones above. so all you have to do it this:
        */

        this.team = team;
        this.type = type;
        this.captured = captured;
        this.moved = moved;

        //also returning it is not needed as you are just creating it for the first time.

    }//constructor

    public override string ToString()
    {
        return "\nTeam" + team + "\nType" + type + "\nCaptured" + captured + "\nMoved" + moved;
    }


}//class
//namespace