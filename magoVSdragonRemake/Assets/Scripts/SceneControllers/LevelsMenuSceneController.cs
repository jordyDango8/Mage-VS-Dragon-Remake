using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelsMenuSceneController : SceneControllerBehaviour
{        
    protected override void Start()
    {
        base.Start();
    }

    public void GoToLevel1()
    {
        ChangeScene(EnumManager.Scenes.Level1);
        audioManager.Stop(EnumManager.Sounds.MainTheme);
    }

    public void GoToLevel2()
    {
        ChangeScene(EnumManager.Scenes.Level2);
        audioManager.Stop(EnumManager.Sounds.MainTheme);
    }

    public void GoToLevel3()
    {
        ChangeScene(EnumManager.Scenes.Level3);
        audioManager.Stop(EnumManager.Sounds.MainTheme);
    }

    public void ExitEvent()
    {
        ChangeScene(EnumManager.Scenes.MainMenu);
    }

}
