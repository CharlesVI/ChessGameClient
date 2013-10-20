public class GameBoard
{
    public bool movePossible;

    public bool tileSelected;

    public bool threatPlayerOne;

    public bool threatPlayerTwo;
  
    public GameBoard(bool movePossible, bool tileSelected, bool threatPlayerOne, bool threatPlayerTwo)
    {
        this.movePossible = movePossible;

        this.tileSelected = tileSelected;

        this.threatPlayerOne = threatPlayerOne;

        this.threatPlayerTwo = threatPlayerTwo;

        //also returning it is not needed as you are just creating it for the first time.
    }//constructor

    public override string ToString()
    {
        return "\nMovePossible" + movePossible + "\nTileSelected" + tileSelected + "\nThreatPlayerOne" + threatPlayerOne + "\nThreatPlayerTwo" + threatPlayerTwo;
    }


}//class
//namespace