using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FurnaceRoomScript : MonoBehaviour
{

	public GameObject Player;
	public GameObject[] DoorsControl;
	public GameObject PlayerTpPos;
	public GameObject[] Fireballs;

	public float LockTimer = 60;
	public bool TimerRunning;

	public bool EventRun;

	public Text UIText;

	public bool HasTP;
	public float EveTimer = 7;
	public bool EveTimerRunning;



	private void Awake()
	{
		UIText = GetComponentInChildren<Text>();	
	}

	private void Start()
	{
		UIText.gameObject.SetActive(false);
	}


	void Update()
    {
		// Hides the lock timer UI
		HideLockTimer();



        if (TimerRunning)
		{
			// Shows the UI
			if (!UIText.gameObject.activeInHierarchy)
			{
				UIText.gameObject.SetActive(true);
			}

			// Lock Doors
			LockDoors();

			// Starts the timer
			LockTimer -= Time.deltaTime;




			// Move player to position to get hit by fire
			if (!HasTP)
			{
				Player.transform.position = PlayerTpPos.transform.position;
				PlayerTpPos.SetActive(false);

				if (Player.transform.rotation.y != -90)
				{
					Player.transform.localRotation = new Quaternion(0, 180, 0, 0);
				}

				Player.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezePositionZ;
				Player.GetComponent<Mouse_Move>().enabled = false;
				EveTimerRunning = true;

				HasTP = true;
			}


			EveTimer -= Time.deltaTime;

			if (EveTimer < 0)
			{
				EveTimer = 0;
				EveTimerRunning = false;
				Player.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotation;
				Player.GetComponent<Mouse_Move>().enabled = true;
			}

			if ((EveTimerRunning) && (EveTimer < 5f) && (!Fireballs[0].GetComponent<FireBallScript>().Fire))
			{
				Fireballs[0].GetComponent<FireBallScript>().Fire = true;
			}
			else if ((EveTimerRunning) && (EveTimer < 1f) && (!Fireballs[1].GetComponent<FireBallScript>().Fire))
			{
				Fireballs[1].GetComponent<FireBallScript>().Fire = true;
			}

		}

		UIText.text = "Room Locked: " + Mathf.FloorToInt(LockTimer / 60).ToString("0") + ":" + Mathf.FloorToInt(LockTimer % 60).ToString("00");
    }


	private void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.tag == "Player")
		{
			if (!EventRun) { EventRun = true; TimerRunning = true; }
		}
	}

	private void HideLockTimer()
	{
		if (LockTimer < .15f)
		{
			if (UIText.gameObject.activeInHierarchy)
			{
				UIText.gameObject.SetActive(false);
			}

			foreach (GameObject G in DoorsControl)
			{
				G.GetComponent<DoorScript>().Locked = false;
			}

			TimerRunning = false;
		}
	}

	private void LockDoors()
	{
		//Closes and locks the doors to the room
		foreach (GameObject G in DoorsControl)
		{
			if (G.GetComponent<DoorScript>().DoorOpen)
			{
				G.GetComponent<DoorScript>().DoorOpen = false;

				foreach (GameObject GG in G.GetComponent<DoorScript>().Doors)
				{
					GG.GetComponentInChildren<Animator>().SetTrigger("DoorClosed");
				}
			}

			G.GetComponent<DoorScript>().Locked = true;
		}
	}
}
