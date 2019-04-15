using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnTriggerOffButcher : MonoBehaviour
{
    public GameObject TriggerBox;    
    
    public Event_Start Boolon;
    public PlayerController MovementScript;

    public Mouse_Move stopMouse;
    public Enemy_Move MoveStop;



    private void Update()
    {
        StartCoroutine(BoxOff());       
    }

    IEnumerator BoxOff()
    {
        if (Boolon.Event == true)
        {
            yield return new WaitForSeconds(10);
            TriggerBox.SetActive(false);
           
            MovementScript.enabled = true;
            stopMouse.enabled = true;
            MoveStop.enabled = false;
        }

    }
}
