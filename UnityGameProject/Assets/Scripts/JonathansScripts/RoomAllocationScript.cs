using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class RoomAllocationScript : MonoBehaviour
{

	public const int NumberOfNPCS = 6;

	public string[] NPCS = new string[]
		{
			"Cally", "Miranda", "PoliceMan", "Witch", "Cat", "Kyle"
		};

	public string[] Rooms = new string[]
	{
		"GrdEnt", "GrdStr", "GrdA", "GrdB", "GrdC", "GrdD", "GrdE", "GrdF", "GrdG",
		"UppFoy", "UppA", "UppB", "UppC", "UppD", "UppE", "UppF", "UppG", "UppH", "UppI", "UppJ", "UppK",
		"BseLnd", "BseA", "BseB", "BseC", "BseD", "BseE", "BseF"
	};

	public string[,] Locations = new string[NumberOfNPCS, 2];

	private void Start()
	{
		DontDestroyOnLoad(this);

		for (int i = 0; i < NumberOfNPCS; i++)
		{
			Locations[i, 0] = NPCS[i];
		}

		for (int i = 0; i < NumberOfNPCS; i++)
		{
			Locations[i, 1] = "Null";
		}
	}
}
