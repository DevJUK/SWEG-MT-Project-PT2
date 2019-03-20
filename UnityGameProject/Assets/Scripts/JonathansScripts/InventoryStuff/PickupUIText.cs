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

	public void SetText(string input)
	{
		text.text = input;
	}

	public void BlankText()
	{
		text.text = "";
	}

}
