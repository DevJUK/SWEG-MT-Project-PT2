using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
	[Header("Min & Max X Bounds")]
	public float MinX;
	public float MaxX;
	[Header("Min & Max Y Bounds")]
	public float MinY;
	public float MaxY;

	private float RotY;
	private float RotX;

	[Header("Speed of Rotation")]
	public float MovementSpeed;

	private void Update()
	{
		RotX += Input.GetAxis("Mouse X") * MovementSpeed;
		RotY += Input.GetAxis("Mouse Y") * MovementSpeed;
		RotX = Mathf.Clamp(RotX, MinX, MaxX);
		RotY = Mathf.Clamp(RotY, MinY, MaxY);
		Camera.main.transform.localRotation = Quaternion.Euler(new Vector3(-RotY, RotX, 0));
	}

}
