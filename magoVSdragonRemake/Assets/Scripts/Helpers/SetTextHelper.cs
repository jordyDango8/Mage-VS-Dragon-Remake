using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

//[RequireComponent(typeof(TextMeshProUGUI))]
public class SetTextHelper : MonoBehaviour
{
    [SerializeField]
    string spanishText;

    [SerializeField]
    string englishText;

    LanguageManager languageManager;    

    TextMeshProUGUI myTMPUGUI;
    TextMeshPro myTMP;

    void SetTextByLanguage()
    {
        switch(languageManager.GetCurrentLanguage())
        {
            case EnumManager.Languages.Spanish:
                //Debug.Log("spanish");
                ChangeText(spanishText);
                break;
            case EnumManager.Languages.English:
                //Debug.Log("english");
                ChangeText(englishText);
                break;
            default:
                Debug.Log("no language");
                break;
        }
    }

    void ChangeText(string _newValue)
    {
        if(myTMPUGUI != null)
        {
            myTMPUGUI.text = _newValue;
        }
        if(myTMP != null)
        {
            myTMP.text = _newValue;
        }
    }

    void OnEnable()
    {
        //AssignLanguageManager();
        languageManager = LanguageManager.languageManager; 

        if (TryGetComponent<TextMeshProUGUI>(out TextMeshProUGUI TMPUGUI))
        {
            myTMPUGUI = TMPUGUI;
        }              
        if (TryGetComponent<TextMeshPro>(out TextMeshPro TMP))
        {
            myTMP = TMP;
        }              
        //myTMP = GetComponent<TextMeshProUGUI>();
        SetTextByLanguage();

        LanguageManager.onLanguageChange += SetTextByLanguage;
    }

    void OnDisable()
    {
        LanguageManager.onLanguageChange -= SetTextByLanguage;
    }

    void AssignLanguageManager()
    {
        if(languageManager == null)
        {
            languageManager = LanguageManager.languageManager;  
        }
    }

}
