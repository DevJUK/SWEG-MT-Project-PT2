using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    Entity This;

    private Vector3 MoveDir;

	// Jonathan Stats Stuff
	private Stats StatsScript;

	// Use this for initialization
	void Start ()
    {
        This = GetComponent<Entity>();

		// Jonathan Stats Stuff
		StatsScript = GetComponent<Stats>();
		StatsScript.CustomSetup(10, 10, 3, 4, 5, 4, 3, 3);
	}
	
	// Update is called once per frame
	void Update ()
    {
         MoveDir = new Vector3(Input.GetAxis("Horizontal"), 0.0f, Input.GetAxis("Vertical"));
         MoveDir = transform.TransformDirection(MoveDir);            

       

         if (Input.GetButton("Jump"))
         {
             This.Jump();
         }     

        This.Move(MoveDir);
        //This.CallyMove(MoveDir);
        //This.StaticCrouch(MoveDir);
	}
}
