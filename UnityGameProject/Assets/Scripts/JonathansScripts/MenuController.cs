using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{

	public Animator Anim;
	public GameObject Canvas;
	public Animator DarkAnim;


	public void PlayPressed()
	{
		DarkAnim.SetTrigger("GoDark");
		Anim.SetTrigger("PlayGame");
	}

	public void LoadScene()
	{
		SceneManager.LoadScene("GroundFloorScene");
	}

	public void QuitGame()
	{
		Application.Quit();
	}
}
