using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharredRoomEventScrpt : MonoBehaviour
{
    [Header("Groups Of Manequins")]
    [Tooltip("The first position that the manequins will show up in")]
    public GameObject ManequinsPos1;
    [Tooltip("The second position that the manequins will show up in")]
    public GameObject ManequinsPos2;
    [Tooltip("The third position that the manequins will show up in")]
    public GameObject ManequinsPos3;

    [Header("Black Canvas")]
    [Tooltip("Used to block players vision")]
    public GameObject BlackCanvas;

    [Header("Coroutine Information")]
    [Tooltip("How long the coroutine should wait")]
    public float Delay;
    [Tooltip("How many times the coroutine should run")]
    public int AmountOfBlinks;
    [Tooltip("The number of times that the coroutine has looped")]
    public int CurrentBlinkNo;

    public void RunEvent()
    {
        if (CurrentBlinkNo <= AmountOfBlinks)
        {
            StartCoroutine(Blink());
            Debug.Log("Start Coroutine");
        }
    }

    IEnumerator Blink()
    {
        Debug.Log("Running Event");
        while (CurrentBlinkNo <= AmountOfBlinks)
        {
            BlackCanvas.SetActive(true);
            if (CurrentBlinkNo == 1)
            {
                ManequinsPos1.SetActive(true);
                ManequinsPos2.SetActive(false);
                ManequinsPos3.SetActive(false);
            }

            if (CurrentBlinkNo == 2)
            {
                ManequinsPos1.SetActive(false);
                ManequinsPos2.SetActive(true);
                ManequinsPos3.SetActive(false);
            }

            if (CurrentBlinkNo == 3)
            {
                ManequinsPos1.SetActive(false);
                ManequinsPos2.SetActive(false);
                ManequinsPos3.SetActive(true);
            }

            CurrentBlinkNo++;
            yield return new WaitForSeconds(Delay);
            BlackCanvas.SetActive(false);
        }
    }
}
