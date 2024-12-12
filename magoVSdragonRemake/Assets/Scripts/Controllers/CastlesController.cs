
//using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using JordiArray;

public class CastlesController : MonoBehaviour
{
    internal delegate void OnSuddenDeath();
    internal static event OnSuddenDeath onSuddenDeath;

    internal delegate void OnGameOver(EnumManager.Characters _winner);
    internal static event OnGameOver onGameOver;

    GameManager gameManager;
    
    [SerializeField]
    GameObject[] castles;

    [SerializeField]
    Transform[] spawnPoints;

    [SerializeField]
    Sprite destroyed;
    
    [SerializeField]
    Sprite builded; 

    int totalCastles;

    int buildedCastles = 0;

    int[] spawnPointsIndex = new int[10];

    float waitBetweenApplaySuperTime = 0.5f;

    int[] randomArray = new int[10];

    void Start()
    {
        gameManager = GameManager.gameManager;

        totalCastles = castles.Length;

        buildedCastles = totalCastles / 2;

        randomArray = ArrayLibrary.GenerateIrrepetibleArray(randomArray, 0, randomArray.Length);
        SetSpawnPointsIndex(randomArray);
        GenerateCastles();
    }

    void GenerateIrrepetibleArray(int[] _array)
    {
        int newRandomValue;
        bool isRepeated;

        // loop array
        for(int i = 0; i < _array.Length; i++)
        {
            // generate new value
            newRandomValue = Random.Range(0, _array.Length);

            //  repeat while new value is repeated
            do
            {            
                // suppose isn't repeated
                isRepeated = false;

                // loop only assigned values
                for(int j = 0; j < i; j++)
                {
                    // if new random value == current
                    if(newRandomValue == _array[j])
                    {               
                        // generate new value  
                        newRandomValue = Random.Range(0, _array.Length);
                        
                        // tell is repeated
                        isRepeated = true;
                        
                        // not necessary check the rest
                        break;
                    }
                }
            } while(isRepeated);

            //assign new value
            _array[i] = newRandomValue;            
        }
    }    

    void GenerateCastles()
    {
        for(int i = 0; i < castles.Length; i++)
        {
            //Debug.Log("generate castle " + i);
            CastleBehaviour castleBehaviour = castles[i].GetComponent<CastleBehaviour>();            
            if(i < castles.Length / 2)
            {
                castleBehaviour.BeBuilded();
            }
            else
            {
                castleBehaviour.BeDestroyed();
            }            
            castleBehaviour.ChangePosition(spawnPoints[spawnPointsIndex[i]].position);
        }
    }

    void ChangeBuildCastles(int _amount)
    {
        buildedCastles += _amount;
        //if(buildedCastles >= 6) // for test
        if(buildedCastles >= totalCastles)
        {
            if(onGameOver != null)
            {
                onGameOver(EnumManager.Characters.Mage);
            }            
        }
        if(buildedCastles <= 0)
        {
            if(onGameOver != null)
            {
                onGameOver(EnumManager.Characters.Dragon);
            }            
        }       
        //Debug.Log("buildedCastles= " + buildedCastles);
    }

    void CheckResults()
    {
        if(buildedCastles > totalCastles / 2)
        {
            if(onGameOver != null)
            {
                onGameOver(EnumManager.Characters.Mage);
            }
        }
        else if(buildedCastles < totalCastles / 2)
        {
            if(onGameOver != null)
            {
                onGameOver(EnumManager.Characters.Dragon);
            }
        }
        else if(buildedCastles == totalCastles / 2 && onSuddenDeath != null)
        {
            onSuddenDeath();
        }
    }

    internal void SuperTime(EnumManager.Characters _whoGotItem)
    {
        StartCoroutine(SuperTimeCR(_whoGotItem));
    }

    IEnumerator SuperTimeCR(EnumManager.Characters _whoGotItem)
    {
        if(_whoGotItem == EnumManager.Characters.Mage)
        {
            foreach(GameObject castle in castles)
            {
                CastleBehaviour castleBehaviourTemp = castle.GetComponent<CastleBehaviour>();
                if(castleBehaviourTemp.GetMyCurrentState() == EnumManager.CastleStates.Destroyed)
                {
                    castle.GetComponent<CastleBehaviour>().BeBuilded();
                    yield return new WaitForSeconds(waitBetweenApplaySuperTime); 
                }
            }
        }
        else
        {
            foreach(GameObject castle in castles)
            {
                CastleBehaviour castleBehaviourTemp = castle.GetComponent<CastleBehaviour>();
                if(castleBehaviourTemp.GetMyCurrentState() == EnumManager.CastleStates.Builded)
                {
                    castle.GetComponent<CastleBehaviour>().BeDestroyed();
                    yield return new WaitForSeconds(waitBetweenApplaySuperTime); 
                }
            }
        }        
    }

    #region Getters&Setters
    internal int GetBuildedCastles()
    {
        return buildedCastles;
    }
    
    internal Sprite GetBuilded()
    {
        return builded;
    }

    internal Sprite GetDestroyed()
    {
        return destroyed;
    }

    void SetSpawnPointsIndex(int[] _randomIndex)
    {
        spawnPointsIndex = _randomIndex;        
    }
    #endregion

    void OnEnable()
    {
    	CastleBehaviour.onChangeBuildedState += ChangeBuildCastles;
        GameplaySceneController.onTimeUp += CheckResults;
    }

    void OnDisable()
    {
	    CastleBehaviour.onChangeBuildedState -= ChangeBuildCastles;
        GameplaySceneController.onTimeUp -= CheckResults;
    }

}
