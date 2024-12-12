using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedDownPowerBehaviour : PowerBehaviour
{    
    float speedDownFactor = 2; 

    protected override void Start()
    {
        base.Start();
        myEffectDuration = 10;
        myPower = EnumManager.Powers.SpeedDown;
    }

    internal override void AssignWho(EnumManager.Characters _whoGot)
    {           
        //powersController.SetItemGotten(myPower);
        whoGotMe = _whoGot;
        if(_whoGot == EnumManager.Characters.Mage)
        {
            whoReceivesEffect = dragonBehaviour;        
        }
        else
        {
            whoReceivesEffect = mageBehaviour;
        }
    }

    protected override void ApplyEffectTo()
    {
        //Debug.Log("speedDownApplyEffectTo");
        StartCoroutine(ApplyEffectCR(whoReceivesEffect));               
    } 

    IEnumerator ApplyEffectCR(CharacterBehaviour whoReceivesEffect)
    {              
        //whoReceivesEffect.ChangeCurrentSpeed(-speedDownAmount);
        whoReceivesEffect.SetCurrentSpeed(whoReceivesEffect.GetCurrentSpeed() / speedDownFactor);
        yield return new WaitForSeconds(myEffectDuration);                
        
        whoReceivesEffect.ResetSpeed(); 
    }
   
}

