//using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using UnityEngine.SceneManagement;

public class CastleBehaviour : MonoBehaviour
{       
    internal delegate void OnChangeBuildedState(int _amount);
    internal static event OnChangeBuildedState onChangeBuildedState;

    SpriteRenderer mySpriteRenderer;

    GameManager gameManager;

    AudioManager audioManager;  

    CastlesController castlesController; 

    EnumManager.CastleStates myCurrentState;

    void Awake()
    {
        castlesController = GetComponentInParent<CastlesController>();
        
        mySpriteRenderer = GetComponent<SpriteRenderer>();                       
    }        

    void Start()
    {
        //audioManager = AudioManager.audioManager;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        //Debug.Log("collided with " + other.gameObject.tag);
        if (other.gameObject.CompareTag(EnumManager.Tags.Mage.ToString()) && mySpriteRenderer.sprite == castlesController.GetDestroyed())
        {
            //Debug.Log("build");
            BeBuilded();          
        }

        if (other.gameObject.CompareTag(EnumManager.Tags.Dragon.ToString()) && mySpriteRenderer.sprite == castlesController.GetBuilded())
        {
            BeDestroyed();
        }
    }

    internal void BeBuilded()
    {
        //Debug.Log(gameObject.name +  ".gameManager= " + gameManager);
        //Debug.Log("IsPlaying= " + gameManager.GetIsPlaying());
        if(gameManager.GetIsPlaying())
        {
            audioManager.Play(EnumManager.Sounds.Build);
            if(onChangeBuildedState != null)
            {
                onChangeBuildedState(1);
            }
        }
        ChangeMySprite(castlesController.GetBuilded());
        SetMayCurrentState(EnumManager.CastleStates.Builded);
    }

    internal void BeDestroyed()
    {
        if(gameManager.GetIsPlaying())
        {
            audioManager.Play(EnumManager.Sounds.Destruction);
            if(onChangeBuildedState != null)
            {
                onChangeBuildedState(-1);
            }
        }
        ChangeMySprite(castlesController.GetDestroyed());
        SetMayCurrentState(EnumManager.CastleStates.Destroyed);
    }

    void ChangeMySprite(Sprite _newSprite)
    {
        mySpriteRenderer.sprite = _newSprite; 
    }

    internal void ChangePosition(Vector3 _newPosition)
    {
        transform.position = _newPosition;
    }

    internal EnumManager.CastleStates GetMyCurrentState()
    {
        return myCurrentState;
    }

    internal void SetMayCurrentState(EnumManager.CastleStates _newState)
    {
        myCurrentState = _newState;
    }

    void OnEnable()
    {
        //Debug.Log(gameObject.name +  ".on enable");
        gameManager = GameManager.gameManager;
        //Debug.Log(gameObject.name + ".gameManager= " + gameManager);
        audioManager = AudioManager.audioManager;
        //Debug.Log(gameObject.name +  ".gameManager= " + gameManager);
    }

}
