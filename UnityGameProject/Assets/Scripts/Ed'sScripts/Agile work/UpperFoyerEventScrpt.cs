using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class UpperFoyerEventScrpt : MonoBehaviour
{
    [Header("Chracter Models")]
    [Tooltip("The player's model goes here")]
    public GameObject Player;
    [Tooltip("The policeman's model goes here")]
    public GameObject Policeman;
    [Tooltip("The occultist's model goes here")]
    public GameObject Occultist;

    [Header("Other Scripts")]
    [Tooltip("Drag the players model here")]
    public Mouse_Move Mouse_Move;
    [Tooltip("Drag the players model here")]
    public PlayerController PlayerController;
    [Tooltip("Drag the players model here")]
    public NPCInteractionScrpt NPCInteractionScrpt;
    [Tooltip("Drag the inventory gameobject here")]
    public InventoryScript InventoryScript;

    [Header("Inventory Items")]
    [Tooltip("Drag the handcuff keys here")]
    public Item HandcuffKeys;
    [Tooltip("Drag the handcuffs here")]
    public Item Handcuffs;
    [Tooltip("Drag the crystalball here")]
    public Item CrystalBall;

    [Header("Event Cameras")]
    [Tooltip("Drag the PlayerCamera gameobject here")]
    public GameObject PlayerCamera;
    [Tooltip("Drag the UpperFoyerEventCamera gameobject here")]
    public GameObject UpperFoyerEventCamera;

    private bool StartEvent = false;

    private Vector3 PlayerPos;
    private Vector3 PolicemanPos;
    private Vector3 OcultistPos;

    [Header("Stopping Distance")]
    [Tooltip("How far away the character will be before they stop")]
    public float StoppingDistance;

    private NavMeshAgent PlayerNavAgent;
    private NavMeshAgent PoliceNavAgent;
    private NavMeshAgent OcultistNavAgent;

    [Header("Character Animators")]
    [Tooltip("Drag the players model here")]
    public Animator PlayerAnim;
    [Tooltip("Drag the policemans model here")]
    public Animator PolicemanAnim;
    [Tooltip("Drag the ocultist model here")]
    public Animator OcultistAnim;

    [Header("Cutscene Navigation Nodes")]
    [Tooltip("How far the player will walk into the room")]
    public GameObject Node1Marker;
    public Vector3 Node1Pos;
    [Tooltip("How far the player will walk away from the policeman")]
    public GameObject Node2Marker;
    public Vector3 Node2Pos;
    [Tooltip("Exit position for npc characters")]
    public GameObject Node3Marker;
    public Vector3 Node3Pos;
    [Tooltip("Policeman stopping position")]
    public GameObject Node4Marker;
    public Vector3 Node4Pos;
    [Tooltip("Policeman aresting position")]
    public GameObject Node5Marker;
    public Vector3 Node5Pos;

    public float ProneValue;

    private bool Prone;
    private bool PWalkAway;
    private bool Arrest;
    private bool OcultistTalk;
    private bool GiveHandcuffs;
    private bool GiveHandcuffKeys;
    private bool TakeCrystalBall;
    private bool OcultistWalkAway;
    private bool EventEnd;
    private bool PolicemanTalking;
    private bool WalkUpToPlayer;
    private bool PlayerEnterRoom;
    private bool WideShot;

    // Start is called before the first frame update
    void Start()
    {
        PlayerNavAgent = GameObject.Find("Cally V1 Model@Idle").GetComponent<NavMeshAgent>();
        PoliceNavAgent = GameObject.Find("Douglas@Idle").GetComponent<NavMeshAgent>();
        OcultistNavAgent = GameObject.Find("Miranda").GetComponent<NavMeshAgent>();
        WalkUpToPlayer = true;
        PlayerEnterRoom = true;
        EventEnd = false;
        Arrest = false;
    }

    public void FixedUpdate()
    {
        CheckBools();
    }
    // Update is called once per frame
    void Update()
    {
        UpdateNodePositions(); // Used to see where the characters will move to 


        if (StartEvent)
        {

            // Switch to wide angle camera 
            if (WideShot)
            {
                LockInputs();
                SwitchToWideShot();
            }
            else if (!WideShot)
            {
                SwitchToCloseUp();
            }

            if (PlayerEnterRoom)
            {
                // Player walks into the room
                GoToPosition(PlayerNavAgent, new Vector3(Node1Pos.x, PlayerNavAgent.transform.position.y, Node1Pos.z));
                PlayerEnterRoom = false;
            }

            // Turn off players animations when they reach their node
            if (PlayerNavAgent.transform.position == new Vector3(Node1Pos.x, PlayerNavAgent.transform.position.y, Node1Pos.z))
            {
                PlayerAnim.SetBool("IsWalking", false);
                PlayerAnim.SetBool("IsRunning", false);
            }

            if (WalkUpToPlayer)
            {
                Debug.Log("Running to character");
                // Policeman runs up to player
                GoToPosition(PoliceNavAgent, new Vector3(Node4Pos.x, PoliceNavAgent.transform.position.y, Node4Pos.z)); 
            }

            if (PoliceNavAgent.transform.position == new Vector3(Node4Pos.x, PoliceNavAgent.transform.position.y, Node4Pos.z))
            {
                PolicemanTalking = true;
                WalkUpToPlayer = false;
                NPCInteractionScrpt.StartInteraction(Policeman); // Start dialogue with the policeman
            }

            // Dialogue with policeman ends and he walks off
            if (!PolicemanTalking && !WalkUpToPlayer)
            {
                GoToPosition(PoliceNavAgent, new Vector3(Node3Pos.x, PoliceNavAgent.transform.position.y, Node3Pos.z));
            }

            if (PoliceNavAgent.transform.position == new Vector3(Node3Pos.x, PoliceNavAgent.transform.position.y, Node3Pos.z))
            {
                Policeman.SetActive(false);

                // Ocultist walks upto player
                RunAtCharacter(OcultistNavAgent, new Vector3(Node5Pos.x, OcultistNavAgent.transform.position.y, Node5Pos.z));

            }

            // if ocultist is infront of player start talking
            if (OcultistNavAgent.transform.position == new Vector3(Node5Pos.x, OcultistNavAgent.transform.position.y, Node5Pos.z))
            {
                ChangeOcultistTalkBool();
            }

            if (OcultistTalk)
            {
                WideShot = false;
                if (!WideShot)
                {
                    SwitchToCloseUp();
                }
                NPCInteractionScrpt.StartInteraction(Occultist);
                PlayerNavAgent.isStopped = true;
            }

            if (EventEnd)
            {
                UnlockInputs();

                Debug.Log("Event end: " + EventEnd);
                if (OcultistNavAgent.transform.position == new Vector3(Node3Pos.x, OcultistNavAgent.transform.position.y, Node3Pos.z))
                {
                    Occultist.SetActive(false);
                    ChangeProneBool();
                }

            }

        }
    }

    private void OnTriggerEnter(Collider other)
    {
        StartEvent = true;
        WideShot = true;
    }

    public void LockInputs()
    {
        Mouse_Move.enabled = false; // Stop the player from being able to look around
        PlayerController.enabled = false; // Stop the player from being able to walk around
    }

    public void UnlockInputs()
    {
        Debug.Log("Unlock Input");
        Mouse_Move.enabled = true; // Allow the player to be able to look around
        PlayerController.enabled = true; // Allow the player to be able to walk around
    }

    public void RunAtCharacter(NavMeshAgent RunningCharacter, Vector3 TargetCharacter)
    {
        RunningCharacter.destination = TargetCharacter;
    }

    public void GoToPosition(NavMeshAgent RunningCharacter, Vector3 TargetPosition)
    {
        RunningCharacter.destination = TargetPosition; // Running Character will run to target position then stop
    }

    public void UpdateNodePositions()
    {
        Node1Pos = Node1Marker.transform.position;
        Node2Pos = Node2Marker.transform.position;
        Node3Pos = Node3Marker.transform.position;
        Node4Pos = Node4Marker.transform.position;
        Node5Pos = Node5Marker.transform.position;
    }

    public void CheckBools()
    {
        Debug.Log("Check Bools");
        if (PWalkAway)
        {
            Debug.Log("PWalkAway: " + PWalkAway);
            GoToPosition(PlayerNavAgent, Node2Pos); // Runs the player character to node 2 

            // Turn off animations when chracter gets to node
            if (PlayerNavAgent.transform.position == new Vector3(Node2Pos.x, PlayerNavAgent.transform.position.y, Node2Pos.z))
            {
                PlayerAnim.SetBool("IsWalking", false);
                PlayerAnim.SetBool("IsRunning", false);
            }
            ChangePWalkAwayBool();
        }

        //if (Arrest && Prone)
        //{
        //    // Cheating and just lowering the chracter into the floor
        //    Debug.Log("Going prone");
        //    Player.transform.position = new Vector3(Player.transform.position.x, Player.transform.position.y - ProneValue, Player.transform.position.z);
        //}

        //if (Prone && !Arrest)
        //{
        //    Player.transform.position = Player.transform.position;
        //}

        if (Arrest)
        {
            RunAtCharacter(PoliceNavAgent, Node5Pos);

            // do stuff for being arrested
            if (PoliceNavAgent.transform.position == new Vector3(Node5Pos.x, PoliceNavAgent.transform.position.y, Node5Pos.z))
            {
                ChangeProneBool();
                Arrest = false;
            }
        }

        //if (!Arrest)
        //{
        //    ChangeProneBool();
        //}

        if (GiveHandcuffKeys)
        {
            ChangeHandcuffKeysBool();
            InventoryScript.AddItem(HandcuffKeys);
        }

        if (GiveHandcuffs)
        {
            ChangeHandcuffsBool();
            InventoryScript.AddItem(Handcuffs);
        }

        if (TakeCrystalBall)
        {
            ChangeCrystalBallBool();
            InventoryScript.RemoveItem(CrystalBall);
        }

        if (OcultistWalkAway)
        {
            GoToPosition(OcultistNavAgent, new Vector3(Node3Pos.x, OcultistNavAgent.transform.position.y, Node3Pos.z));
            if (OcultistNavAgent.transform.position == new Vector3(Node3Pos.x, OcultistNavAgent.transform.position.y, Node3Pos.z))
            {
                Occultist.SetActive(false);
                ChangeOcultistWalkAwayBool();
            }
        }
    }

    public void ChangeOcultistTalkBool()
    {
        OcultistTalk = !OcultistTalk;
    }

    public void ChangeCrystalBallBool()
    {
        TakeCrystalBall = !TakeCrystalBall;
    }

    public void ChangeHandcuffsBool()
    {
        GiveHandcuffs = !GiveHandcuffs;
    }

    public void ChangeHandcuffKeysBool()
    {
        GiveHandcuffKeys = !GiveHandcuffKeys;
    }

    public void ChangeProneBool()
    {
        Prone = !Prone;
    }

    public void ChangePWalkAwayBool()
    {
        PWalkAway = !PWalkAway;
    }

    public void ChangeArrestBool()
    {
        Arrest = !Arrest;
    }

    public void TalkWithOcultist()
    {
        OcultistTalk = true;
    }

    public void ChangeOcultistWalkAwayBool()
    {
        OcultistWalkAway = !OcultistWalkAway;
    }

    public void ChangePolicemanTalkingBool()
    {
        PolicemanTalking = !PolicemanTalking;
    }

    public void ChangeEventEndBool()
    {
        EventEnd = !EventEnd;
    }

    // Camera Stuff
    public void SwitchToWideShot()
    {
        UpperFoyerEventCamera.SetActive(true);
        PlayerCamera.SetActive(false);
    }

    public void SwitchToCloseUp()
    {
        PlayerCamera.SetActive(true);
        UpperFoyerEventCamera.SetActive(false);
    }
}
