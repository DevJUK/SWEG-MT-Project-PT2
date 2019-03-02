using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class InventoryScript : MonoBehaviour
{

	public const int NumberItemSlots = 10;						// Creates a constant number of the amount of inventory slots the inventory has

	public Image[] ItemImages = new Image[NumberItemSlots];     // Creates an array of images that is the size of the number of slots in the inventory (this is used to hold the images in the inventory)
	public Item[] items = new Item[NumberItemSlots];            // Creates an array of items that is the size of the number of slots in the inventory (this is used to hold the actual item)

	[Header("Player GameObject")]
	public GameObject Player;

	[Header("Are there any items in the inventory")]
	public bool ItemsInInv;

	[Header("The Item Currently Selected")]
	public Item SelectedItem;

	[Header("Raycasting Elements")]
	public GraphicRaycaster Ray;
	public PointerEventData PointerEvent;
	public EventSystem Events;

	public int NumberOfPresses;
	public GameObject ItemDetailPrefab;
	internal bool PanelOpen;
	internal bool PrefabMade;


	private void Start()
	{
		Player = GameObject.FindGameObjectWithTag("Player");
		Events = FindObjectOfType<EventSystem>().GetComponent<EventSystem>();
	}


	void Update()
	{
		PointerEvent = new PointerEventData(Events);                        // Set up a new PointerEvent
		PointerEvent.position = Input.mousePosition;                        // Set up the PointerEvent to be where the mouse is
		List<RaycastResult> Results = new List<RaycastResult>();            // Creating a list to store the raycase information
		Ray.Raycast(PointerEvent, Results);

		if (Input.GetMouseButtonDown(0))                                    // If right click is pressed
		{
			foreach (RaycastResult item in Results)                         // search through the results
			{
				foreach (Item i in GetComponent<InventoryScript>().items)                         // search the items in the inveontry
				{
					if (NumberOfPresses == 2)
					{
						OpenItem();
						NumberOfPresses = 0;
					}
					else
					{
						if (i == null)                                          // if NULL then break out of the loop (avoids NullExepctionErrors)
						{
							continue;
						}
						else
						{
							if (SelectedItem == null)
							{
								SelectedItem = i;

								//foreach (Image i2 in ItemImages)
								//{
								//	if (i2.sprite == SelectedItem.ItemSprite)
								//	{
								//		i2.transform.parent.GetComponentInChildren<Image>().color = Color.yellow;
								//		break;
								//	}
								//	else
								//	{
								//		continue;
								//	}
								//}
							}
							NumberOfPresses++;
						}
					}
				}
			}
		}
	}



	public void AddItem(Item Input)
    {
		for (int i = 0; i < items.Length; i++)
		{

			if (items[i] == null)
			{
				ItemsInInv = false;
			}
			else if (Input.ItemName == items[i].ItemName)
			{
				ItemsInInv = true;
			}
			else
			{
				ItemsInInv = false;
			}


			if (ItemsInInv)
			{
				//ItemImages[i].GetComponent<Stack>().AddToStackValue();
				break;
			}
			else
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
					//ItemImages[i].GetComponent<Stack>().AddToStackValue();
					//ItemImages[i].GetComponent<Stack>().SetItemText(items[i]);				// would show the name of the item - removed as the selected part isn't made
					ItemsInInv = true;
					break;
				}
				else
				{
					// if the inventory is full enable the object in the scene
				}
			}

			Input.gameObject.SetActive(true);
			Input.gameObject.GetComponent<SpriteRenderer>().enabled = true;
			Input.gameObject.GetComponent<PolygonCollider2D>().enabled = true;
		}
    }



	public void RemoveItem()
	{
		for (int i = 0; i < items.Length; i++)                  // For each slot in the inventory
		{
			if (items[i] == SelectedItem)                              // Check to see if the item inputted is in the inventory
			{
				items[i].gameObject.transform.position = Player.GetComponentInChildren<ThrowableItemScript>().gameObject.transform.position;
				items[i].transform.SetParent(null);
				items[i].gameObject.SetActive(true);
				
				if (!items[i].gameObject.GetComponent<Rigidbody>())
				{
					items[i].gameObject.AddComponent<Rigidbody>();
				}

				items[i].gameObject.GetComponent<Item>().enabled = true;

				items[i] = null;                                // removes the item from the inventory returning it to null
				ItemImages[i].sprite = null;                           // removes the image for the inventory slot, reutrning it to null
				ItemImages[i].enabled = false;
				break;
			}
		}
	}




	public void OpenItem()
	{
		if (!PanelOpen)
		{
			PanelOpen = true;


			if (!PrefabMade)
			{
				PrefabMade = true;

				GameObject ItemDetail = Instantiate(ItemDetailPrefab);
				ItemDetail.gameObject.name = "UIBG";
				ItemDetail.transform.SetParent(GameObject.Find("InvCanvas").transform, false);
				ItemDetail.GetComponent<Animator>().SetTrigger("FadeIn");

				foreach (Text t in ItemDetail.GetComponentsInChildren<Text>())
				{
					if (t.gameObject.name == "ItemName")
					{
						t.text = SelectedItem.ItemName;
					}

					if (t.gameObject.name == "ItemDesc")
					{
						t.text = SelectedItem.ItemDesc;
					}
				}

				ItemDetail.GetComponent<UIBGScript>().ThisItem = SelectedItem;
				SelectedItem = null;
				NumberOfPresses = 0;
			}
			else
			{
				GameObject Panel = GameObject.Find("UIBG");

				Panel.GetComponent<Animator>().SetTrigger("FadeIn");

				foreach (Text t in Panel.GetComponentsInChildren<Text>())
				{
					if (t.gameObject.name == "ItemName")
					{
						t.text = SelectedItem.ItemName;
					}

					if (t.gameObject.name == "ItemDesc")
					{
						t.text = SelectedItem.ItemDesc;
					}
				}

				Panel.GetComponent<UIBGScript>().ThisItem = SelectedItem;
				SelectedItem = null;
				NumberOfPresses = 0;
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
			//ItemImages[i].GetComponent<Stack>().ResetStack();
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
}
