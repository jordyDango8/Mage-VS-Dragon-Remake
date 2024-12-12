using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainScreenSceneController : SceneControllerBehaviour
{    
    protected override void Start()
    {
        base.Start();
        audioManager.Play(EnumManager.Sounds.MainTheme);
    }

    public void GoToMainMenu()
    {
        ChangeScene(EnumManager.Scenes.MainMenu);
    }

    public void ExitEvent()
    {
        Application.Quit();
    }
}
