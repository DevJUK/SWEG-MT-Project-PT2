using UnityEngine;

public class Stats : MonoBehaviour
{
	/*
	 *	Health & Sanity 
	*/

	public int Health { get; set; }
	public int Sanity { get; set; }

	/*
	 *	Remaining Stats 
	*/ 

	public int Strength { get; set; }
	public int Agility { get; set; }
	public int Intelligence { get; set; }
	public int Willpower { get; set; }
	public int Perception { get; set; }
	public int Charisma { get; set; }



	// Runs default setup
	private void Awake()
	{
		DefaultSetup();
	}


	private void Update()
	{
		Debug.Log(Health);
	}

	// Used for to set the stats to a default value of 50% filled and max health and sanity
	internal void DefaultSetup()
	{
		Health = 10;
		Sanity = 10;
		Strength = 1;
		Agility = 1;
		Intelligence = 1;
		Willpower = 1;
		Perception = 1;
		Charisma = 1;
	}


	// Used to setup the stats of a player or NPC to the entered values
	internal void CustomSetup(int HealthValue, int SanityValue, int StrValue, int AglValue, int IntValue, int WilValue, int PerValue, int ChaValue)
	{
		Health = HealthValue;
		Sanity = SanityValue;
		Strength = StrValue;
		Agility = AglValue;
		Intelligence = IntValue;
		Willpower = WilValue;
		Perception = PerValue;
		Charisma = ChaValue;
	}
}