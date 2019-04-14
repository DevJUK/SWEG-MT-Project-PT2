using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class FadeTransitionScript : MonoBehaviour
{
	public Image Darkness;
	internal bool IsCoRunning;

	public IEnumerator GoDark()
	{
		IsCoRunning = true;
		Darkness.color = new Color(Darkness.color.r, Darkness.color.g, Darkness.color.b, Darkness.color.a + Time.deltaTime);

		if (Darkness.color.a == 255f)
		{
			yield return new WaitForSeconds(4);
			Darkness.color = new Color(Darkness.color.r, Darkness.color.g, Darkness.color.b, Darkness.color.a - Time.deltaTime);
			IsCoRunning = false;
		}
	}
}
