using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Patrol : FollowScript
{
    public AINode[] PatrolPoints;

    public int CurrentPoint;

    public bool Reverse;

    public bool Loop;


    // Use this for initialization
    protected override void Start()
    {
        base.Start();

        for (int i = 0; i < PatrolPoints.Length; ++i)
        {
            PatrolPoints[i].Owner = this;
            PatrolPoints[i].Ent = This;
            PatrolPoints[i].NodeNum = i;
        }

        if (Reverse)
        {
            CurrentPoint = PatrolPoints.Length - 1;
        }

        FollowObject = PatrolPoints[CurrentPoint].transform;
    }


    public void GoToNextPoint()
    {
        CurrentPoint += (Reverse) ? -1 : 1;

        if (CurrentPoint > PatrolPoints.Length - 1)
        {
            if (Loop)
            {
                CurrentPoint = 0;
            }
            else
            {
                Reverse = true;
                CurrentPoint = PatrolPoints.Length - 2;
            }
        }
        else if (CurrentPoint < 0)
        {
            if (Loop)
            {
                CurrentPoint = PatrolPoints.Length - 1;
            }
            else
            {
                Reverse = false;
                CurrentPoint = 1;
            }
        }

        FollowObject = PatrolPoints[CurrentPoint].transform;
    }

    private void OnTriggerEnter(Collider other)
    {

        Entity User = other.GetComponent<Entity>();
        if (User)
        {
            FollowObject = User.transform;
        }

    }

}
