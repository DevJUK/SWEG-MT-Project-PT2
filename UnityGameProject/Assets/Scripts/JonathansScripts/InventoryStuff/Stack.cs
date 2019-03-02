using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Stack : MonoBehaviour
{
	public int NumberStacked;

	public GameObject StackNumberText;
	public GameObject ItemText;

	public void AddToStackValue()
	{
		NumberStacked++;
		StackNumberText.GetComponent<Text>().text = NumberStacked.ToString();
	}

	public void SetItemText(Item input)
	{
		ItemText.GetComponent<Text>().text = input.ItemName;
	}


	public void ResetItemText()
	{
		ItemText.GetComponent<Text>().text = "";
	}

	public void ResetStack()
	{
		StackNumberText.GetComponent<Text>().text = "";
		NumberStacked = 0;
	}
}
