using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoliceTrigger : MonoBehaviour
{
    public GameObject TriggerBox;
    public GameObject Camera1;
    public GameObject Camera2;

    public Event_Start Boolon;
    public PlayerController MovementScript;

    public Mouse_Move stopMouse;
    



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
        }

    }
}
