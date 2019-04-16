using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class FadeTransitionScript : MonoBehaviour
{
	public Image Darkness;

	public void GoDark()
	{
		Darkness.GetComponent<Animator>().SetBool("FadeIn", true);
	}
}
