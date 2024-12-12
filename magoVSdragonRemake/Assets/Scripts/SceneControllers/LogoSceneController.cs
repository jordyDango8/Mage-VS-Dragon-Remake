using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LogoSceneController : SceneControllerBehaviour
{
    //ScenesManager scenesManager;

    protected override void Start()
    {
        //scenesManager = ScenesManager.scenesManager;        
        //scenesManager.ChangeScene(EnumManager.Scenes.MainScreen);
        ChangeScene(EnumManager.Scenes.MainScreen);
    }


}
