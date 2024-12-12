using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HurryUpHelper : ActivateDeactivateBase
{    
    [SerializeField]
    //Aimator animator;
    AnimationClip animationClip;

    float clipLength;

    protected override void Activate()
    {
        base.Activate();
        StartCoroutine(DeactivateCR());
        clipLength = animationClip.length;
        //Debug.Log("clip length= " + clipLength);
    }

    IEnumerator DeactivateCR()
    {
        yield return new WaitForSeconds(clipLength);
        Deactivate();
        //Debug.Log("deactivate hurry up");
    }

    void OnEnable()
    {
        GameplaySceneController.onHurryUp += Activate;
    }
    
    void OnDisable()
    {
        GameplaySceneController.onHurryUp -= Activate;
    }
}
