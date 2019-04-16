using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseGameScript : MonoBehaviour
{
	public GameObject PauseUI;
	public SaveScript Save;
	private bool IsPaused;


	private void Start()
	{
		if (GameObject.Find("PauseUI")) { PauseUI = GameObject.Find("PauseUI"); }
		if (FindObjectOfType<SaveScript>()) { Save = FindObjectOfType<SaveScript>(); }
	}

	void Update()
    {
        if (Input.GetButtonDown("Pause") && (!IsPaused))
		{
			// Pauses the game
			Time.timeScale = 0;
			GetComponent<Mouse_Move>().enabled = false;
			PauseUI.GetComponent<Canvas>().enabled = true;
			IsPaused = true;
		}
		else if (Input.GetButtonDown("Pause") && (IsPaused))
		{
			// resumes the game
			Time.timeScale = 1;
			GetComponent<Mouse_Move>().enabled = true;
			PauseUI.GetComponent<Canvas>().enabled = false;
			IsPaused = false;
		}
	}


	public void SaveTheGame()
	{
		Save.SaveGame();
	}

	public void Menu()
	{
		SceneManager.LoadSceneAsync(0);
		Cursor.visible = true;
		Time.timeScale = 1;
	}

	public void Quit()
	{
		Application.Quit();
	}

	public void ResumeTime()
	{
		Time.timeScale = 1;
	}
}
