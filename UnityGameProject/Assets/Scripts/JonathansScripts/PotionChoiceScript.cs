using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PotionChoiceScript : MonoBehaviour
{
	public GameObject Player;
	public GameObject Darkness;

	public Text TitleText;
	private float Range = 2;

	public bool TurnSmall;

	private void Update()
	{
		RaycastHit Hit;

		if (Physics.Raycast(transform.position, transform.forward, out Hit, Range)) // Check to see if raycast hits anything
		{

			if (Hit.collider.name.Contains("Blue"))
			{
				//TitleText.transform.parent.GetComponentInChildren<Animator>().SetTrigger("Hit");
				TitleText.text = "Blue Potion:   Drink (q) | Pickup (e)";
			}
			else
			{
				//TitleText.transform.parent.GetComponentInChildren<Animator>().SetTrigger("Close");
				TitleText.text = "";
			}


			if (Hit.collider.name.Contains("Blue") && Input.GetButtonDown("Drink"))
			{
				TurnSmall = true;
				Hit.collider.gameObject.SetActive(false);
			}

		}


		if (TurnSmall)
		{
			Player.transform.localScale -= new Vector3(Time.deltaTime / 4, Time.deltaTime / 4, Time.deltaTime / 4);

			if (!Darkness.GetComponent<Animator>().GetBool("FadeIn"))
			{
				Darkness.GetComponent<Animator>().SetBool("FadeIn", true);
			}

			if (Player.transform.localScale.x <= .3f)
			{
				TurnSmall = false;
				Player.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
			}
		}
	}
}
