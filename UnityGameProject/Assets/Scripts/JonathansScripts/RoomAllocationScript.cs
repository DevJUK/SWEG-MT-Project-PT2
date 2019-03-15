using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class RoomAllocationScript : MonoBehaviour
{

	public const int NumberOfNPCS = 6;

	public List<string> NPCS = new List<string>(new string[]
		{
			"Cally", "Miranda", "PoliceMan", "Witch", "Cat", "Kyle"
		});

	public enum Room
	{	GrdEnt, GrdStr, GrdA, GrdB, GrdC, GrdD, GrdE, GrdF, GrdG,
		UppFoy, UppA, UppB, UppC, UppD, UppE, UppF, UppG, UppH, UppI, UppJ, UppK,
		BseLnd, BseA, BseB, BseC, BseD, BseE, BseF
	};

	public Room CallyRoom;
	public Room MirandaRoom;
	public Room PoliceManRoom;
	public Room WitchRoom;
	public Room CatRoom;
	public Room KyleRoom;

	public string[,] Locations = new string[NumberOfNPCS, 2];

	private void Start()
	{
		for (int i = 0; i < NumberOfNPCS; i++)
		{
			Locations[0, i] = NPCS[i];
		}

		for (int i = 0; i < NumberOfNPCS; i++)
		{
			Locations[1, i] = Room.GrdEnt.ToString();
		}
	}
}
