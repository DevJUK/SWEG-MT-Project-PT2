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
				if ((collision.gameObject.tag == "Player"))
				{
					Darkness.GetComponent<FadeTransitionScript>().GoDark();
					SceneManager.LoadScene("GroundFloorScene");
				}
				break;
			case Rooms.Upper:
				if ((collision.gameObject.tag == "Player"))
				{
					Darkness.GetComponent<FadeTransitionScript>().GoDark();
					SceneManager.LoadScene("UpperFloorScene");
				}
				break;
			case Rooms.Basement:
				if ((collision.gameObject.tag == "Player"))
				{
					Darkness.GetComponent<FadeTransitionScript>().GoDark();
					SceneManager.LoadScene("BasementScene");
				}
				break;
			case Rooms.End:
				break;
			default:
				break;
		}
	}
}
