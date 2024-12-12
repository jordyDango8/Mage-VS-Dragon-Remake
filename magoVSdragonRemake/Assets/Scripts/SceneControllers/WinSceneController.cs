using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class WinSceneController : SceneControllerBehaviour
{    
    [SerializeField]
    TextMeshProUGUI winnerTMP;    

    protected override void Start()
    {
        base.Start();
        audioManager.Play(EnumManager.Sounds.Win);
        SetWinnerText();
    }

    void SetWinnerText()
    {
        switch(languageManager.GetCurrentLanguage())
        {
            case EnumManager.Languages.English:
                winnerTMP.text = gameManager.GetWinner().ToString() + " won.";
                break;
            case EnumManager.Languages.Spanish:
                if(gameManager.GetWinner() == EnumManager.Characters.Mage)
                {
                    winnerTMP.text = "Mago ganó.";
                }
                else
                {
                    winnerTMP.text = "Dragón ganó.";
                }
                break;

            default:
                Debug.Log("no language");
                break;
        }
    }

    public void PlayAgain()
    {
        gameManager.ResetAll();
        ChangeScene(EnumManager.Scenes.LevelsMenu);
    }

    public void ExitEvent()
    {
        gameManager.ResetAll();
        ChangeScene(EnumManager.Scenes.MainScreen);
    }
}
