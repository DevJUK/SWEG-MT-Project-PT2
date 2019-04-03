using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class InventoryScript : MonoBehaviour
{

	public const int NumberItemSlots = 10;						// Creates a constant number of the amount of inventory slots the inventory has

	public Image[] ItemImages = new Image[NumberItemSlots];     // Creates an array of images that is the size of the number of slots in the inventory (this is used to hold the images in the inventory)
	public Item[] items = new Item[NumberItemSlots];            // Creates an array of items that is the size of the number of slots in the inventory (this is used to hold the actual item)

	public Text SelectedText;

	[Header("The Item Currently Selected")]
	public Item SelectedItem;

	[Header("Raycasting Elements")]
	public GraphicRaycaster Ray;
	public PointerEventData PointerEvent;
	public EventSystem Events;

	private ThrowableItemScript ThrowScript;
	private InvButtonScript InvButton;
	internal int ItemsInInv;

	private void Start()
	{
		Events = FindObjectOfType<EventSystem>().GetComponent<EventSystem>();
		ThrowScript = GameObject.FindGameObjectWithTag("Player").GetComponentInChildren<ThrowableItemScript>();
		InvButton = GameObject.Find("InventoryUIElements").GetComponent<InvButtonScript>();
	}

	private void Update()
	{
		Debug.Log(ItemsInInv);

		PointerEvent = new PointerEventData(Events);                        // Set up a new PointerEvent
		PointerEvent.position = Input.mousePosition;                        // Set up the PointerEvent to be where the mouse is
		List<RaycastResult> Results = new List<RaycastResult>();            // Creating a list to store the raycase information
		Ray.Raycast(PointerEvent, Results);

		if (Input.GetMouseButtonDown(0))                                    // If left click pressed
		{
			foreach (RaycastResult item in Results)                         // search through the results
			{
				foreach (Image I in ItemImages)
				{
					if (item.gameObject == I.gameObject)
					{
						if (I.gameObject.GetComponent<Image>().sprite != null)
						{
							foreach (Item Thing in items)
							{
								if ((Thing != null) && (I.gameObject.GetComponent<Image>().sprite == Thing.ItemInvImage))
								{
									SelectedItem = Thing;
								}
							}
						}
						else
						{
							SelectedItem = null;
							SelectedText.text = "";
						}
					}
				}

				for (int i = 0; i < NumberItemSlots; i++)
				{

					if ((SelectedItem != null) && (ItemImages[i].sprite == SelectedItem.ItemInvImage))
					{
						ItemImages[i].gameObject.transform.parent.GetComponentInChildren<Image>().color = Color.yellow;
						SelectedText.text = SelectedItem.ItemName;
					}
					else
					{
						ItemImages[i].gameObject.transform.parent.GetComponentInChildren<Image>().color = Color.white;
					}
				}
			}
		}
	}


	public void AddItem(Item Input)
    {
		for (int i = 0; i < items.Length; i++)
		{
			if (ItemsInInv <= 10)
			{
				if (items[i] == null)                               // Check to see if there is a free slot in the inventory, null being a free slot
				{
					items[i] = Input;                               // adds the item to the free slot

					if (!Input.IsSpriteNull(Input.ItemInvImage))
					{
						ItemImages[i].sprite = Input.ItemInvImage;
					}
					else
					{
						ItemImages[i].sprite = Input.ItemSprite;
					}

					ItemImages[i].enabled = true;

					ItemsInInv++;
					break;
				}

				Input.gameObject.SetActive(true);

				if (Input.gameObject.GetComponent<SpriteRenderer>())
				{
					Input.gameObject.GetComponent<SpriteRenderer>().enabled = true;
				}
			}
			else
			{
				Input.RemoveItemFromInv();
			}
		}
    }



	public void RemoveItem()
	{
		for (int i = 0; i < items.Length; i++)                  // For each slot in the inventory
		{
			if (items[i] == SelectedItem)                              // Check to see if the item inputted is in the inventory
			{
				items[i].gameObject.transform.position = GameObject.FindGameObjectWithTag("Player").GetComponentInChildren<ThrowableItemScript>().gameObject.transform.position;
				items[i].transform.SetParent(null);
				items[i].gameObject.SetActive(true);
				
				if (!items[i].gameObject.GetComponent<Rigidbody>())
				{
					items[i].gameObject.AddComponent<Rigidbody>();
				}

				items[i].gameObject.GetComponent<Item>().enabled = true;
				items[i] = null;										// removes the item from the inventory returning it to null
				ItemImages[i].sprite = null;                           // removes the image for the inventory slot, reutrning it to null
				ItemImages[i].enabled = false;

				SelectedItem = null;
				SelectedText.text = "";
				ItemImages[i].gameObject.transform.parent.GetComponentInChildren<Image>().color = Color.white;

				break;
			}
		}
	}


	public void ClearInv()
	{
		for (int i = 0; i < items.Length; i++)
		{
			items[i] = null;
			ItemImages[i].sprite = null;                           // removes the image for the inventory slot, reutrning it to null
			ItemImages[i].enabled = false;
		}
	}

    
	public void SetItemSprite(Item Input)
	{
		for (int i = 0; i < items.Length; i++)
		{
			if (items[i] == Input)
			{
				if (Input.IsSpriteNull(Input.ItemInvImage))
				{
					ItemImages[i].sprite = Input.ItemSprite;
				}
				else
				{
					ItemImages[i].sprite = Input.ItemInvImage;
				}

				return;
			}
		}
	}

	public void EquipItem()
	{
		if (ThrowScript.ItemHeld == null)
		{
			ThrowScript.ItemHeld = SelectedItem.gameObject;
			SelectedItem.RemoveItemFromInv();
			InvButton.OpenInv();
		}
	}

	public void DropItem()
	{
		SelectedItem.RemoveItemFromInv();
		InvButton.OpenInv();
	}
}
