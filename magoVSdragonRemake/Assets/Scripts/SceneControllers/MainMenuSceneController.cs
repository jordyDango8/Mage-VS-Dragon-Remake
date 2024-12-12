using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuSceneController : SceneControllerBehaviour
{    
    public void Play()
    {
        //Debug.Log("play");
        ChangeScene(EnumManager.Scenes.LevelsMenu);
    }

    public void ExitEvent()
    {
        ChangeScene(EnumManager.Scenes.MainScreen);
    }
}
