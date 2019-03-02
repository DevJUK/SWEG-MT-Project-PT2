using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonkeyScript : MonoBehaviour
{
	internal bool IsSnakeDone;
	private bool Moved;
	private ThrowableItemScript ThrowScript;


	private void Start()
	{
		ThrowScript = GameObject.FindGameObjectWithTag("Player").GetComponentInChildren<ThrowableItemScript>();
	}

	void Update()
    {
        if ((IsSnakeDone) && (!Moved))
		{
			transform.position = ThrowScript.gameObject.transform.position;
			ThrowScript.gameObject.GetComponentInParent<Stats>().Sanity -= 1;
			Moved = true;
		}
    }
}
