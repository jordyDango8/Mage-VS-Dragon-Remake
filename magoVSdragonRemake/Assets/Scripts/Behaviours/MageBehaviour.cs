using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

public class MageBehaviour : CharacterBehaviour
{
    protected override void Start()
    {
        base.Start();
        Initialize();
    }

    void Initialize()
    {
        startSpeed = 2;
        maxSpeedAllowed = startSpeed * 2;
        startUp = "w";
        startRight = "d";
        startDown = "s";
        startLeft = "a";   

        ResetSpeed();
        ResetControls();
        //SetMyKeys(up, right, down, left);     
    }
        
}
