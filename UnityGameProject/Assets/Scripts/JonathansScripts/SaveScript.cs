using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// new using stuff :)
using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;


public class SaveScript : MonoBehaviour
{

	public GameObject Player;


	private void Start()
	{
		Player = GameObject.FindGameObjectWithTag("Player");
	}


	public void SaveGame()
	{
		// does stuff with binrary one assumes :)
		BinaryFormatter BinFormat = new BinaryFormatter();

		// creates a file in the data path /C/users/appdata/......
		FileStream DataFile = File.Create(Application.persistentDataPath + "/gamedata.dat");

		// creates an instance of the game data class...
		GameData Data = new GameData();

		// populating the new instance with the current values in the game

		// Player Data
		Data.PlayerPos = Player.transform.position;

		// Starts Data
		Data.Health = Player.GetComponentInChildren<Stats>().Health;
		Data.Sanity = Player.GetComponentInChildren<Stats>().Sanity;
		Data.Strenght = Player.GetComponentInChildren<Stats>().Strength;
		Data.Agility = Player.GetComponentInChildren<Stats>().Agility;
		Data.Intelligence = Player.GetComponentInChildren<Stats>().Intelligence;
		Data.Willpower = Player.GetComponentInChildren<Stats>().Willpower;
		Data.Perception = Player.GetComponentInChildren<Stats>().Perception;
		Data.Charisma = Player.GetComponentInChildren<Stats>().Charisma;


		// Converts to binrary, using the data from the data thingy in a data file
		BinFormat.Serialize(DataFile, Data);

		// Closes the data file
		DataFile.Close();
	}


	public void LoadGame()
	{
		// checks to see if the file exsists, duh...
		if (File.Exists(Application.persistentDataPath + "/gamedata.dat"))
		{
			BinaryFormatter BinFormat = new BinaryFormatter();

			// Makes a local file with the file in the location and opens it :)
			FileStream DataFile = File.Open(Application.persistentDataPath + "/gamedata.dat", FileMode.Open);

			// converts the file to readable thingys :) ( "unbinraryfys" the file )
			GameData Data = (GameData)BinFormat.Deserialize(DataFile);

			// Closes the file
			DataFile.Close();


			// Sets the values to the values that were in the file

			// Player Data
			Player.transform.position = Data.PlayerPos;

			// Stats Data
			Player.GetComponentInChildren<Stats>().Health = Data.Health;
			Player.GetComponentInChildren<Stats>().Sanity = Data.Sanity;
			Player.GetComponentInChildren<Stats>().Strength = Data.Strenght;
			Player.GetComponentInChildren<Stats>().Agility = Data.Agility;
			Player.GetComponentInChildren<Stats>().Intelligence = Data.Intelligence;
			Player.GetComponentInChildren<Stats>().Willpower = Data.Willpower;
			Player.GetComponentInChildren<Stats>().Perception = Data.Perception;
			Player.GetComponentInChildren<Stats>().Charisma = Data.Charisma;

			//ControllerScript.ReactorTotalPoints = Data.Score;


		}

		// Loads the level scene again
		//ControllerScript.ChangeLevelState(GameStates.Level);
	}


}

// holds things we wanna save :)
[Serializable]
class GameData
{
	//public float Score;
	public Vector3 PlayerPos;

	// Starts Saving
	public int Health;
	public int Sanity;
	public int Strenght;
	public int Agility;
	public int Intelligence;
	public int Willpower;
	public int Perception;
	public int Charisma;

}