using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeUpHelper : ActivateDeactivateBase
{
    void OnEnable()
    {
        GameplaySceneController.onTimeUp += Activate;
        GameplaySceneController.onPrepareSuddenDeath += Deactivate;
        GameplaySceneController.onGameOver += Deactivate;
    }
    
    void OnDisable()
    {
        GameplaySceneController.onTimeUp -= Activate;
        GameplaySceneController.onPrepareSuddenDeath -= Deactivate;
        GameplaySceneController.onGameOver -= Deactivate;
    }
}
