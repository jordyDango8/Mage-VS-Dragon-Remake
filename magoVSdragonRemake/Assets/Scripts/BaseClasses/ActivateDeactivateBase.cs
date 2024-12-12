using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateDeactivateBase : MonoBehaviour
{
    [SerializeField]
    protected GameObject objectToActivate;

    protected virtual void Activate()
    {
        SetActive(true);
    }

    protected void Deactivate()
    {
        SetActive(false);
    }

    void SetActive(bool _newState)
    {        
        //Debug.Log(gameObject.name + ".object to activate " + objectToActivate);
        objectToActivate.SetActive(_newState);
    }

}
