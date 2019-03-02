using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Larder_Event_Start : MonoBehaviour
{
    public bool Event;
    //public GameObject Player;
    public PlayerController MovementScript;
    public Mouse_Move stopMouse;
    public Enemy_Move AIMovment;
    //public Entity AnimStop;
    //public Event_Start Boolon;
    Animator anim; //Assigns the animator
    



    void Update()
    {
        if (Event == true)
        {
            AIMovment.enabled = true;
        }
    }

    void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("running");
            MovementScript.enabled = false;
            stopMouse.enabled = false;            
            Event = true;

            MovementScript.gameObject.GetComponent<Stats>().Health -= 2;
        }
    }
}
