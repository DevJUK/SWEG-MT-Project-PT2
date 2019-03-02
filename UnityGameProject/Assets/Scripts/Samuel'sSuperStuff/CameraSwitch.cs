using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraSwitch : MonoBehaviour
{
    public GameObject Camera1;
    public GameObject Camera2;
    public GameObject Fire;
    public Larder_Event_Start Boolon;
    private void Update()
    {
        if (Boolon.Event == true)
        {
            Camera1.SetActive(false);
            Camera2.SetActive(true);
            Fire.SetActive(true);
        }
    }
}
