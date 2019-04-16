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

    // Start is called before the first frame update
    void Start()
    {
        PlayerNavAgent = GameObject.Find("Cally V1 Model@Idle").GetComponent<NavMeshAgent>();
        PoliceNavAgent = GameObject.Find("Douglas@Idle").GetComponent<NavMeshAgent>();
        OcultistNavAgent = GameObject.Find("Miranda").GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        UpdateNodePositions(); // Used to see where the characters will move to 
        CheckBools();

        if (StartEvent)
        {
            LockInputs();

            // Switch to wide angle camera 
            SwitchToWideShot();

            GoToPosition(PlayerNavAgent, Node1Pos); // Player walks into the room

            RunAtCharacter(PoliceNavAgent, PlayerNavAgent.transform.position); // Policeman runs up to player 

            NPCInteractionScrpt.StartInteraction(); // Start dialogue with the policeman

            // Dialogue with policeman ends and he walks off

            GoToPosition(PoliceNavAgent, Node3Pos);

            if (PoliceNavAgent.transform.position == Node3Pos)
            {
                Policeman.SetActive(false);

                // Ocultist waks upto player
                RunAtCharacter(OcultistNavAgent, PlayerNavAgent.transform.position);

            }

            // if ocultist is infront of player start talking
            if (OcultistNavAgent.transform.position == new Vector3(PlayerNavAgent.transform.position.x + StoppingDistance, PlayerNavAgent.transform.position.y + StoppingDistance, PlayerNavAgent.transform.position.z + StoppingDistance))
            {
                ChangeOcultistTalkBool();
            }

            if (OcultistTalk)
            {
                // Switch back to player camera 
                SwitchToCloseUp();
                NPCInteractionScrpt.StartInteraction(Occultist);
            }

            if (EventEnd)
            {
                ChangeProneBool();
                UnlockInputs();
            }

        }
    }

    private void OnTriggerEnter(Collider other)
    {
        StartEvent = true;
    }

    public void LockInputs()
    {
        Mouse_Move.enabled = false; // Stop the player from being able to look around
        PlayerController.enabled = false; // Stop the player from being able to walk around
    }

    public void UnlockInputs()
    {
        Mouse_Move.enabled = true; // Allow the player to be able to look around
        PlayerController.enabled = true; // Allow the player to be able to walk around
    }

    public void RunAtCharacter(NavMeshAgent RunningCharacter, Vector3 TargetCharacter)
    {
        Vector2 CharacterPos = new Vector2 (TargetCharacter.x + StoppingDistance, TargetCharacter.z + StoppingDistance);
        RunningCharacter.destination = CharacterPos; // Running chracter will run at target character until they reach the stopping distance
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
    }

    public void CheckBools()
    {
        if (PWalkAway)
        {
            ChangePWalkAwayBool();
            GoToPosition(PlayerNavAgent, Node2Pos); // Runs the player character to node 2 
        }

        if (Prone)
        {
            // Cheating and just lowering the chracter into the floor
            Player.transform.position = new Vector3(Player.transform.position.x, Player.transform.position.y - ProneValue, Player.transform.position.z);
        }

        if (!Prone)
        {
            Player.transform.position = Player.transform.position;
        }

        if (Arrest)
        {
            // do stuff for being arrested
            RunAtCharacter(PoliceNavAgent, PlayerNavAgent.transform.position);
            ChangeArrestBool();
        }

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
            GoToPosition(OcultistNavAgent, Node3Pos);
            if (OcultistNavAgent.transform.position == Node3Pos)
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
