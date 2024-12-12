using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LanguageManager : MonoBehaviour
{
    internal delegate void OnLanguageChange();
    internal static event OnLanguageChange onLanguageChange;

    internal static LanguageManager languageManager;    

    EnumManager.Languages currentLanguage;    
    
    void Awake()
    {
        if(languageManager == null)
        {
            languageManager = this;
        }
        else
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);
        
        // load language
        //SetCurrentLanguage(EnumManager.Languages.Spanish); // for test
        SetCurrentLanguage(EnumManager.Languages.English); // for test
    }

    internal EnumManager.Languages GetCurrentLanguage()
    {
        return currentLanguage;
    }

    internal void SetCurrentLanguage(EnumManager.Languages _newValue)
    {
        currentLanguage = _newValue;
        if(onLanguageChange != null)
        {
            onLanguageChange();
        }
    }

}
