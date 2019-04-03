using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
// new using stuff :)
using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;


public class SaveScript : MonoBehaviour
{

	public GameObject Player;
	public RoomAllocationScript RAS;
	public InventoryScript Invent;


	private void Awake()
	{
		DontDestroyOnLoad(this);
	}

	private void Start()
	{
		if (GameObject.FindGameObjectWithTag("Player")) { Player = GameObject.FindGameObjectWithTag("Player"); }
		if (FindObjectOfType<RoomAllocationScript>()) { RAS = FindObjectOfType<RoomAllocationScript>(); }
		if (FindObjectOfType<InventoryScript>()) { Invent = FindObjectOfType<InventoryScript>(); }
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
		//Data.PlayerPos = Player.transform.position;

		//// Starts Data
		Data.Health = Player.GetComponentInChildren<Stats>().Health;
		Data.Sanity = Player.GetComponentInChildren<Stats>().Sanity;
		Data.Strenght = Player.GetComponentInChildren<Stats>().Strength;
		Data.Agility = Player.GetComponentInChildren<Stats>().Agility;
		Data.Intelligence = Player.GetComponentInChildren<Stats>().Intelligence;
		Data.Willpower = Player.GetComponentInChildren<Stats>().Willpower;
		Data.Perception = Player.GetComponentInChildren<Stats>().Perception;
		Data.Charisma = Player.GetComponentInChildren<Stats>().Charisma;

		// Room Data
		Data.PlayerRoom = RAS.Locations[0, 1];
		Data.MirandaRoom = RAS.Locations[1, 1];
		Data.PolicemanRoom = RAS.Locations[2, 1];
		Data.WitchRoom = RAS.Locations[3, 1];
		Data.CatRoom = RAS.Locations[4, 1];
		Data.KyleRoom = RAS.Locations[5, 1];

		// Invert Data
		//Data.Item0 = Invent.items[0].ItemObject;
		//Data.Item1 = Invent.items[1].ItemObject;
		//Data.Item2 = Invent.items[2].ItemObject;
		//Data.Item3 = Invent.items[3].ItemObject;
		//Data.Item4 = Invent.items[4].ItemObject;
		//Data.Item5 = Invent.items[5].ItemObject;
		//Data.Item6 = Invent.items[6].ItemObject;
		//Data.Item7 = Invent.items[7].ItemObject;
		//Data.Item8 = Invent.items[8].ItemObject;
		//Data.Item9 = Invent.items[9].ItemObject;

		//Data.Image0 = Invent.ItemImages[0].sprite;
		//Data.Image1 = Invent.ItemImages[1].sprite;
		//Data.Image2 = Invent.ItemImages[2].sprite;
		//Data.Image3 = Invent.ItemImages[3].sprite;
		//Data.Image4 = Invent.ItemImages[4].sprite;
		//Data.Image5 = Invent.ItemImages[5].sprite;
		//Data.Image6 = Invent.ItemImages[6].sprite;
		//Data.Image7 = Invent.ItemImages[7].sprite;
		//Data.Image8 = Invent.ItemImages[8].sprite;
		//Data.Image9 = Invent.ItemImages[9].sprite;





		// Converts to binrary, using the data from the data thingy in a data file
		BinFormat.Serialize(DataFile, Data);
		//DataFile.Flush();

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

			DataFile.Seek(0, SeekOrigin.Begin);

			// converts the file to readable thingys :) ( "unbinraryfys" the file )
			GameData Data = (GameData)BinFormat.Deserialize(DataFile);

			// Closes the file
			DataFile.Close();


			// Sets the values to the values that were in the file

			Debug.LogWarning(Data.PlayerRoom);

			switch (Data.PlayerRoom)
			{
				case string a when a.Contains("Grd"):
					SceneManager.LoadSceneAsync("GroudFloorScene");
					break;
				case string a when a.Contains("Upp"):
					SceneManager.LoadSceneAsync("UpperFloorScene");
					break;
				case string a when a.Contains("Bse"):
					SceneManager.LoadSceneAsync("BasementScene");
					break;
				default:
					break;
			}


			// Player Data
			//Player.transform.position = Data.PlayerPos;

			//// Stats Data
			Player.GetComponentInChildren<Stats>().Health = Data.Health;
			Player.GetComponentInChildren<Stats>().Sanity = Data.Sanity;
			Player.GetComponentInChildren<Stats>().Strength = Data.Strenght;
			Player.GetComponentInChildren<Stats>().Agility = Data.Agility;
			Player.GetComponentInChildren<Stats>().Intelligence = Data.Intelligence;
			Player.GetComponentInChildren<Stats>().Willpower = Data.Willpower;
			Player.GetComponentInChildren<Stats>().Perception = Data.Perception;
			Player.GetComponentInChildren<Stats>().Charisma = Data.Charisma;

			//ControllerScript.ReactorTotalPoints = Data.Score;

			// Room Data
			RAS.Locations[0, 1] = Data.PlayerRoom;
			RAS.Locations[1, 1] = Data.MirandaRoom;
			RAS.Locations[2, 1] = Data.PolicemanRoom;
			RAS.Locations[3, 1] = Data.WitchRoom;
			RAS.Locations[4, 1] = Data.CatRoom;
			RAS.Locations[5, 1] = Data.KyleRoom;

			// Invent Data
			//Invent.items[0].ItemObject = Data.Item0;
			//Invent.items[1].ItemObject = Data.Item1;
			//Invent.items[2].ItemObject = Data.Item2;
			//Invent.items[3].ItemObject = Data.Item3;
			//Invent.items[4].ItemObject = Data.Item4;
			//Invent.items[5].ItemObject = Data.Item5;
			//Invent.items[6].ItemObject = Data.Item6;
			//Invent.items[7].ItemObject = Data.Item7;
			//Invent.items[8].ItemObject = Data.Item8;
			//Invent.items[9].ItemObject = Data.Item9;

			//Invent.ItemImages[0].sprite = Data.Image0;
			//Invent.ItemImages[1].sprite = Data.Image1;
			//Invent.ItemImages[2].sprite = Data.Image2;
			//Invent.ItemImages[3].sprite = Data.Image3;
			//Invent.ItemImages[4].sprite = Data.Image4;
			//Invent.ItemImages[5].sprite = Data.Image5;
			//Invent.ItemImages[6].sprite = Data.Image6;
			//Invent.ItemImages[7].sprite = Data.Image7;
			//Invent.ItemImages[8].sprite = Data.Image8;
			//Invent.ItemImages[9].sprite = Data.Image9;



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
	//public Vector3 PlayerPos;

	// Starts Saving
	public int Health;
	public int Sanity;
	public int Strenght;
	public int Agility;
	public int Intelligence;
	public int Willpower;
	public int Perception;
	public int Charisma;

	// Rooms in
	public string PlayerRoom;
	public string MirandaRoom;
	public string PolicemanRoom;
	public string KyleRoom;
	public string WitchRoom;
	public string CatRoom;

	// Invent Data
	//public GameObject Item0;
	//public GameObject Item1;
	//public GameObject Item2;
	//public GameObject Item3;
	//public GameObject Item4;
	//public GameObject Item5;
	//public GameObject Item6;
	//public GameObject Item7;
	//public GameObject Item8;
	//public GameObject Item9;

	//public Sprite Image0;
	//public Sprite Image1;
	//public Sprite Image2;
	//public Sprite Image3;
	//public Sprite Image4;
	//public Sprite Image5;
	//public Sprite Image6;
	//public Sprite Image7;
	//public Sprite Image8;
	//public Sprite Image9;
}