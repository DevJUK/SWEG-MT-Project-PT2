using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnTriggerOff : MonoBehaviour
{
    public GameObject TriggerBox;
    public GameObject Camera1;
    public GameObject Camera2;
    public GameObject Fire;
    
    public Larder_Event_Start Boolon;
    public PlayerController MovementScript;
    public Enemy_Move2 MoveStart;
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
            Camera1.SetActive(true);
            Camera2.SetActive(false);
            Fire.SetActive(false);
            
            MoveStart.enabled = true;
            MovementScript.enabled = true;
            stopMouse.enabled = true;
            MoveStop.enabled = false;
        }

    }
}
