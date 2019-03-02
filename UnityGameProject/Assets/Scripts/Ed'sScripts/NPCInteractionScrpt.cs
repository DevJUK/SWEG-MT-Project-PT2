using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NPCInteractionScrpt : MonoBehaviour
{
    // Are you talking with a npc or not?
    [Header("Interacting with the NPC")]
    [Tooltip("Is the player interacting with the NPC")]
    public bool Interacting;

    [Header("Audio Manager")]
    public AudioManager AudioManager;

    // The canvas that will hold all of the text boxes for dialogue
    [Header("Dialogue Canvas")]
    [Tooltip("The canvas that will hold all of the text boxes for dialogue")]
    public GameObject InteractionCanvas;

    // Dictates who starts talking first in conversation
    [Header("Whos talking first")]
    [Tooltip("If true the player is talking first. If false the NPC will talk first")]
    public bool PlayerTalksFirst;
    public bool PlayerTalking;

    // Used to signify whos talking in conversation
    [Header("Whos Talking")]
    [Tooltip("Text box where the current talkers name goes")]
    public Text WhosTalking;
    [Tooltip("Players name goes here")]
    public string PlayerName;
    [Tooltip("NPC's name goes here")]
    public string NPCName;

    // Textbox where player dialogue will go
    [Header("Dialogue Box")]
    [Tooltip("Text box where the current dialogue line goes")]
    public Text DialogueBox;

    [Header("Player dialogue lines")]
    [Tooltip("Put any dialogue you want the player to say in here, remember to use the in text triggers")]
    public List<string> PlayerDialogue;

    [Header("Dialogue Option Canvas")]
    [Tooltip("The canvas that will hold the buttons for selecting dialogue options")]
    public GameObject DialogueOptionCanvas;

    private RaycastItems RaycastItems; // The script that tells us if we are looking at an item or npc
    public NPCDialogueScrpt NPCDialogueScrpt; // The script that has all the NPC's dialogue in it
    private bool NPCNameSet; // Has the NPC's name been set yet?

    [Header("Dialogue Path Values")]
    public int DialoguePath; // The path your currently on
    public int StartingDialoguePath; // The path youll start on
    public int CurrentPlayerDialoguePath; // The most recent path read from the player's dialogue
    public int CurrentNPCDialoguePath; // The most recent path read from the NPC'sdialogue
    public int LastDialoguePath; // The last dialogue path that was read

    [Header("Current Character number")]
    public int CharNo;

    [Header("Dialogue Value")]
    public string DialogueValue;

    [Header("Print Sentence In One Go")]
    [Tooltip("If true will print all of the sentence in one go. If false will print using typewriter effect")]
    public bool PrintSentenceInOneGo;

    [Header("Typewriter Coroutine Values")]
    public bool CoroutineStarted;
    public float CoroutineDelay;

    [Header("Position in Dialogue")]
    public bool ConversationStart;
    public bool EndOfSentence;
    public bool NewConversation;
    public bool PrintingDialogue;
    public bool FirstSwitch;

    [Header("Initial Line")]
    public int InitialPlayerLine;
    public int InitialNPCLine;

    void Start()
    {
        // Make sure the dialogue box is empty when script is run
        DialogueBox.text = "";

        RaycastItems = GameObject.Find("Camera").GetComponent<RaycastItems>();

        ConversationStart = true;
    }

    void Update()
    {
        ShowHideInteractionUI();

        if (Interacting)
        {
            if (!NPCNameSet) // If the NPC's name hasnt been set yet then set it
            {
                NPCName = NPCDialogueScrpt.NPCName;
                NPCNameSet = true;
            }

            if (ConversationStart)
            {
                NPCNameSet = false;
                if (PlayerTalksFirst) // Checking whos dialogue will come up first
                {
                    WhosTalking.text = PlayerName;
                    PlayerTalking = true;
                }
                else
                {
                    WhosTalking.text = NPCName;
                    PlayerTalking = false;
                }
                FirstSwitch = true;
                CurrentNPCDialoguePath = 0;
                CurrentPlayerDialoguePath = 0;

            }

            if (PlayerTalking)
            {
                if (NewConversation) // If this isnt the first conversation with the NPC then start at the last known dialogue path for that npc
                {
                    StartingDialoguePath = LastDialoguePath;
                    WhosTalking.text = PlayerName;
                }
                else
                {
                    StartingDialoguePath = NPCDialogueScrpt.PlayersStartingDiologuePath; // If it is the first conversation with the NPC then just use the player start point
                    WhosTalking.text = PlayerName;
                }
            }
            else
            {
                if (NewConversation) // If this isnt the first conversation with the NPC then start at the last known dialogue path for that npc
                {
                    {
                        StartingDialoguePath = LastDialoguePath;
                        WhosTalking.text = NPCName;
                    }
                }
                else
                {
                    StartingDialoguePath = NPCDialogueScrpt.NPCsStartingDiologuePath; // If it is the first conversation with the NPC then just use the NPC start point
                    WhosTalking.text = NPCName;
                }
            }
            ReadDialogue();
        }
    }

    public void StartInteraction() // Call this to start interacting..... duh
    {
        // Used to get the dialogue script from the NPC the player is currently interacting with
        NPCDialogueScrpt = GameObject.Find(RaycastItems.NameofNPCHit).GetComponent<NPCDialogueScrpt>(); 

        Interacting = true;
    }

    public void StartInteraction(GameObject Hit) // Call this to start interacting..... duh
    {
        // Used to get the dialogue script from the NPC the player is currently interacting with
        NPCDialogueScrpt = Hit.GetComponent<NPCDialogueScrpt>();

        Interacting = true;
    }

    public void ShowHideInteractionUI() // If interacting with NPC bring up ui element with text etc...
    {
        if (Interacting)
        {
            InteractionCanvas.SetActive(true);
        }
        else
        {
            InteractionCanvas.SetActive(false);
        }
    }

    public void ReadDialogue() // Used to ditermine what to do i.e. print text or act on in text logic
    {
        ConversationStart = false;
        Debug.Log("Read Dialogue");

        if (PrintingDialogue) // If text is being printed into dialogue box then dont check for logic
        {
            PrintDialogue();
        }
        else // If text isnt being printed into dialogue box then check for logic
        {
            if (PlayerTalking)
            {
                DialogueValue = PlayerDialogue[DialoguePath];
            }
            else
            {
                DialogueValue = NPCDialogueScrpt.NPCDialogue[DialoguePath];
            }
            Debug.Log("Check if logic");
            if (DialogueValue == "#End") // Ends dialogue (use at the very end of dialogue to stop errors)
            {
                EndOfDialogueBlock();
                Debug.Log("End");
            }
            else if (DialogueValue.Contains("#Break")) // Creates a break in the dialogue (use for whole new conversations i.e after the player walks off and does something)
            {
                BreakInDialogue();
                Debug.Log("Break");
            }

            else if (DialogueValue.Contains("#LP:")) // Loop back to set point in dialogue (note LP == Loop)
            {
                int NewLoop = System.Convert.ToInt16(DialogueValue.Remove(0, 4));
                StartingDialoguePath = NewLoop;
                DialoguePath++;
            }

            else if (DialogueValue.Contains("#PS:")) // Use the audio manager to play a sound (Put sound Index number after : ) (Note PS == Play Sound)
            {
                PlaySound();
            }

            else if (DialogueValue.Contains("#Switch")) // Used to switch between player and NPC dialogue
            {
                Switch();
            }

            else if (DialogueValue.Contains("#Landing")) // Used as a landing spot for the switch to stop it from flicking back and forth
            {
                Debug.Log("Landing");
                DialoguePath++;
            }

            else if (DialogueValue.Contains("#TConfirm:")) // Used to ask the player if they want to trade (could be expanded later to enable support for multiple dialogue options)
            {
                Debug.Log("Confirming Trade");
                TradeConfirmation();
            }

            else if (DialogueValue.Contains("#TradeScript")) // Calls the script that controlls trading
            {
                Debug.Log("Starting Trade");
                StartTrading();
            }

            else
            {
                PrintDialogue();
            }
        }
       
    }

    public void PrintDialogue()
    {
        Debug.Log("Print Dialogue");
        if (PlayerTalking)
        {
            if (PrintSentenceInOneGo && !EndOfSentence)
            {
                PrintFullSentencePlayer();
            }
            else
            {
                if (!CoroutineStarted && !EndOfSentence)
                {
                    Debug.Log("Typewriter Player");
                    StartCoroutine(TypewriterPrintPlayer());
                    CoroutineStarted = true;
                }
            }
            if (EndOfSentence)
            {
                ProgressToNextSentence();
            }
        }
        else
        {
            if (PrintSentenceInOneGo && !EndOfSentence)
            {
                PrintFullSentenceNPC();
            }
            else
            {
                if (!CoroutineStarted && !EndOfSentence)
                {
                    Debug.Log("Typewriter NPC");
                    StartCoroutine(TypewriterPrintNPC());
                    CoroutineStarted = true;
                }
            }
            if (EndOfSentence)
            {
                ProgressToNextSentence();
            }
        }
    }

    public void PrintFullSentencePlayer()
    {
        Debug.Log("Print Full Sentence Player");
        // Prints the full line of dialogue in one go
        DialogueBox.text = PlayerDialogue[DialoguePath];
        EndOfSentence = true;
    }

    IEnumerator TypewriterPrintPlayer() // Runs through each character individually and prints them with a pause between each one
    {
        PrintingDialogue = true;
        Debug.Log("Typewriter print Player");
        for (int CharNo = 0; CharNo < (PlayerDialogue[DialoguePath].Length + 1); CharNo++)
        {
            DialogueBox.text = PlayerDialogue[DialoguePath].Substring(0, CharNo);
            
                yield return new WaitForSeconds(CoroutineDelay); // Wait can be edited in inspector, suggested delay is 0.03f
            
        }

        CoroutineStarted = false;
        EndOfSentence = true;
        PrintingDialogue = false;
    }

    public void PrintFullSentenceNPC()
    {
        Debug.Log("Print Full Sentence NPC");
        // Prints the full line of dialogue in one go
        DialogueBox.text = NPCDialogueScrpt.NPCDialogue[DialoguePath];
        EndOfSentence = true;
    }

    IEnumerator TypewriterPrintNPC() // Runs through each character individually and prints them with a pause between each one
    {
        PrintingDialogue = true;
        Debug.Log("Typewriter print NPC");
        for (int CharNo = 0; CharNo < (NPCDialogueScrpt.NPCDialogue[DialoguePath].Length + 1); CharNo++)
        {
            DialogueBox.text = NPCDialogueScrpt.NPCDialogue[DialoguePath].Substring(0, CharNo);

            yield return new WaitForSeconds(CoroutineDelay);

        }

        CoroutineStarted = false;
        EndOfSentence = true;
        PrintingDialogue = false;
    }

    public void EndOfDialogueBlock()
    {
        Debug.Log("End of dialogue");
        LastDialoguePath = DialoguePath;
        DialoguePath = 0;
        Interacting = false;
        DialogueBox.text = "";
        ConversationStart = true;
    }

    public void BreakInDialogue()
    {
        Debug.Log("Break In Dialogue");
        LastDialoguePath = DialoguePath;
        Interacting = false;
        NewConversation = true;
        DialogueBox.text = "";
        ConversationStart = true;
    }

    public void ProgressToNextSentence()
    {
        if( Input.GetMouseButton(0))
        {
            DialoguePath++;
            EndOfSentence = false;
        }
    }

    public void Switch()
    {
        Debug.Log("Switch Speaker");
        if (FirstSwitch)
        {
            if (PlayerTalking)
            {
                CurrentPlayerDialoguePath = DialoguePath;
                PlayerTalking = false;
                DialoguePath = NPCDialogueScrpt.NPCsStartingDiologuePath;
                DialoguePath++;
                FirstSwitch = false;
            }
            else
            {
                CurrentNPCDialoguePath = DialoguePath;
                PlayerTalking = true;
                DialoguePath = NPCDialogueScrpt.PlayersStartingDiologuePath;
                DialoguePath++;
                FirstSwitch = false;
            }
        }
        else
        {
            if (PlayerTalking)
            {
                CurrentPlayerDialoguePath = DialoguePath;
                PlayerTalking = false;
                DialoguePath = CurrentNPCDialoguePath;
                DialoguePath++;
            }
            else
            {
                CurrentNPCDialoguePath = DialoguePath;
                PlayerTalking = true;
                DialoguePath = CurrentPlayerDialoguePath;
                DialoguePath++;
            }
        }
    }

    public void PlaySound()
    {
        int SoundIndexNo = System.Convert.ToInt16(DialogueValue.Remove(0, 4));
        AudioManager.PlayClip(SoundIndexNo, 1, 1);
        Debug.Log("Play Sound: " + SoundIndexNo);
        DialoguePath++;
    }

    public void TradeConfirmation() // Turns on canvas so dialogue option can be chosen 
    {
        // +++++++++++++++++++++++++++++++++++++++++++++++++++++++ Lock camera movement +++++++++++++++++++++++++++++++++++++++++++++++++++++++++
        InteractionCanvas.SetActive(false);
        DialogueOptionCanvas.SetActive(true);
        Interacting = false;
    }

    public void PlayerPickedYes() // jumps the player to the dialogue in responce to them saying yes
    {
        DialoguePath++;
        DialogueOptionCanvas.SetActive(false);
        // InteractionCanvas.SetActive(true);
        StartInteraction();
    }

    public void PlayerPickedNo() // jumps the player to the dialogue in responce to them saying no
    {
        int route = System.Convert.ToInt16(DialogueValue.Remove(0, 10));
        DialoguePath = route;
        DialogueOptionCanvas.SetActive(false);
        // InteractionCanvas.SetActive(true);
        StartInteraction();
    }

    public void StartTrading() // calls the trading script
    {
        // Call your code here sam!! +++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
    }
}
