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

    [Header("Stats Script")]
    [Tooltip("The script that controls all the players stats")]
    public Stats stats;

    [Header("Camera Movement Script")]
    [Tooltip("The script that moves the camera with the mouse")]
    public Mouse_Move Mouse_Move;

    public PlayerController PlayerController;

    public void RunEvent()
    {
        //if (CurrentBlinkNo <= AmountOfBlinks)
        //{
            StartCoroutine(Blink());
            Debug.Log("Start Coroutine");
        //}
    }

    IEnumerator Blink()
    {
        Debug.Log("Running Event");

        Mouse_Move.enabled = false; // Stop the player from being able to look around
        PlayerController.enabled = false; // Stop the player from being able to walk around

        // First blink
        BlackCanvas.SetActive(true);

        ManequinsPos1.SetActive(true);
        ManequinsPos2.SetActive(false);
        ManequinsPos3.SetActive(false);

        yield return new WaitForSeconds(Delay);

        BlackCanvas.SetActive(false);

        yield return new WaitForSeconds(Delay);

        // Second blink
        BlackCanvas.SetActive(true);

        ManequinsPos1.SetActive(false);
        ManequinsPos2.SetActive(true);

        yield return new WaitForSeconds(Delay);

        BlackCanvas.SetActive(false);

        yield return new WaitForSeconds(Delay);

        // Third blink
        BlackCanvas.SetActive(true);

        ManequinsPos2.SetActive(false);
        ManequinsPos3.SetActive(true);

        yield return new WaitForSeconds(Delay);

        stats.Health -= 2;
        stats.Sanity -= 2;
        BlackCanvas.SetActive(false);

        Mouse_Move.enabled = true; // Allow the player to be able to look around again
        PlayerController.enabled = true; // Allow the player to be able to walk around again

    }
}
