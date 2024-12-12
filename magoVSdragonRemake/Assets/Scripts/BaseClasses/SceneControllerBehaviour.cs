using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneControllerBehaviour : MonoBehaviour
{
    protected AudioManager audioManager;
    protected GameManager gameManager;
    protected ScenesManager scenesManager;
    protected LanguageManager languageManager;

    bool isGameplayScene = false; 

    protected float myGameplayTimeLimit;

    protected virtual void Start()
    {        
        scenesManager.FadeIn();
        gameManager.SetGameplayTimeLimit(myGameplayTimeLimit);
    }  

    protected virtual void Update()
    {
        if(!isGameplayScene)
        {
            return;
        }        
    }

    internal void ChangeScene(EnumManager.Scenes _newScene)
    {
        StartCoroutine(ChangeSceneCR(_newScene));
    }

    IEnumerator ChangeSceneCR(EnumManager.Scenes _newScene)
    {
        scenesManager.FadeOut();
        yield return new WaitForSeconds(1);

        //scenesManager.ChangeScene(_newLevel);
        SceneManager.LoadScene(_newScene.ToString());
    }

    protected virtual void Exit()
    {        
        if(gameManager.GetIsPaused()) // check if necessary because is called from pause event
        {
            gameManager.Pause();
        }
        if(isGameplayScene)
        {
            gameManager.ResetAll();
        }                     
    }

    #region Getters&Setters
    protected void SetMyGameplayTimeLimit(float _newValue)
    {
        myGameplayTimeLimit = _newValue;
    }
    
    protected void SetIsGameplayScene(bool _newState)
    {   
        isGameplayScene = _newState;
        scenesManager.SetIsCurrentSceneGameplay(_newState);
    }

     internal void SetActivePauseSubMenu(bool _newState)
    {
        //pauseSubmenu.SetActive(_newState);
    }
    #endregion

    protected virtual void OnEnable()
    {
        audioManager = AudioManager.audioManager;
        gameManager = GameManager.gameManager;
        scenesManager = ScenesManager.scenesManager;
        //Debug.Log(gameObject.name +  ".scenesManager= " + scenesManager);
        languageManager = LanguageManager.languageManager;

        PauseHelper.onExit += Exit;
        //Debug.Log(gameObject.name +  " subscribe to on exit of pause helper");
    }

    protected virtual void OnDisable()
    {
        PauseHelper.onExit -= Exit;
        //Debug.Log(gameObject.name +  " unsubscribe to on exit of pause helper");
    }
}
