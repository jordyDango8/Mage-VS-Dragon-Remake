using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CountdownHelper : MonoBehaviour
{
    GameManager gameManager;
    ScenesManager scenesManager;

    [SerializeField]
    GameObject countDownSubmenu;

    [SerializeField]
    TextMeshProUGUI countdownTMP;
    
    int countdownCounter;
    
    //int waitCountdown = 0; //for test    

    internal void Countdown()
    {
        StartCoroutine(CountdownCR());
    }

    IEnumerator CountdownCR() 
    {
        SetActiveCountDownSubmenu(true);
        countdownCounter = scenesManager.GetWaitCountdown();
        while(countdownCounter > 0)
        {
            //Debug.Log("count");
            countdownTMP.text = countdownCounter.ToString();
            countdownCounter -= 1;
            yield return new WaitForSeconds(1f);
        }        
        SetActiveCountDownSubmenu(false);
    }

    #region Getters&Setters
    void SetActiveCountDownSubmenu(bool _newState)
    {
        countDownSubmenu.SetActive(_newState);
    }    
    #endregion

    void OnEnable()
    {
        gameManager = GameManager.gameManager;
        scenesManager = ScenesManager.scenesManager;
    }
}
