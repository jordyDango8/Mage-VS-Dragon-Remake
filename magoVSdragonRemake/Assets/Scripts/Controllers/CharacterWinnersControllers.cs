using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterWinnersControllers : MonoBehaviour
{
    GameManager gameManager;

    [SerializeField]
    GameObject mageWinner;

    [SerializeField]
    GameObject dragonWinner;

    void Start()
    {
        gameManager = GameManager.gameManager;
        //Debug.Log("winner= " + gameManager.GetWinner());
        if(gameManager.GetWinner() == EnumManager.Characters.Mage)
        {
            mageWinner.SetActive(true);
        }
        else
        {
            dragonWinner.SetActive(true);
        }
    }
   
}
