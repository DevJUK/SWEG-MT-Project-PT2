using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaycastItems : MonoBehaviour
{
	public float Range;
	public GameObject UI;
	private bool UIOpen;

	public PickupUIText PickupScript;
	private ThrowableItemScript ThrowScript;

    // Anything to do with NPC's in this script was done by Ed so talk to him

    public NPCInteractionScrpt NPCInteractionScrpt; // the main controll script for npc dialogue interactions
    public MirrorTipScript MirrorTipScript; // Script used to tip the mirror (who'd of guessed it)
    public string NameofNPCHit; // Used to send the name of the NPC to the NPC interaction script

	private void Start()
	{
		ThrowScript = GameObject.FindGameObjectWithTag("Player").GetComponentInChildren<ThrowableItemScript>();
		PickupScript = GameObject.Find("PickupPopup").GetComponent<PickupUIText>();
	}


	private void FixedUpdate()
	{
		RaycastHit Hit;
        

		if (Physics.Raycast(transform.position, transform.forward, out Hit, Range)) // Check to see if raycast hits anything
		{

            Debug.Log(Hit.transform.gameObject.name);

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


            else
            {
				if (Input.GetButtonDown("Pickup")) // E Key
                {
					if (ThrowScript.ItemHeld != null)
					{
						AddHitToInv(Hit);
						ThrowScript.ItemHeld = null;
					}
					else
					{
						if (Hit.transform.gameObject.GetComponent<Item>())
						{
							ThrowScript.ItemHeld = Hit.transform.gameObject;
						}
					}
				}

				if (ThrowScript.ItemHeld == null)
				{
					if (Hit.transform.gameObject.GetComponent<Item>())
					{
						PickupScript.SetText(Hit.transform.gameObject.GetComponent<Item>().ItemName.ToString());
					}
					else
					{
						if (UI.activeInHierarchy)
						{
							PickupScript.BlankText();
						}
					}
				}
            }
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



	private void ToggleUI()
	{
		if (UIOpen)
		{
			UI.SetActive(true);
		}

		if (!UIOpen)
		{
			UI.SetActive(false);
		}
	}
}
