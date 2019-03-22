using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class LightningScript : MonoBehaviour
{

	public Light TheMoon;
	public Light Flash;
	private float Time;

	private void Start()
	{
		TheMoon = GetComponent<Light>();
	}

	private void Update()
	{
		Time = Random.Range(0, 1001);

		if (Time <= 5)
		{
			TheMoon.enabled = false;
			Flash.enabled = true;
		}
		else
		{
			TheMoon.enabled = true;
			Flash.enabled = false;
		}
	}
}
