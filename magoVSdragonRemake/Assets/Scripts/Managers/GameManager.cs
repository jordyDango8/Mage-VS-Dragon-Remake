using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    internal static GameManager gameManager;    

    EnumManager.GameStates currentGameState;
    EnumManager.Characters winner;

    bool isPlaying = false;
    bool isPaused = false;

    float gameplayTimeCounter = 0; 

    float remainingTime;

    float gameplayTimeLimit; 

    void Awake()
    {
        if(gameManager == null)
        {
            gameManager = this;
            //Debug.Log(gameObject.name + ".gameManager= " + gameManager);
        }
        else
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);
    }        

    void Start()
    {
        //SetWinner(EnumManager.Characters.Mage); // for test
        //SetWinner(EnumManager.Characters.Dragon); // for test
    }

    void Update()
    {
        //Debug.Log("current gamestate= " + currentGameState);
        if(isPlaying)
        {
            TrackGamePlayTime();
            SetRemainingTime(gameplayTimeLimit - gameplayTimeCounter);
        }
    }

     void TrackGamePlayTime()
    {
        SetGameplayTimeCounter(gameplayTimeCounter + Time.deltaTime);        
    } 

    internal void Pause()
    {
        SetIsPaused(!isPaused);
        if(isPaused)
        {
            Time.timeScale = 0.0f;
        }
        else
        {
            Time.timeScale = 1.0f;
        }
    }    

    internal void ResetAll()
    {
        ResetIsPlaying();
        ResetIsPaused();
        ResetGameplayTimeCounter();
        ResetCurrentGameSatate();     
    } 

    internal void ResetIsPlaying()
    {
        SetIsPlaying(false);
    }

    internal void ResetIsPaused()
    {
        SetIsPaused(false);
    }

    internal void ResetGameplayTimeCounter()
    {
        //Debug.Log("reset gameplay time counter");
        SetGameplayTimeCounter(0); 
    }

    internal void ResetCurrentGameSatate()
    {
        SetCurrentGameState(EnumManager.GameStates.Normal);
    }

    /*
    internal void ResetRemainingTime() // check if necessary
    {
        SetRemainingTime(0);
    } 
    */ 

    #region Getters&Setters    
    internal float GetGameplayTimeCounter()
    {
        return gameplayTimeCounter;
    }

    internal void SetGameplayTimeCounter(float _newValue)
    {       
        gameplayTimeCounter = _newValue;
    }

    internal float GetRemainingTime()
    {
        return remainingTime;        
    }

    internal void SetRemainingTime(float _newValue)
    {
         if(_newValue < 0)
        {
            _newValue = 0;
        }
        remainingTime = _newValue;
    }

    internal float GetGameplayTimeLimit()
    {
        return gameplayTimeLimit;
    }

    internal void SetGameplayTimeLimit(float _newValue)
    {
        gameplayTimeLimit = _newValue;
    }


    internal EnumManager.Characters GetWinner()
    {
        return winner;
    }

    internal void SetWinner(EnumManager.Characters _newValue)
    {
        winner = _newValue;
    }

    internal EnumManager.GameStates GetCurrentGameState()
    {
        return currentGameState;
    }

    internal void SetCurrentGameState(EnumManager.GameStates _newValue)
    {
        currentGameState = _newValue;
    }
    
    internal bool GetIsPlaying()
    {
        return isPlaying;
    }    

    internal void SetIsPlaying(bool _newState)
    {
        isPlaying = _newState;
    }

    internal bool GetIsPaused()
    {
        return isPaused;
    }

    internal void SetIsPaused(bool _newState)
    {
        isPaused = _newState;
    }
    #endregion

}
