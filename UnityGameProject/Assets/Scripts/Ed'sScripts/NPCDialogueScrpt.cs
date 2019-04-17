using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCDialogueScrpt : MonoBehaviour
{
    [Header("NPC ID")]
    [Tooltip("Unique number granted to each npc (must be set manually)")]
    public int NpcIDNumber;

    [Header("NPC Name")]
    [Tooltip("Unique name granted to each npc (must be set manually)")]
    public string NPCName;

    [Header("Player's Starting Diologue Path")]
    [Tooltip("Where in the player's diologue does the diologue need to start (must be set manually)")]
    public int PlayersStartingDiologuePath;

    [Header("NPC's Starting Diologue Path")]
    [Tooltip("Where in the NPC's diologue does the diologue need to start (must be set manually)")]
    public int NPCsStartingDiologuePath;

    [Header("NPC Dialogue Lines")]
    [Tooltip("Put any dialogue you want the NPC to say in here, remember to use the in text triggers")]
    public List<string> NPCDialogue;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
