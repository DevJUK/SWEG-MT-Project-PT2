using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrosshairScript : MonoBehaviour
{

	private void FixedUpdate()
	{
		if (gameObject.activeInHierarchy)
		{
			Cursor.visible = false;
		}
	}

}
