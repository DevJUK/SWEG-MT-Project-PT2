using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBarScript : MonoBehaviour
{
	public float Health = 10;
	public Image HealthBar;


	private void Update()
	{
		UpdateHealthBar(Health);
	}


	public float UpdateHealthBar(float Health)
	{
		float Amount = Health / 10;
		HealthBar.fillAmount = Amount;
		return Amount;
	}
}
