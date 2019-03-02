using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInRoomScrpt : MonoBehaviour
{
    public Enemy_Move_NeedleWoman Enemy_Move_NeedleWoman;

    private void OnTriggerEnter(Collider other)
    {
        Enemy_Move_NeedleWoman.PlayerInRoom = true;
    }
}
