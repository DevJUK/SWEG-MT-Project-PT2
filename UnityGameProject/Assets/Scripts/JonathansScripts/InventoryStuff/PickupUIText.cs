using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PickupUIText : MonoBehaviour
{
	private Text text;

	private void Awake()
	{
		text = GetComponent<Text>();
	}

	public void SetText(string itemname)
	{
		text.text = "(" + itemname + ")" + " | Pickup Item (e)";
	}

	public void BlankText()
	{
		text.text = "";
	}

}
