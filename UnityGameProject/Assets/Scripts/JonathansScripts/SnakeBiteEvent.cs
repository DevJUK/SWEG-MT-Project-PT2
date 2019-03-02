using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnakeBiteEvent : MonoBehaviour
{
	public GameObject Player;
	public GameObject Snake;
	public bool HasEventRun;
	public bool MoveSnake;

	private void Update()
	{
		if (MoveSnake)
		{
			SnakeMoveToPlayer();
		}
	}

	private void OnTriggerEnter(Collider other)
	{
		if ((other.gameObject.tag == "Player") && (!HasEventRun))
		{
			HasEventRun = true;
			Snake.SetActive(true);
			Player.GetComponent<Animator>().SetBool("IsWalking", false);
			Player.GetComponent<PlayerController>().enabled = false;
			MoveSnake = true;
		}
	}

	private void SnakeMoveToPlayer()
	{
		Snake.transform.position = Vector3.MoveTowards(Snake.transform.position, Player.transform.position, Time.deltaTime);
	}
}
