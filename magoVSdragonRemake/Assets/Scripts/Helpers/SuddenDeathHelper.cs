using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SuddenDeathHelper : ActivateDeactivateBase
{    
    void OnEnable()
    {
        GameplaySceneController.onPrepareSuddenDeath += Activate;
        GameplaySceneController.onStartSuddenDeath += Deactivate;
    }
    
    void OnDisable()
    {
        GameplaySceneController.onPrepareSuddenDeath -= Activate;
        GameplaySceneController.onStartSuddenDeath -= Deactivate;
    }
}
