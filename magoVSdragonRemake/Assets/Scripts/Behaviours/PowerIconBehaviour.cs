using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerIconBehaviour : MonoBehaviour
{
    [SerializeField]
    EnumManager.Powers myPower;

    SpriteRenderer mySpriteRenderer;

    void Start()
    {
        mySpriteRenderer = GetComponent<SpriteRenderer>();
    }
    
    internal void UpdateSprite(Sprite _enabledSprite, float _duration, Sprite _disabledSprite)
    {   
        StartCoroutine(EnabledCR(_enabledSprite, _duration, _disabledSprite));        
    }

    IEnumerator EnabledCR(Sprite _enabledSprite, float _duration, Sprite _disabledSprite)
    {
        mySpriteRenderer.sprite = _enabledSprite;
        yield return new WaitForSeconds(_duration);
        
        mySpriteRenderer.sprite = _disabledSprite;    
    } 

    internal EnumManager.Powers GetMyPower()
    {
        return myPower;
    }

    
}
