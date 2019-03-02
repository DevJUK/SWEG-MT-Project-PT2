using UnityEngine;
using UnityEngine.SceneManagement;

public class EnterHouseScript : MonoBehaviour
{
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
				if (collision.gameObject.tag == "Player")
				{
					SceneManager.LoadSceneAsync("GroundFloorScene");
				}
				break;
			case Rooms.Upper:
				if (collision.gameObject.tag == "Player")
				{
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
