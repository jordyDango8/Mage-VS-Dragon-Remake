using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LanguageButtonBehaviour : MonoBehaviour
{
    [SerializeField]
    EnumManager.Languages myLanguage;

    LanguageManager languageManager;

    //void Awake()
    void Start()
    {
        languageManager = LanguageManager.languageManager;
    }

    public void ChangeLanguage()
    {
        languageManager.SetCurrentLanguage(myLanguage);
    }


}
