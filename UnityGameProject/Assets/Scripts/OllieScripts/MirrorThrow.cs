using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MirrorThrow : MonoBehaviour

   
{

    public GameObject MusicBox;
    private bool ObjectInMirror = false;


    // Start is called before the first frame update
    void Start()
    {
        MusicBox.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (ObjectInMirror == true)
        {
            MusicBox.SetActive(true);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Inventory")
        {
            Destroy(collision.gameObject);
            ObjectInMirror = true;
        }
          
    }

}
