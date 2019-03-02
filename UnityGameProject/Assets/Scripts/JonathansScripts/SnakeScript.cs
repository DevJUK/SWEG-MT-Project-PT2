using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnakeScript : MonoBehaviour
{
	public GameObject Player;
	public SnakeBiteEvent BiteEventScript;
	private MonkeyScript MKScript;

	private void Start()
	{
		BiteEventScript = GameObject.Find("EventTrigger-RoomE").GetComponent<SnakeBiteEvent>();
		MKScript = GameObject.Find("Totally-A-Monkey").GetComponent<MonkeyScript>();
		Player = GameObject.FindGameObjectWithTag("Player");
	}

	private void OnCollisionEnter(Collision collision)
	{
		if (collision.gameObject.tag == "Player")
		{
			BiteEventScript.MoveSnake = false;
			MKScript.IsSnakeDone = true;
			Player.GetComponentInChildren<Stats>().Health -= 1;
			Player.GetComponentInChildren<PlayerController>().enabled = true;
			gameObject.SetActive(false);
		}
	}
}
