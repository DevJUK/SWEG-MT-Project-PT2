using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{

	public Animator Anim;
	public GameObject Canvas;


	public void PlayPressed()
	{
		Canvas.SetActive(false);
		Anim.SetTrigger("PlayGame");
	}

	public void LoadScene()
	{
		SceneManager.LoadSceneAsync("Loading");
	}

	public void QuitGame()
	{
		Application.Quit();
	}
}
