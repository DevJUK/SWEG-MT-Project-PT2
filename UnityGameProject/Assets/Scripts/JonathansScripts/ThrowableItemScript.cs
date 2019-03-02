using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowableItemScript : MonoBehaviour
{
	public GameObject ItemHeld;
	public float ThrowSpeed;

	private void Start()
	{
		ThrowSpeed = 500f;	
	}

	void Update()
	{
		if (ItemHeld != null)
		{
			if (ItemHeld.GetComponent<Rigidbody>()) { ItemHeld.GetComponent<Rigidbody>().velocity = Vector3.zero; }

			ItemHeld.gameObject.transform.position = transform.position;

			if (Input.GetKeyDown(KeyCode.T))
			{
				ThrowItem(ItemHeld.GetComponent<Item>());
			}
		}
	}


	public void ThrowItem(Item I)
	{
		I.gameObject.transform.SetParent(null);

		if (!I.gameObject.GetComponent<Rigidbody>()) { I.gameObject.AddComponent<Rigidbody>(); }

		ItemHeld = null;
		I.gameObject.GetComponent<Rigidbody>().AddForce(transform.forward * ThrowSpeed);

	}
}