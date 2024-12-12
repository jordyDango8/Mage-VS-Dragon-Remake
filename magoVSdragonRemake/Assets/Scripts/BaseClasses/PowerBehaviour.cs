using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerBehaviour : MonoBehaviour
{    
    //internal delegate void OnPowerGotten(EnumManager.Characters _character, EnumManager.Powers _power, float _duration);    
    //internal static OnPowerGotten onPowerGotten;

    internal delegate void OnPowerGotten(EnumManager.Characters _character, PowerBehaviour _powerBehaviour);    
    internal static OnPowerGotten onPowerGotten;

    internal delegate void OnPowerGottenWP();
    internal static OnPowerGottenWP onPowerGottenWP; // WP without pa rameters   

    protected PowersController powersController;
    protected PlayersController playersController;
    protected CastlesController castlesController;

    protected float myEffectDuration;

    internal EnumManager.Characters whoGotMe;
    protected CharacterBehaviour whoReceivesEffect;

    internal EnumManager.Powers myPower;

    //protected MageBehaviour mageBehaviour;
    //protected DragonBehaviour dragonBehaviour;

    protected CharacterBehaviour mageBehaviour;
    protected CharacterBehaviour dragonBehaviour;

    SpriteRenderer mySpriteRenderer;
    Collider2D myCollider2D; 

    protected void Awake()
    {
        mySpriteRenderer = GetComponent<SpriteRenderer>();
        myCollider2D = GetComponent<Collider2D>();
    }

    protected virtual void Start()
    {        
        powersController = FindObjectOfType<PowersController>();
        //Debug.Log("strat " + gameObject.name);
        playersController = PlayersController.playersController;
        //Debug.Log("itemBehaviour.playersController= " + playersController);
        castlesController = FindObjectOfType<CastlesController>();
        mageBehaviour = playersController.GetMage();
        dragonBehaviour = playersController.GetDragon();        
    }

    void OnTriggerEnter2D(Collider2D _other)
    {        
        //Debug.Log("collided with " + _other.tag);
        if(_other.CompareTag(EnumManager.Characters.Mage.ToString()))
        {
            AssignWho(EnumManager.Characters.Mage);
        }
        else
        {            
            AssignWho(EnumManager.Characters.Dragon);
        }        
        ApplyEffectTo();                                 
        if(onPowerGotten != null)
        {
            //onPowerGotten(whoGotMe, myPower, myEffectDuration);            
            onPowerGotten(whoGotMe, this);            
            onPowerGottenWP();            
        }
        powersController.SetIsPower(false);
        EnableDisableMe(false);
    }

    internal void EnableDisableMe(bool _newState)
    {
        mySpriteRenderer.enabled = _newState;
        myCollider2D.enabled = _newState;
    }

    internal void SetPosition(Vector3 _newPosition)
    {
        transform.position = _newPosition;
    }

    internal virtual void AssignWho(EnumManager.Characters _whoGot)
    {   
        // Debug.Log("assign who);
    }
    
    protected virtual void ApplyEffectTo()
    {
        //Debug.Log("ApplyEffectTo " + _target);        
    }

    #region Getters&Setters
    internal EnumManager.Powers GetMyPower()
    {
        return myPower;
    }

    internal float GetMyEffectDuration()
    {
        return myEffectDuration;
    }
    #endregion  

}
