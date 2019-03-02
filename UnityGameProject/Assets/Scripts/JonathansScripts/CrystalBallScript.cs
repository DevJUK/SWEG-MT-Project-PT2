using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrystalBallScript : MonoBehaviour
{
	public float Speed;
	private Renderer Rend;

	private void Start()
	{
		Rend = GetComponent<Renderer>();
	}

	private void Update()
	{
		Rend.material.mainTextureOffset += new Vector2(Speed * Time.deltaTime, Speed * Time.deltaTime);
	}
}
