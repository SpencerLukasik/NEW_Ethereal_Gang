using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInfo : MonoBehaviour
{
    private float numOfPlayers;
    void Start()
    {
        numOfPlayers = 0f;
    }

    public void increment()
    {
        numOfPlayers += 1f;
    }
}