using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.Design;
using UnityEngine;

public class PowersIconController : MonoBehaviour
{
    [SerializeField]
    PowerIconBehaviour[] magePowerIcons;
    
    [SerializeField]
    PowerIconBehaviour[] dragonPowerIcons;    

    [SerializeField]
    Sprite changeEnabledSprite;
    
    [SerializeField]
    Sprite speedDownEnabledSprite;
    
    [SerializeField]
    Sprite speedUpEnabledSprite;
    
    [SerializeField]
    Sprite superTimeEnabledSprite;
    
    [SerializeField]
    Sprite timeEnabledSprite; 

    [SerializeField]
    Sprite changeDisabledSprite;
    
    [SerializeField]
    Sprite speedDownDisabledSprite;
    
    [SerializeField]
    Sprite speedUpDisabledSprite;
    
    [SerializeField]
    Sprite timeDisabledSprite; 
    
    PowerIconBehaviour iconToUpdate;

    //void CheckWhichActivate(EnumManager.Characters _character, EnumManager.Powers _power, float _duration)
    void CheckWhichActivate(EnumManager.Characters _character, PowerBehaviour _powerBehaviour)
    {        
        EnumManager.Powers power = _powerBehaviour.GetMyPower();
        float duration = _powerBehaviour.GetMyEffectDuration();

        if(_character == EnumManager.Characters.Mage)        
        {     
            //FindWhichIcon(_power, magePowerIcons);                   
            FindWhichIcon(power, magePowerIcons);            
        }
        else
        {
            //FindWhichIcon(_power, dragonPowerIcons);            
            FindWhichIcon(power, dragonPowerIcons);            
        }
            //switch(_power)
            switch(power)
            {
                case EnumManager.Powers.Change:
                    //iconToUpdate.UpdateSprite(changeEnabledSprite, _duration, changeDisabledSprite);
                    iconToUpdate.UpdateSprite(changeEnabledSprite, duration, changeDisabledSprite);
                    break;
                case EnumManager.Powers.SpeedDown:
                    //iconToUpdate.UpdateSprite(speedDownEnabledSprite, _duration, speedDownDisabledSprite);
                    iconToUpdate.UpdateSprite(speedDownEnabledSprite, duration, speedDownDisabledSprite);
                    break;
                case EnumManager.Powers.SpeedUp:                    
                    //iconToUpdate.UpdateSprite(speedUpEnabledSprite, _duration, speedUpDisabledSprite);                    
                    iconToUpdate.UpdateSprite(speedUpEnabledSprite, duration, speedUpDisabledSprite);                    
                    break;
                case EnumManager.Powers.SuperTime:
                    iconToUpdate.UpdateSprite(superTimeEnabledSprite, duration, timeDisabledSprite);                    
                    break;
                case EnumManager.Powers.Time:
                    //iconToUpdate.UpdateSprite(timeEnabledSprite, _duration, timeDisabledSprite);
                    if(power == EnumManager.Powers.Time)
                    {
                        iconToUpdate.UpdateSprite(timeEnabledSprite, duration, timeDisabledSprite);
                    }                                   
                    break;
                default:
                    //Debug.Log(_power + " not found");
                    Debug.Log(power + " not found");
                    break;
            }                
    }    

    void FindWhichIcon(EnumManager.Powers _power, PowerIconBehaviour[] _array)
    {
        foreach(PowerIconBehaviour icon in _array)
        {
            //Debug.Log("icon.GetMyPower()= " + icon.GetMyPower());
            //Debug.Log("_power()= " + _power);
            if(_power == EnumManager.Powers.SuperTime)
            {
                iconToUpdate = _array[0];
                return;
            }
            if(icon.GetMyPower() == _power)
            {                
                iconToUpdate = icon;
                //Debug.Log("iconToUpdate= " + icon);
                break;
            }
        }
    }

    void OnEnable()
    {
        PowerBehaviour.onPowerGotten += CheckWhichActivate;
    }

    void OnDisable()
    {
        PowerBehaviour.onPowerGotten -= CheckWhichActivate;
    }
}
