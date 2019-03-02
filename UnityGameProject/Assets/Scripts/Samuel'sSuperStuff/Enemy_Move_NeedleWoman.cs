using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy_Move_NeedleWoman : MonoBehaviour
{
    public GameObject Player;   //sets object to follow

    NavMeshAgent enemy; //Assigns the nav mesh

    Animator anim; //Assigns the animator

    public GameObject NeedleWoman;
    public bool PlayerInRoom;
    private int AmountOfStabs;
    public Stats Stats;
    public float WaitTime;

   // public float RayLength;
    private float TimeLeft = 3.0f;

    void Start()
    {
        anim = GetComponent<Animator>(); //Calls animator
        enemy = GetComponent<NavMeshAgent>(); //Calls nav mesh
    }

    void Update()
    {
            if ((Player) && (PlayerInRoom))
            {
                if (enemy.remainingDistance != Mathf.Infinity && enemy.remainingDistance >= enemy.stoppingDistance)
                {
                    enemy.destination = Player.transform.position;
                    anim.SetBool("Running", true);
                    anim.SetBool("Stab", false);
                }

                else if (enemy.remainingDistance != Mathf.Infinity && enemy.remainingDistance < enemy.stoppingDistance)
                {
                    if (AmountOfStabs == 2) // Used to get round the fact that the character will stab for no reason before running at the player
                    {
                        anim.SetBool("Running", false);
                        anim.SetBool("Stab", false);

                        StartCoroutine(StabPlaying(WaitTime));
                        
                }
                    else
                    {
                        enemy.destination = Player.transform.position;
                        anim.SetBool("Running", false);
                        anim.SetBool("Stab", true);
                        AmountOfStabs++;

                    }
                }
            }
            else
            {
                anim.SetBool("Running", false);
                anim.SetBool("Stab", false);
                Debug.Log("Go to idle");
            }
    }

    IEnumerator StabPlaying(float Wait)
    {
        yield return new WaitForSeconds(Wait);
        Stats.Strength--;
        Stats.Agility--;
        NeedleWoman.SetActive(false);
    }
}
