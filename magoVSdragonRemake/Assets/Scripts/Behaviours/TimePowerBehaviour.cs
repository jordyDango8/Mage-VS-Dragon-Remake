using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimePowerBehaviour : PowerBehaviour
{
     protected override void Start()
    {
        base.Start();
        myEffectDuration = 5;
        myPower = EnumManager.Powers.Time;
    }

    internal override void AssignWho(EnumManager.Characters _whoGot)
    {           
        //powersController.SetItemGotten(myItem);
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
        //Debug.Log("rewind apply effect");
        StartCoroutine(ApplyEffectCR(whoReceivesEffect));               
    }    

    IEnumerator ApplyEffectCR(CharacterBehaviour whoReceivesEffect)
    {                
        whoReceivesEffect.Rewind();            
        yield return new WaitForSeconds(myEffectDuration);                
     
        whoReceivesEffect.ResetRewind();
    }
}
