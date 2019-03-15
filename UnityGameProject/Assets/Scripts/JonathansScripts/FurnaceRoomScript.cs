using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FurnaceRoomScript : MonoBehaviour
{

	public GameObject Player;
	public GameObject[] DoorsControl;

	public float LockTimer = 60;
	public bool TimerRunning;

	public bool EventRun;

	public Text UIText;



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


        if (TimerRunning)
		{
			if (!UIText.gameObject.activeInHierarchy)
			{
				UIText.gameObject.SetActive(true);
			}

			LockTimer -= Time.deltaTime;

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

		UIText.text = "Room Locked: " + Mathf.FloorToInt(LockTimer / 60).ToString("0") + ":" + Mathf.FloorToInt(LockTimer % 60).ToString("00");
    }


	private void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.tag == "Player")
		{
			if (!EventRun) { EventRun = true; TimerRunning = true; }
		}
	}
}
