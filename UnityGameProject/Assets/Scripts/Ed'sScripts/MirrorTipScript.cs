using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MirrorTipScript : MonoBehaviour
{
    public bool Tipped;
    public GameObject UntippedMirror;
    public GameObject TippedMirror;
    public GameObject Key;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Tipped)
        {
            UntippedMirror.SetActive(false);
            TippedMirror.SetActive(true);
            Key.SetActive(true);
        }
        else
        {
            Key.SetActive(false);
            TippedMirror.SetActive(false);
            UntippedMirror.SetActive(true);
        }
    }

    public void TipMirror()
    {
        Tipped = true;
    }
}
