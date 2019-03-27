using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    public float Joystickdeadzone = 0.1f;
    public float Joysticksensativity = 2f;
    public float Mousesensatiity = 2f;

    Entity player;

    // Start is called before the first frame update
    void Start()
    {

        Debug.Log("running");
        player = GameObject.FindObjectOfType<Entity>();
    }

    // Update is called once per frame
    void Update()
    {
        float forward = Input.GetAxis("Vertical");
        float sideways = Input.GetAxis("Horizontal");
        Debug.Log("up/down");


        if (forward < Joystickdeadzone && forward > -Joystickdeadzone)
        {
            Debug.Log("up/down");
            forward = 0;
        }
        if (sideways < Joystickdeadzone && forward > -Joystickdeadzone)
        {
            sideways = 0;
        }

        Vector3 MoveDir = new Vector3(sideways, 0.0f, forward);
        player.Move(MoveDir);
    }

   // private void Movement()
   // {
   //     float forward = Input.GetAxis("Vertical");
   //     float sideways = Input.GetAxis("Horizontal");
   //     Debug.Log("up/down");
   //
   //
   //     if (forward < Joystickdeadzone && forward > -Joystickdeadzone)
   //     {
   //         Debug.Log("up/down");
   //         forward = 0;
   //     }
   //     if(sideways < Joystickdeadzone && forward > -Joystickdeadzone)
   //     {
   //         sideways = 0;
   //     }
   //
   //     Vector3 MoveDir = new Vector3(sideways, 0.0f, forward);
   //     MoveDir = transform.TransformDirection(MoveDir);
   // }

}
