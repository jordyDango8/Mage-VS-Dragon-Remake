using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangePowerBehaviour : PowerBehaviour
{       
    protected override void Start()
    {
        base.Start();
        myEffectDuration = 15;
        myPower = EnumManager.Powers.Change;
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
        //Debug.Log("changeControlsApplyEffectTo");
        StartCoroutine(ApplyEffectCR(whoReceivesEffect));    
    }    

    IEnumerator ApplyEffectCR(CharacterBehaviour whoReceivesEffect)
    {                
        whoReceivesEffect.ChangeControls();            
        yield return new WaitForSeconds(myEffectDuration);                
     
        whoReceivesEffect.ResetControls();
    }

}
