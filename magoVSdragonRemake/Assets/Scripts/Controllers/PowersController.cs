using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowersController : MonoBehaviour
{        
    [SerializeField]
    PowerBehaviour[] powers;  

    [SerializeField]
    Transform[] spawnPoints; 
    
    GameManager gameManager;
    AudioManager audioManager;
    
    bool isPower;    

    //float appearPowerCooldownMin = 5;
    float appearPowerCooldownMin = 4; // for test
    //float appearPowerCooldownMax = 15;
    float appearPowerCooldownMax = 5; // for test

    int randomPowerIndex;

    EnumManager.Powers itemGotten;

    void Start()
    {
        gameManager = GameManager.gameManager;
        audioManager = AudioManager.audioManager;

        SetIsPower(false);

        DeactivateAllPowers();

        StartCoroutine(ActivateRandomPowersCR());
    }   

    void DeactivateAllPowers()
    {
        foreach(PowerBehaviour power in powers)
        {
            power.EnableDisableMe(false);
        }
    }

    IEnumerator ActivateRandomPowersCR()
    {
        //Debug.Log("activate random powers cr");
        yield return new WaitForSeconds(Random.Range(appearPowerCooldownMin, appearPowerCooldownMax));
        //Debug.Log("isPower= " + isPower);
        //Debug.Log("is playing= " + gameManager.GetIsPlaying());
        if(!isPower && gameManager.GetIsPlaying())
        {
            if(gameManager.GetCurrentGameState() != EnumManager.GameStates.SuddenDeath)
            {
                //Debug.Log("activate power");
                randomPowerIndex = Random.Range(0, powers.Length);            
                while(powers[randomPowerIndex].GetMyPower() == EnumManager.Powers.SuperTime)
                {
                    //Debug.Log("get other random power index");
                    randomPowerIndex = Random.Range(0, powers.Length);            
                }
                ActivatePower(randomPowerIndex);  
                //ActivatePower(SearchPower(EnumManager.Powers.Change));  // for test                 
            }
            else if(gameManager.GetCurrentGameState() == EnumManager.GameStates.SuddenDeath)
            {
                //Debug.Log("activate super time");
                ActivatePower(SearchPower(EnumManager.Powers.SuperTime));
            }
        }        
    }

    int SearchPower(EnumManager.Powers _power)
    {
        foreach(PowerBehaviour power in powers)
        {
            if(power.GetMyPower() == _power)
            {
                return power.transform.GetSiblingIndex();                               
            }
        }      
        Debug.Log(_power + "not found");
        return 0;                    
    }

    void ActivatePower(int _index)
    {
        //Debug.Log("activate power");
        audioManager.Play(EnumManager.Sounds.Power);
        SetIsPower(true);

        Vector3 newPosition = spawnPoints[Random.Range(0, spawnPoints.Length - 2)].position;         
        powers[_index].SetPosition(newPosition);
        powers[_index].EnableDisableMe(true);
        
        //Instantiate(powers[powerIndex], newPosition, Quaternion.identity, transform); 
    }
    
    void ReactToPowerGotten()
    {
        SetIsPower(false);
        //if(gameManager.GetCurrentGameState() != EnumManager.GameStates.SuddenDeath)                
        StartCoroutine(ActivateRandomPowersCR());        
    }

    void PrepareSuddenDeath()
    {
        //Debug.Log("prepare sudden death");
        SetIsPower(false);        
        DeactivateAllPowers();        
        StartCoroutine(ActivateRandomPowersCR());     
    }

    #region  Getters & Setters
    internal void SetIsPower(bool _newState)
    {
        isPower = _newState;
    }

    internal void SetItemGotten(EnumManager.Powers _itemGotten)
    {
        itemGotten = _itemGotten;
    }

    internal EnumManager.Powers GetItemGotten()
    {
        return itemGotten;
    }
    #endregion


    void OnEnable()
    {
        CastlesController.onSuddenDeath += PrepareSuddenDeath;
        PowerBehaviour.onPowerGottenWP += ReactToPowerGotten;
    }

    void OnDisable()
    {
        CastlesController.onSuddenDeath -= PrepareSuddenDeath;
        PowerBehaviour.onPowerGottenWP -= ReactToPowerGotten;
    }

}

