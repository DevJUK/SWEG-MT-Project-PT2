using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mouse_Move : MonoBehaviour
{
    public enum RotationAxis
    {
        MouseXandY = 0,
        MouseX = 1,
        MouseY = 2
    }

    public RotationAxis axess = RotationAxis.MouseXandY;

    public Transform Cam;

    public float sensitivityHor;
    public float sensitivityVer;

    public float minVert = -45.0f;
    public float maxVert = 45.0f;

    public float rotationY = 0;

    internal Quaternion CamOriginalRotation;

    Rigidbody rigid;

	// (Jonathan) - Added a bool to enable / disable movement of the camera so the Inv can be accessed better.
	internal bool EnableCamera;

	void Start ()
    {
        rigid = GetComponent<Rigidbody>();
        if (rigid != null)
            rigid.freezeRotation = true;
	}
	
	
	void Update ()
    {

		// (Jonathan) - Bool is just here to allow it to be disabled or enabled
		if (!EnableCamera)
		{

			if (axess == RotationAxis.MouseX)
			{
				
			}
			
			else
			{
				rotationY -= Input.GetAxis("Mouse Y") * sensitivityVer;
				rotationY = Mathf.Clamp(rotationY, minVert, maxVert);

				float delta = Input.GetAxis("Mouse X") * sensitivityHor;
				float rotationX = transform.localEulerAngles.y + delta;

				Cam.localEulerAngles = new Vector3(rotationY, Cam.rotation.y, 0);
				transform.localEulerAngles = new Vector3(transform.rotation.x, rotationX, 0);
			}
		}
	}
}
