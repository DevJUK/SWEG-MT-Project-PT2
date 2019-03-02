using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemySight : MonoBehaviour
{
    public GameObject Player;

    public bool Moving;

    //public float fieldOfViewAngle = 110f;
    //public bool PlayerInSight;
    //public GameObject Player;
    //private NavMeshAgent nav;
    //private SphereCollider col;
    //
    //private void Start()
    //{
    //    nav = GetComponent<NavMeshAgent>();
    //    col = GetComponent<SphereCollider>();
    //}
    //
    //private void OnTriggerStay(Collider other)
    //{
    //    if(other.gameObject == Player)
    //    {
    //        PlayerInSight = false;
    //        Vector3 direction = other.transform.position - transform.position;
    //        float angle = Vector3.Angle(direction, transform.forward);
    //
    //        if(angle < fieldOfViewAngle * 0.5f)
    //        {
    //            RaycastHit hit;
    //
    //            if(Physics.Raycast(transform.position + transform.up, direction.normalized, out hit, col.radius))
    //            {
    //                if (hit.collider.gameObject == Player)
    //                {
    //                   // Debug.DrawRay(transform.position + transform.up, direction.normalized, out hit, col.radius) Color.red);
    //
    //                    PlayerInSight = true;
    //
    //                }
    //            }
    //        }
    //    }        
    //}
    //
    //private void OnTriggerExit(Collider other)
    //{
    //    if(other.gameObject == Player)
    //    {
    //        PlayerInSight = false;
    //    }
    //}
    //
    ////float CalculatePathLength(Vector3 targetPosition)
    ////{
    ////    NavMeshPath path = new NavMeshPath();
    ////    if (nav.enabled)
    ////    {
    ////        nav.CalculatePath(targetPosition, path);
    ////
    ////        Vector3[] allWayPoints = new Vector3[path.corners.Length + 2];
    ////    }
    ////}
    //

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject == Player)
        {
            Moving = true;
        }
        else
        {
            Moving = false;
        }
    }
}
