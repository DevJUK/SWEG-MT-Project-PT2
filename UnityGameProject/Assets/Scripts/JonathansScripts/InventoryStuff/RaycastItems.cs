using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaycastItems : MonoBehaviour
{
	public float Range;
	public GameObject UI;

	public PickupUIText PickupScript;
	public ThrowableItemScript ThrowScript;

	// Anything to do with NPC's in this script was done by Ed so talk to him

	public NPCInteractionScrpt NPCInteractionScrpt; // the main controll script for npc dialogue interactions
	public MirrorTipScript MirrorTipScript; // Script used to tip the mirror (who'd of guessed it)
	public string NameofNPCHit; // Used to send the name of the NPC to the NPC interaction script

    // Added during agile by Ed --------------------------------------------
    public CharredRoomEventScrpt CharredRoomEventScrpt;
    public string Interact = "E - Interact";
    // ---------------------------------------------------------------------

	private void Start()
	{
		ThrowScript = GameObject.FindGameObjectWithTag("Player").GetComponentInChildren<ThrowableItemScript>();
		PickupScript = GameObject.Find("PickupPopup").GetComponent<PickupUIText>();
	}


	private void Update()
	{
		RaycastHit Hit;


		if (Physics.Raycast(transform.position, transform.forward, out Hit, LayerMask.NameToLayer("Interact"))) // Check to see if raycast hits anything
		{

			//Debug.Log(Hit.transform.gameObject.name);

			// Check to see if hit object is a npc or item
			if (Hit.transform.tag == "NPC") // if npc do this
			{
				// May need to change Name of model when using in actual game
				NPCInteractionScrpt = GameObject.Find("Cally V1 Model@Idle").GetComponent<NPCInteractionScrpt>();

				NameofNPCHit = Hit.transform.name; // getting the name of the NPC hit 
				Debug.Log("Raycast Hitting NPC");

				if (UI.activeInHierarchy) // might need work +++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
				{
					PickupScript.SetText(Hit.transform.gameObject.GetComponent<NPCDialogueScrpt>().NPCName);
				}

				if (Input.GetButtonDown("Pickup")) // E Key
				{

					NPCInteractionScrpt.StartInteraction(Hit.transform.gameObject);
					PickupScript.BlankText();

					if (Hit.transform.gameObject.GetComponent<ClassroomCutsceneController>())
					{
						Hit.transform.gameObject.GetComponent<ClassroomCutsceneController>().PlayCutscene();
					}
				}
			}

			if (Hit.transform.tag == "Mirror")
			{
				Debug.Log("Raycast hitting mirror");

				if (Input.GetButtonDown("Pickup")) // E Key
				{
					PickupScript.BlankText();
					MirrorTipScript.TipMirror();
				}
			}

            // Added in agile by ed -----------------------------------------
            if (Hit.transform.tag == "CharredMirror")
            {
                Debug.Log("Raycast hitting charred mirror");
                //PickupScript.SetText(Interact);

                if (Input.GetButtonDown("Pickup")) // E Key
                {
                    Debug.LogWarning("is this working");
                    CharredRoomEventScrpt.RunEvent();
                    PickupScript.BlankText();
                }
            }
            // -------------------------------------------------------------

			// Item Raycasting
			else
			{
				ItemRaycasts(Hit);
				BluePotionRaycast(Hit);
			}
		}
		else
		{
			//PickupScript.BlankText();
		}
	}



	private void AddHitToInv(RaycastHit Hit)
	{
		if (Hit.transform.gameObject.GetComponent<Item>())
		{
			Hit.transform.gameObject.GetComponent<Item>().AddToInv(Hit.transform.gameObject.GetComponent<Item>());
			Hit.transform.gameObject.transform.SetParent(GameObject.Find("InvItems").transform);
			Hit.transform.gameObject.SetActive(false);
			PickupScript.BlankText();
		}
	}


	private void ItemRaycasts(RaycastHit Hit)
	{
		// if there is an item been held by the player - then add it to the inventory when pcikup key is pressed again
		if ((ThrowScript.ItemHeld != null) && (Hit.transform.gameObject.GetComponent<Item>()))
		{
			PickupScript.SetText(Hit.collider.gameObject.GetComponent<Item>().ItemName + " | Throw (t) | Add to Inv (e)");

			if (Input.GetButtonDown("Pickup"))
			{
				AddHitToInv(Hit);
				ThrowScript.ItemHeld = null;
				PickupScript.BlankText();
			}
		}
		// if there isn't an item been held by the player but it has hit an item
		else if ((ThrowScript.ItemHeld == null) && (Hit.transform.gameObject.GetComponent<Item>()))
		{
			PickupScript.SetText(Hit.collider.gameObject.GetComponent<Item>().ItemName + " | Pickup (e)");

			if (Input.GetButtonDown("Pickup"))
			{
				ThrowScript.ItemHeld = Hit.transform.gameObject;
			}
		}
		else
		{
			//PickupScript.BlankText();
		}

	}


	private void BluePotionRaycast(RaycastHit Hit)
	{
		if (Hit.collider.name.Contains("Blue"))
		{
			if ((ThrowScript.ItemHeld != null) && (ThrowScript.ItemHeld.name.Contains("Blue")))
			{
				PickupScript.SetText("Blue Potion | Drink (q) | Throw (t) | Add To Inv (e)");
			}
			else
			{
				//TitleText.transform.parent.GetComponentInChildren<Animator>().SetTrigger("Hit");
				PickupScript.SetText("Blue Potion | Drink (q) | Pickup (e)");
			}

			if (Input.GetButtonDown("Drink"))
			{
				GetComponent<PotionChoiceScript>().TurnSmall = true;
				Hit.collider.gameObject.SetActive(false);
			}
		}
	}
}
