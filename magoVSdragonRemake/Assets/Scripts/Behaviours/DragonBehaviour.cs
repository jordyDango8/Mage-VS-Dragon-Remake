using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Security.Cryptography;
using UnityEngine;

public class DragonBehaviour : CharacterBehaviour
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
        startUp = "i";
        startRight = "l";
        startDown = "k";
        startLeft = "j";   

        ResetSpeed();
        ResetControls();        
    }
    
}
