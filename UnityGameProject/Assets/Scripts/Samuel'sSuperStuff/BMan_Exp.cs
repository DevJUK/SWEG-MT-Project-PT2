using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BMan_Exp : MonoBehaviour
{    
    public float ExplosivePower;
    public float ExplosiveRadius;
    //public GameObject Explosion;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //transform.DetachChildren;



        var Objects = GameObject.FindObjectsOfType<Rigidbody>();
        foreach (Rigidbody item in Objects)
        {
            if (Vector2.Distance(item.gameObject.transform.position, transform.gameObject.transform.position) < ExplosivePower)
            {
                Debug.Log(item.gameObject.name);
                item.gameObject.GetComponent<Rigidbody>().AddForce((item.gameObject.transform.position - transform.position) * ExplosivePower, ForceMode.Impulse);
            }
        }
    }
}
