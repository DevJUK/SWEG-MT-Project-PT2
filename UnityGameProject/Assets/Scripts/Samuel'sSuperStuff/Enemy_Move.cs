using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy_Move : MonoBehaviour
{
    public GameObject Player;   //sets object to follow

    NavMeshAgent enemy; //Assigns the nav mesh

    Animator anim; //Assigns the animator

    
    //public EnemySight MovTime;

   // public float RayLength;
    private float TimeLeft = 3.0f;

    void Start()
    {
        anim = GetComponent<Animator>(); //Calls animator
        enemy = GetComponent<NavMeshAgent>(); //Calls nav mesh
    }

    void Update()
    {
        //if (MovTime.Moving)
        //{

            if (Player)
            {
                if (enemy.remainingDistance != Mathf.Infinity && enemy.remainingDistance <= enemy.stoppingDistance)
                {
                    enemy.destination = Player.transform.position;
                    anim.SetBool("IsWalking", false);
                    
                    
                    //anim.SetBool("Attack", true);
                }
                else 
                {
                    anim.SetBool("IsWalking", true);
                    //anim.SetBool("Attack", false);
                    enemy.destination = Player.transform.position;
                    
                }
            }
        //}
       // else if (!MovTime.Moving)
       // {
       //     anim.SetBool("IsWalking", false);
       //     anim.SetBool("Attack", false);
       //     enemy.destination = transform.localPosition;
       //     //enemy.destination = enemy.destination;
       // }
    }
}
