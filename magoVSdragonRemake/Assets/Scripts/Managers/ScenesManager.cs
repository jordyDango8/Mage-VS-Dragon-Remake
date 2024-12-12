using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class ScenesManager : MonoBehaviour
{
    internal static ScenesManager scenesManager;    

    [SerializeField]
    CountdownHelper countdownHelper;

    [SerializeField]
    Animator fadeAnimator;

    int waitCountdown = 3;
    //int waitCountdown = 0; // for test

    bool isCurrentSceneGameplay = false;

    void Awake()
    {
        if(scenesManager == null)
        {
            scenesManager = this;
            //Debug.Log(gameObject.name +  ".scenesManager= " + scenesManager);
        }
        else
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);
    }    

    internal void Countdown()
    {
        countdownHelper.Countdown();
    }

    internal void FadeIn()
    {
        fadeAnimator.SetTrigger(EnumManager.AnimParameters.FadeIn.ToString());
        //fadeAnimator.Play(EnumManager.animClips.fadeIn.ToString());
    }

    internal void FadeOut()
    {
        fadeAnimator.SetTrigger(EnumManager.AnimParameters.FadeOut.ToString());
        //fadeAnimator.Play(EnumManager.animClips.fadeOut.ToString());
    }

    #region Getters&Setters
    //-------------------------------------------------------------------------------
    internal int GetWaitCountdown()    
    {
        return waitCountdown;
    }

    internal bool GetIsCurrentSceneGameplay()
    {        
        return isCurrentSceneGameplay;
    }    

    internal void SetIsCurrentSceneGameplay(bool _newState)
    {
        isCurrentSceneGameplay = _newState;
    }
    //-------------------------------------------------------------------------------
    #endregion

}
