using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using JordiArray;

public class PlayersController : MonoBehaviour
{
    internal static PlayersController playersController;

    [SerializeField]
    GameObject[] characters;

    [SerializeField]
    Transform[] spawnPoints;    

    MageBehaviour mage;
    DragonBehaviour dragon;
    
    int[] randomArray = new int[2];

    int[] spawnPointsIndex = new int[2];

    void Awake()
    {        
        playersController = this;
        //Debug.Log("playersController.playersController= " + playersController);

        randomArray = ArrayLibrary.GenerateIrrepetibleArray(randomArray, 0, spawnPoints.Length);
        SetSpawnPointsIndex(randomArray);
        SpawnPlayers();
    }

    void SpawnPlayers()
    {
        for(int i = 0; i < characters.Length; i++)
        {
            Instantiate(characters[i], spawnPoints[spawnPointsIndex[i]].position, Quaternion.identity, transform);
        }

        mage = FindObjectOfType<MageBehaviour>();
        //Debug.Log("playersController.mage= " + mage);
        dragon = FindObjectOfType<DragonBehaviour>();
        //Debug.Log("playersController.dragon= " + dragon);        
    }

    #region Getters&Setters
    internal MageBehaviour GetMage()
    {
        return mage;
    }

    internal DragonBehaviour GetDragon()
    {
        return dragon;
    } 

    void SetSpawnPointsIndex(int[] _randomIndex)
    {
        spawnPointsIndex = _randomIndex;        
    }
    #endregion
    
}
