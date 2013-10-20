using UnityEngine;
/// <summary>
/// This script is to Make a class for the player database. This Database will be used to register and track the two players.
/// 
/// A class is more or less a custom varible that contains alot of stuff.
/// </summary>

public class PlayerDataClass
{ 

	//Varibles Start ----------------------------------------------------
	
	public NetworkPlayer networkPlayer;
	
	public string playerName;
	
	public int team;
	
	//Varibles End -----------------------------------------------------
	
	
	public PlayerDataClass Constructor ()
	{
		PlayerDataClass player = new PlayerDataClass();
	
		player.networkPlayer = networkPlayer;

		player.playerName = playerName;

		player.team = team;

		return player;
	}
}




