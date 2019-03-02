using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueScrpt : MonoBehaviour
{
    public GameObject TextBox;
    public Text TextBoxVal;
    public bool StartDialogue;
    public bool EndOfBlock;
    public bool CoroutineStarted;
    public bool DialoguePause;
    public bool NPCTalking;
    public bool NewConversation;
    public bool PrintSentence;
    public int DialoguePath;
    public int StartingDialoguePath;

    public int CharNo;
    public int LastSentenceNo;
    public string DialogueValue;
    public List<string> Dialogue;


    // Use this for initialization
    void Start()
    {
        StartDialogue = false;
        TextBox = GameObject.Find("Text");
        TextBoxVal = TextBox.GetComponent<Text>();
        TextBox.SetActive(false);
        DialoguePath = StartingDialoguePath;
        LastSentenceNo = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (NPCTalking)
        {
            DialogueStart();
        }
    }

    public void DialogueStart()
    {
        if ((Input.GetMouseButtonDown(0)) && (!StartDialogue) && (!NewConversation))
        {
            StartDialogue = true;
            DialoguePath = StartingDialoguePath;
            Debug.Log("Click");
        }
        else if ((Input.GetMouseButtonDown(0)) && (!StartDialogue) && (NewConversation))
        {
            StartDialogue = true;
            LastSentenceNo++;
            DialoguePath = LastSentenceNo;
            Debug.Log("Click");
        }

        if (StartDialogue)
        {
            TextBox.SetActive(true);

            DialogueValue = (Dialogue[DialoguePath]);

            if (DialogueValue == "#End#") // #End# used to signify end of dialogue
            {
                EndOfDialogueBlock();
                Debug.Log("End");
            }
            else if (DialogueValue.Contains("#Break#")) // creates a break in the dialogue
            {
                BreakInDialogue();
                Debug.Log("Break");
            }
            //else if (DialogueValue == "#Pause#")
            //{
            //    DialoguePause = true;
            //    Debug.Log("Pause");
            //}
            else if (DialogueValue.Contains("#LP:")) // Loop back to set point in dialogue
            {

                int NewLoop = System.Convert.ToInt16(DialogueValue.Remove(0, 4));
                StartingDialoguePath = NewLoop;
                DialoguePath++;
            }
            

            else // if text isnt logic value print to text box 
            {
                if (!CoroutineStarted && !EndOfBlock && !DialoguePause)
                {
                    Debug.Log("Not Logic");
                    StartCoroutine(PrintText());
                    CoroutineStarted = true;
                    NewConversation = false;
                }
                if (EndOfBlock && !DialoguePause)
                {
                    Debug.Log("Waiting");
                    NextDialogueBlock();
                }
                if (DialoguePause)
                {
                    if (Input.GetMouseButtonDown(0))
                    {
                        DialoguePause = false;
                        DialoguePath++;
                    }
                }
            }
        }
    }

    IEnumerator PrintText()
    {
        // Runs through each character individually and prints them with a 0.03 pause between each one
            for (int CharNo = 0; CharNo < (Dialogue[DialoguePath].Length + 1); CharNo++)
            {
                TextBoxVal.text = Dialogue[DialoguePath].Substring(0, CharNo);
                if (Input.GetMouseButtonDown(1))
                {
                    PrintFullSentence();
                }
                else
                {
                    yield return new WaitForSeconds(0.03f);
                }
            }

        CoroutineStarted = false;
        EndOfBlock = true;
    }
    public void PrintFullSentence()
    {
        TextBoxVal.text = Dialogue[DialoguePath];
        PrintSentence = false;
        Debug.Log("Print full line");
    }

    public void NextDialogueBlock()
    {
        if (Input.GetMouseButtonDown(0))
        {
            DialoguePath++;
            Debug.Log("NextDialogueBlock");
            EndOfBlock = false;
        }
    }

    public void EndOfDialogueBlock()
    {
        LastSentenceNo = DialoguePath;
        NPCTalking = false;
        StartDialogue = false;
        TextBoxVal.text = "";
    }

    public void BreakInDialogue()
    {
        Debug.Log("Break In Dialogue");
        LastSentenceNo = DialoguePath;
        NPCTalking = false;
        NewConversation = true;
        StartDialogue = false;
        TextBoxVal.text = "";
    }

    private void OnTriggerEnter2D(Collider2D collision) // Used for detecting if the player has walked in range of the NPC
    {
        if (collision.name == "HunterBox")
        {
            NPCTalking = true;
        }
    }

    private void StartTalking()
    {
        Vector2 rayPos = new Vector2(Camera.main.ScreenToWorldPoint(Input.mousePosition).x, Camera.main.ScreenToWorldPoint(Input.mousePosition).y);
        RaycastHit2D hit = Physics2D.Raycast(rayPos, Vector2.zero, 100f);

        if (hit && hit.collider.CompareTag("NPC") == true && !StartDialogue) // Checking tag of hit sprite
        {
            StartDialogue = true;
            Debug.Log("Click");
        }
    }

    public void ButtonToTalk()
    {
        NPCTalking = true;
    }
}
