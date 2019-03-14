using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FurnaceRoomScript : MonoBehaviour
{

	public GameObject Player;
	public GameObject[] Doors;


	public float LockTimer = 60;
	public bool TimerRunning;



    void Start()
    {
        
    }


    void Update()
    {

		if (LockTimer < 0)
		{
			TimerRunning = false;
		}

        if (TimerRunning)
		{
			LockTimer -= Time.deltaTime;

			foreach (GameObject G in Doors)
			{
				G.GetComponent<DoorScript>().Locked = true;

				if (G.GetComponent<DoorScript>().DoorOpen)
				{
					G.GetComponentInChildren<Animator>().SetTrigger("DoorClosed");
				}

				//G.GetComponent<DoorScript>().DoorOpen = false;
			}
		}
    }


	private void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.tag == "Player")
		{
			if (!TimerRunning) { TimerRunning = true; }
		}
	}
}
