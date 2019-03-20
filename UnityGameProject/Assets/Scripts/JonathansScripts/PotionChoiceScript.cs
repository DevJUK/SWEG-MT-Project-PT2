using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PotionChoiceScript : MonoBehaviour
{
	public GameObject Player;
	public GameObject Darkness;
	public bool TurnSmall;

	private void FixedUpdate()
	{
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
