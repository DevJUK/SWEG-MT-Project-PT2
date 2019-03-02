using UnityEngine;
using UnityEngine.UI;

public class StatsUI : MonoBehaviour
{
	// Enum for the states for this script, allows it to be attached to all UI elements that need it without needing aditional scripts.
	public enum Stat
	{
		Health, Sanity, Strength, Agility, Intelligence, Willpower, Perception, Charisma, HealthText, SanityText
	};

	// Reference to enum for use in this script
	public Stat GetStat;

	// Elements that are needed to make this script work (are referenced in start)
	private Slider Slider;
	private Image Image;
	private Text Text;
	private Stats StatsScript;

	// Sets up an references that are needed for this script
	private void Start()
	{
		if (GameObject.FindGameObjectWithTag("Player"))
		{
			StatsScript = GameObject.FindGameObjectWithTag("Player").GetComponentInChildren<Stats>();
		}
		else
		{
			Debug.Log("There must be a Player tagged gameobject in the scene for the Stats UI to work");
		}

		if (GetComponent<Slider>())
		{
			Slider = GetComponent<Slider>();
		}

		if (GetComponent<Image>())
		{
			Image = GetComponent<Image>();
		}

		if (GetComponent<Text>())
		{
			Text = GetComponent<Text>();
		}
	}


	private void FixedUpdate()
	{
		UpdateStats();
	}


	// Updates the slider values to the values on the players stats
	public void UpdateStats()
	{
		switch (GetStat)
		{
			case Stat.Health:
				Image.fillAmount = StatsScript.Health / 10f;
				break;
			case Stat.Sanity:
				Image.fillAmount = StatsScript.Sanity / 10f;
				break;
			case Stat.Strength:
				Slider.value = StatsScript.Strength;
				break;
			case Stat.Agility:
				Slider.value = StatsScript.Agility;
				break;
			case Stat.Intelligence:
				Slider.value = StatsScript.Intelligence;
				break;
			case Stat.Willpower:
				Slider.value = StatsScript.Willpower;
				break;
			case Stat.Perception:
				Slider.value = StatsScript.Perception;
				break;
			case Stat.Charisma:
				Slider.value = StatsScript.Charisma;
				break;
			case Stat.HealthText:
				Text.text = StatsScript.Health.ToString();
				break;
			case Stat.SanityText:
				Text.text = StatsScript.Sanity.ToString();
				break;
			default:
				break;
		}
	}
}
