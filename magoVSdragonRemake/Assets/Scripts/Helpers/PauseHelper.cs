using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PauseHelper : MonoBehaviour
{
    internal delegate void OnExit();
    internal static event OnExit onExit;

    protected AudioManager audioManager;
    protected GameManager gameManager;
    protected ScenesManager scenesManager;    

    [SerializeField]
    GameObject pauseSubmenu;

    [SerializeField]
    TextMeshProUGUI timerTMP;
    
    void Start()
    {
        scenesManager = ScenesManager.scenesManager;
        audioManager = AudioManager.audioManager;
        gameManager = GameManager.gameManager;
    }  

    void Update()
    {        
        //Debug.Log("onExit= " + onExit);

        if(!scenesManager.GetIsCurrentSceneGameplay() || !gameManager.GetIsPlaying())
        {
            return;
        }   

        if(Input.GetKeyDown("space"))
        {            
            audioManager.Play(EnumManager.Sounds.Pause);
            gameManager.Pause();
            pauseSubmenu.SetActive(gameManager.GetIsPaused());
        }  
        UpdateTimer();
    }

    void UpdateTimer()
    {                
        int reminingTime = (int)gameManager.GetRemainingTime() ;
        timerTMP.text = reminingTime.ToString();        
    }

    public void Exit()
    {
        //Debug.Log("exit");
        gameManager.Pause();
        pauseSubmenu.SetActive(gameManager.GetIsPaused());
                
        if(onExit != null)
        {
            //Debug.Log("call exit event");
            onExit();
        }
    }

}
