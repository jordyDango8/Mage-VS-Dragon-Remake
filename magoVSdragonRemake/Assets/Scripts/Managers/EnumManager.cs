using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnumManager : MonoBehaviour
{
    internal enum GameStates
    {
        None = 0,
        Normal,
        HurryUp,
        SuddenDeath,
    }

    internal enum Tags
    {
        None = 0,
        Dragon,
        Mage,
    }

    internal enum Sounds
    {
        None = 0,
        Destruction,
        Build,
        Game,
        MainTheme,
        Pause,
        Power,
        Win,
    }

    internal enum Languages
    {
        None = 0,
        Spanish,
        English,
    }

    internal enum Scenes
    {
        None = 0,
        MainScreen,
        MainMenu,
        LevelsMenu,
        Level1,
        Level2,
        Level3,
        Win,
    }

    internal enum Powers
    {
        None = 0,
        Change,
        SpeedUp,
        SpeedDown,
        Time,
        SuperTime,
    }

    internal enum Characters
    {
        None = 0,
        Mage,
        Dragon,
    }

    internal enum CastleStates
    {
        None = 0,
        Builded,
        Destroyed,
    }

    internal enum AnimParameters
    {
        None = 0,
        FadeIn,
        FadeOut,
    }

    internal enum animClips
    {
        None = 0,
        fadeIn,
        fadeOut,
    }
}
