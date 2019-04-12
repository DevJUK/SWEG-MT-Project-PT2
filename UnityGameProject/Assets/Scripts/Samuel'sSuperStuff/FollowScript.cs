using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class FollowScript : MonoBehaviour
{
    public Transform FollowObject;

    protected NavMeshAgent Nav;
    protected Entity This;


    // Use this for initialization
    protected virtual void Start()
    {
        This = GetComponent<Entity>();
        Nav = GetComponent<NavMeshAgent>();        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (FollowObject)
        {
            Nav.destination = FollowObject.position;            
        }
    }
}