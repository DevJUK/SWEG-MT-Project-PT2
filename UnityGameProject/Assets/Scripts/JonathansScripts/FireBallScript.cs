using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBallScript : MonoBehaviour
{

	public float Speed;
	public int Damange;
	public GameObject Target;
	public bool Fire;

    void Update()
    {
        if ((Fire) && (GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("Idle")))
		{
			GetComponent<Animator>().SetTrigger("Fired");
		}
    }


	private void OnTriggerEnter(Collider other)
	{
		if ((other.gameObject.tag == "Player") && (other.gameObject.GetComponent<Stats>()))
		{
			other.gameObject.GetComponentInChildren<BloodSplatterScript>().Hit = true;
			other.gameObject.GetComponent<Stats>().Health -= Damange;
			gameObject.SetActive(false);
		}
	}
}
