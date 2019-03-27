using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class SteveScript : MonoBehaviour
{
	[Header("Reference to the player")]
	public GameObject Player;

	[Header("Reference to the pickup popup text")]
	public Text ZeText;

	// Booleans that contorl this script
	private bool IsCollected;
	private bool IsPosed;
	private bool IsPickup;
	private bool IsCoRunning;

	// Array of strings used in the text popup
	private string[] Talk = new string[] { "Book: take me with you", "Cally: whats your name", "Steve: My name is Steve" };


	private void Update()
	{
		// Starts Co (ensures it only gets called once with a bool)
		if ((IsCollected) && (!IsCoRunning))
		{
			StartCoroutine(Introductions());
		}

		// Checks to see if the book has been collected and positioned behind the player
		if (IsCollected && IsPosed)
		{
			transform.SetParent(Player.transform);
		}
		// Checks to see if the book has been collected BUT not positioned behind the player
		else if (IsCollected && !IsPosed)
		{
			transform.position = new Vector3(Player.transform.position.x + .25f, Player.transform.position.y + 1.5f, Player.transform.position.z - .5f);
			IsPosed = true;
		}

		// Checks to see if the pickup button has been pressed
		if (Input.GetButton("Pickup") && !IsPickup)
		{
			IsPickup = true;
		}
	}


	private void OnTriggerEnter(Collider other)
	{
		IsPickup = false;
	}


	private void OnTriggerStay(Collider other)
	{
		if ((other.gameObject.tag == "Player") && (!IsCollected))
		{
			ZeText.text = Talk[0];

			if (IsPickup)
			{
				IsCollected = !IsCollected;
			}
		}
	}

	private void OnTriggerExit(Collider other)
	{
		if (ZeText.text == Talk[0])
		{
			ZeText.text = "";
		}
	}

	// Shows the introduction text when called as well as disables the box trigger as its not needed anymore
	internal IEnumerator Introductions()
	{
		Debug.LogWarning("Co Running");
		GetComponent<BoxCollider>().enabled = false;
		IsCoRunning = true;
		ZeText.text = Talk[1];
		yield return new WaitForSeconds(3f);
		ZeText.text = Talk[2];
		yield return new WaitForSeconds(3f);
		ZeText.text = "";
	}
}
