using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadingScript : MonoBehaviour
{
	public SaveScript SaveGO;

    // Start is called before the first frame update
    void Start()
    {
        if (FindObjectOfType<SaveScript>()) { SaveGO = FindObjectOfType<SaveScript>(); }
    }


	public void LoadTheGame()
	{
		SaveGO.LoadGame();
	}
}
