using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InvButtonScript : MonoBehaviour
{
	public GameObject Inv;
	private bool IsInvOpen;


	private void Awake()
	{
		if (Inv == null)
		{
			Debug.Log("Plase create an empty gameobject to store the hidden items, then attach it here!");
		}
	}

	private void Update()
	{
		if (Input.GetButtonDown("OpenInv"))
		{
			OpenInv();
		}
	}

	public void OpenInv()
	{
		if (Inv != null) 
		{
			if (!IsInvOpen)
			{
				Inv.SetActive(true);
				IsInvOpen = true;
				StopCamera();
			}
			else
			{
				Inv.SetActive(false);
				IsInvOpen = false;
				StopCamera();
			}
		}
	}


	internal void StopCamera()
	{
		foreach (Mouse_Move i in FindObjectsOfType<Mouse_Move>())
		{
			i.EnableCamera = !i.EnableCamera;
		}
	}
}
