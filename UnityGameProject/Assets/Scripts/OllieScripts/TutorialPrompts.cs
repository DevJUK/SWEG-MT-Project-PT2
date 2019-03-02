using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TutorialPrompts : MonoBehaviour
{
    private bool Wpress;         
    private bool Apress;
    private bool Spress;
    private bool Dpress;
    private bool LCTRLpress;
    private bool LSHIFTpress;
    private bool Epress;
    private bool Ipress;

    public Text WpressText;
    public Text ApressText;
    public Text SpressText;
    public Text DpressText;
    public Text LCTRLpressText;
    public Text LSHIFTpressText;
    public Text EpressText;
    public Text IpressText;
  
    void Start()
    {
        Wpress = false;
        Apress = false;
        Spress = false;
        Dpress = false;
        LCTRLpress = false;
        LSHIFTpress = false;
        Epress = false;
        Ipress = false;
    }

    void Update()
    {
        if (Wpress == false)
        {
            WpressText.text = "To walk forward press 'W'";
        }
        if (Input.GetKeyDown(KeyCode.W))
        {
            Wpress = true;
        }

        if (Wpress == true)
        {
            SpressText.text = "To walk backwards press 'S'";
        }
        if(Input.GetKeyDown(KeyCode.S))
        {
            Spress = true;
        }

        if (Spress == true)
        {
            ApressText.text = "To side step left press 'A'";
        }
        if (Input.GetKeyDown(KeyCode.A))
        {
            Apress = true;
        }

        if (Apress == true)
        {
            DpressText.text = "To side step right press 'D'";
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            Dpress = true;
        }

        if (Dpress == true)
        {
            LCTRLpressText.text = "To crouch press 'Left Control'";
        }
        if (Input.GetKeyDown(KeyCode.RightControl))
        {
            LCTRLpress = true;
        }

        if (LCTRLpress == true)
        {
            LSHIFTpressText.text = "To Sprint press 'Left Shift' while moving forward";
        }
        if ((Input.GetKeyDown(KeyCode.LeftShift)) && (Input.GetKeyDown(KeyCode.W)))
        {
            LSHIFTpress = true;
        }

        if (LSHIFTpress == true)
        {
            IpressText.text = "To open your inventory press 'I'";
        }
        if (Input.GetKeyDown(KeyCode.I))
        {
            Ipress = true;
        }

        if (Ipress == true)
        {
            EpressText.text = "To interact with an item press 'E;";
        }
        if (Input.GetKeyDown(KeyCode.E))
        {
            Epress = true;
        }
    }
}
