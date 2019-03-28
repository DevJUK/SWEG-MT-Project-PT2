using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseGameScript : MonoBehaviour
{
	public GameObject PauseUI;
	private bool IsPaused;

	private void Start()
	{
		PauseUI.SetActive(false);
	}

	void Update()
    {
        if (Input.GetButtonDown("Pause") && (!IsPaused))
		{
			// Pauses the game
			Time.timeScale = 0;
			GetComponent<Mouse_Move>().enabled = false;
			PauseUI.SetActive(true);
			IsPaused = true;
		}
		else if (Input.GetButtonDown("Pause") && (IsPaused))
		{
			// resumes the game
			Time.timeScale = 1;
			GetComponent<Mouse_Move>().enabled = true;
			PauseUI.SetActive(false);
			IsPaused = false;
		}
	}

	public void Menu()
	{
		SceneManager.LoadSceneAsync(0);
	}

	public void Quit()
	{
		Application.Quit();
	}
}
