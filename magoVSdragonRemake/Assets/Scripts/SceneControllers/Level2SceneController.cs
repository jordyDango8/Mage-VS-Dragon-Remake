using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Level2SceneController : SceneControllerBehaviour
{    
    #region variables     
    internal delegate void OnGameStarted();
    internal static event OnGameStarted onGameStarted;

    internal delegate void OnHurryUp();
    internal static event OnHurryUp onHurryUp;

    internal delegate void OnTimeUp();
    internal static event OnTimeUp onTimeUp; 

    internal delegate void OnPrepareSuddenDeath();
    internal static event OnPrepareSuddenDeath onPrepareSuddenDeath;               
    
    internal delegate void OnStartSuddenDeath();
    internal static event OnStartSuddenDeath onStartSuddenDeath;   

    internal delegate void OnGameOver();
    internal static event OnGameOver onGameOver;                            
        
    float remainingTimeToHurryUp = 20.0f;  
    //float remainingTimeToHurryUp = 3.0f; // for test  
    #endregion  

    protected override void Start()
    {
        //SetMyGameplayTimeLimit(120);        
        SetMyGameplayTimeLimit(5); // for test
        base.Start();
        SetIsGameplayScene(true);
        StartCoroutine(CountdownCR());
    }

    protected override void Update()
    {
        base.Update();
        if(gameManager.GetIsPlaying())
        {            
            CheckToChangeCurrentGameState();                                
        }
    }

    IEnumerator CountdownCR() 
    {
        scenesManager.Countdown();        
        yield return new WaitForSeconds(scenesManager.GetWaitCountdown());

        audioManager.Play(EnumManager.Sounds.Game);                              
        gameManager.SetCurrentGameState(EnumManager.GameStates.Normal); 
        gameManager.SetIsPlaying(true);
        if(onGameStarted != null)
        {
            onGameStarted();
        }  
    }          

    void CheckToChangeCurrentGameState()
    {
        //Debug.Log("hurry up= " + EnumManager.GameStates.HurryUp);
        //Debug.Log("current game state= " + gameManager.GetCurrentGameState());
        if(gameManager.GetRemainingTime() <= remainingTimeToHurryUp &&
         gameManager.GetCurrentGameState() == EnumManager.GameStates.Normal)
        {
            //Debug.Log("hurry up mode");            
            gameManager.SetCurrentGameState(EnumManager.GameStates.HurryUp);            
            audioManager.Faster(EnumManager.Sounds.Game);
            if(onHurryUp != null)
            {
                //Debug.Log(gameObject.name + "call on hurry up");  
                onHurryUp();
            }
        }        
        
        //Debug.Log("gamePlayTimeCounter = " + gamePlayTimeCounter);
        //Debug.Log("gamePlayTimeLimit = " + gamePlayTimeLimit);
        //Debug.Log("sudden death= " + EnumManager.GameStates.SuddenDeath);
        //Debug.Log("current game state= " + gameManager.GetCurrentGameState());
        if(gameManager.GetRemainingTime() <= 0 &&         
         gameManager.GetCurrentGameState() == EnumManager.GameStates.HurryUp)
        {                    
            //Debug.Log("on time finished");
            gameManager.SetCurrentGameState(EnumManager.GameStates.SuddenDeath);     
            if(onTimeUp != null)
            {
                onTimeUp();
            }            
        }
    }          

    void SuddenDeath()
    {
        StartCoroutine(SuddenDeathCR());
    }

    IEnumerator SuddenDeathCR()
    {
        gameManager.SetIsPlaying(false);
        audioManager.Stop(EnumManager.Sounds.Game);
        audioManager.Slow(EnumManager.Sounds.Game);
        yield return new WaitForSeconds(1);
                
        scenesManager.FadeOut();
        yield return new WaitForSeconds(1);
        
        scenesManager.FadeIn();
        yield return new WaitForSeconds(1);
        
        if(onPrepareSuddenDeath != null)
        {
            // quit time text
            // show sudden death text
            onPrepareSuddenDeath();
        }
        yield return new WaitForSeconds(0.5f);
        
        gameManager.SetIsPlaying(true);
        audioManager.Play(EnumManager.Sounds.Game);
        if(onStartSuddenDeath != null)
        {
            onStartSuddenDeath();
        }
    }

    void GameOver(EnumManager.Characters _winner)
    {
        StartCoroutine(GameOverCR(_winner));
    }   

    IEnumerator GameOverCR(EnumManager.Characters _winner)
    {
        //Debug.Log("game over");        
        gameManager.SetIsPlaying(false);
        audioManager.Stop(EnumManager.Sounds.Game);
        audioManager.Slow(EnumManager.Sounds.Game);

        if(_winner == EnumManager.Characters.Mage)
        {
            gameManager.SetWinner(EnumManager.Characters.Mage);
        }
        else
        {
            gameManager.SetWinner(EnumManager.Characters.Dragon);
        }  
        //scenesManager.ChangeScene(EnumManager.Scenes.Win);
        ChangeScene(EnumManager.Scenes.Win);
        yield return new WaitForSeconds(1);

        if(onGameOver != null)
        {
            onGameOver();
        }
    } 

    protected override void Exit()
    {
        base.Exit();
        audioManager.Stop(EnumManager.Sounds.Game);
        audioManager.Slow(EnumManager.Sounds.Game);
        ChangeScene(EnumManager.Scenes.MainScreen);
        //ChangeScene(EnumManager.Scenes.Level1); // for test
    }

    protected override void OnEnable()
    {
        base.OnEnable();
        gameManager = GameManager.gameManager;
        audioManager = AudioManager.audioManager;    
        //scenesManager = ScenesManager.scenesManager;
        
        //PowerBehaviour.onItemGotten += UpdatePowersUI;
        CastlesController.onSuddenDeath += SuddenDeath; 
        CastlesController.onGameOver += GameOver;        
    }

    protected override void OnDisable()
    {
        base.OnDisable();
        //PowerBehaviour.onItemGotten -= UpdatePowersUI;
        CastlesController.onSuddenDeath -= SuddenDeath;
        CastlesController.onGameOver -= GameOver;        
    }

}
