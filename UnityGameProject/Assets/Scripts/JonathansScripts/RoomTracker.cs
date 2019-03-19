using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomTracker : MonoBehaviour
{
	// RAS = Room Allocation System (just what I called this, its a weird name I know)
	public RoomAllocationScript RAS;
	public string Character;
	public int CharacterID;
	public string RoomIn;

	private void Start()
	{
		// Sets the character ID up to the array position
		SetCharacterID(RAS, Character);
	}

	// Checks which empty object holds the floor the tracked object is on
	private void OnCollisionEnter(Collision collision)
	{
		Debug.Log("Tracking Running");
		switch (collision.gameObject.transform.parent.transform.parent.name)
		{
			// Ground Floor Rooms
			case string a when a.Contains("=Entra"):
				RoomIn = "GrdEnt";
				RAS.Locations[CharacterID, 1] = "=GrdEnt";
				break;
			case string a when a.Contains("Grand"):
				RoomIn = "GrdStr";
				RAS.Locations[CharacterID, 1] = "GrdStr";
				break;
			case string a when a.Contains("=RoomA"):
				RoomIn = "GrdA";
				RAS.Locations[CharacterID, 1] = "GrdA";
				break;
			case string a when a.Contains("=RoomB"):
				RoomIn = "GrdB";
				RAS.Locations[CharacterID, 1] = "GrdB";
				break;
			case string a when a.Contains("=RoomC"):
				RoomIn = "GrdC";
				RAS.Locations[CharacterID, 1] = "GrdC";
				break;
			case string a when a.Contains("=RoomD"):
				RoomIn = "GrdD";
				RAS.Locations[CharacterID, 1] = "GrdD";
				break;
			case string a when a.Contains("=RoomE"):
				RoomIn = "GrdE";
				RAS.Locations[CharacterID, 1] = "GrdE";
				break;
			case string a when a.Contains("=RoomF"):
				RoomIn = "GrdF";
				RAS.Locations[CharacterID, 1] = "GrdF";
				break;
			case string a when a.Contains("=RoomG"):
				RoomIn = "GrdG";
				RAS.Locations[CharacterID, 1] = "GrdG";
				break;


			// Upper Floor Rooms
			case string a when a.Contains("^Upp"):
				RoomIn = "UppFoy";
				RAS.Locations[CharacterID, 1] = "UppFoy";
				break;
			case string a when a.Contains("^RoomA"):
				RoomIn = "UppA";
				RAS.Locations[CharacterID, 1] = "UppA";
				break;
			case string a when a.Contains("^RoomB"):
				RoomIn = "UppB";
				RAS.Locations[CharacterID, 1] = "UppB";
				break;
			case string a when a.Contains("^RoomD"):
				RoomIn = "UppD";
				RAS.Locations[CharacterID, 1] = "UppD";
				break;
			case string a when a.Contains("^RoomF"):
				RoomIn = "UppF";
				RAS.Locations[CharacterID, 1] = "UppF";
				break;
			case string a when a.Contains("^RoomG"):
				RoomIn = "UppG";
				RAS.Locations[CharacterID, 1] = "UppG";
				break;
			case string a when a.Contains("^RoomH"):
				RoomIn = "UppH";
				RAS.Locations[CharacterID, 1] = "UppH";
				break;
			case string a when a.Contains("^RoomI"):
				RoomIn = "UppI";
				RAS.Locations[CharacterID, 1] = "UppI";
				break;

			// Basement Rooms
			case string a when a.Contains("*Land"):
				RoomIn = "BseLnd";
				RAS.Locations[CharacterID, 1] = "BseLnd";
				break;
			case string a when a.Contains("*RoomA"):
				RoomIn = "BseA";
				RAS.Locations[CharacterID, 1] = "BseA";
				break;
			case string a when a.Contains("*RoomB"):
				RoomIn = "BseB";
				RAS.Locations[CharacterID, 1] = "BseB";
				break;
			case string a when a.Contains("*RoomC"):
				RoomIn = "BseC";
				RAS.Locations[CharacterID, 1] = "BseC";
				break;
			case string a when a.Contains("*RoomD"):
				RoomIn = "BseD";
				RAS.Locations[CharacterID, 1] = "BseD";
				break;
			case string a when a.Contains("*RoomE"):
				RoomIn = "BseE";
				RAS.Locations[CharacterID, 1] = "BseE";
				break;
			case string a when a.Contains("*RoomF"):
				RoomIn = "BseF";
				RAS.Locations[CharacterID, 1] = "BseF";
				break;
			default:
				break;
		}
	}


	private int SetCharacterID(RoomAllocationScript RA, string Test)
	{
		for (int i = 0; i < RA.NPCS.Length; i++)
		{
			if (RA.NPCS[i] == Test)
			{
				return i;
			}
		}

		return 0;
	}
}
