using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorScript : MonoBehaviour
{
    public bool Locked;
    public bool DoorOpen;
	public bool Called;
	public bool PlayerInTrigger;

	public List<GameObject> Doors;
    public GameObject DoorLockedUI;

    public float LockedTimer = 0.0f;

	private void Update()
	{
		if (Input.GetButton("Pickup"))
		{
			Debug.Log("Test");

			if ((!Called) && (PlayerInTrigger))
			{
				OpenDoor();
				Called = true;
			}
		}
	}


	public void OpenDoor()
    {
		if (Locked)
		{
			Debug.Log("The door is locked");

            // -- Oliver Addition Start -- 
            if (Input.GetButton("Pickup"))
            {
                DoorLockedUI.SetActive(true);
                LockedTimer = 5.0f;
            }

            if (LockedTimer <= 0.0f)
            {
                DoorLockedUI.SetActive(false);
            }
            // -- Oliver Addition End --
		}
		else
		{
			if (DoorOpen)
			{

				// Play closing animation

				// -- Jonathan Addition Start --
				foreach (GameObject G in Doors)
				{
					G.GetComponent<Animator>().SetTrigger("DoorClosed");
				}

				DoorOpen = false;
				Called = false;
				// -- Jonathan Addition End --
				Debug.Log("Door is Open, closing door");
			}

			else
			{
				// -- Jonathan Addition Start --
				foreach (GameObject G in Doors)
				{
					G.GetComponent<Animator>().SetTrigger("DoorOpened");
				}

				DoorOpen = true;
				Called = false;
				// -- Jonathan Addition End --
				// Play Opening animation
				Debug.Log("Door is closed, opening door");
			}
		}
    }

	// -- Jonathan Addition Start --
	private void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.tag == "Player")
		{
			PlayerInTrigger = true;
		}

	}

	private void OnTriggerExit(Collider other)
	{
		if (other.gameObject.tag == "Player")
		{
			PlayerInTrigger = false;
			Called = false;
		}

	}
	// -- Jonathan Addition End --
}
