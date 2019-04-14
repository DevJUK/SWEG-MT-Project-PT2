using UnityEngine;
using UnityEngine.SceneManagement;

public class EnterHouseScript : MonoBehaviour
{
	public GameObject Darkness;

	public enum Rooms
	{
		Outside,
		Ground,
		Upper,
		Basement,
		End,
	}

	public Rooms Scenes;

	private void OnCollisionEnter(Collision collision)
	{
		switch (Scenes)
		{
			case Rooms.Outside:
				break;
			case Rooms.Ground:
				if ((collision.gameObject.tag == "Player") && (!Darkness.GetComponent<FadeTransitionScript>().IsCoRunning))
				{
					Darkness.GetComponent<FadeTransitionScript>().StartCoroutine(Darkness.GetComponent<FadeTransitionScript>().GoDark());
					SceneManager.LoadSceneAsync("GroundFloorScene");
				}
				break;
			case Rooms.Upper:
				if ((collision.gameObject.tag == "Player") && (!Darkness.GetComponent<FadeTransitionScript>().IsCoRunning))
				{
					Darkness.GetComponent<FadeTransitionScript>().StartCoroutine(Darkness.GetComponent<FadeTransitionScript>().GoDark());
					SceneManager.LoadSceneAsync("UpperFloorScene");
				}
				break;
			case Rooms.Basement:
				break;
			case Rooms.End:
				break;
			default:
				break;
		}
	}
}
