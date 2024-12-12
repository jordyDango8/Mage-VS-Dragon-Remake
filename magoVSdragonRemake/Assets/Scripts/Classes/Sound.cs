using UnityEngine.Audio;
using UnityEngine;

[System.Serializable]
internal class Sound
{
    //[SerializeField]
    internal string name;
    
    [SerializeField]
    internal AudioClip clip;

    [SerializeField]
    [Range(0f, 1f)]
    internal float volume;
    
    [SerializeField]
    [Range(.1f, 3f)]
    internal float pitch;

    [SerializeField]
    internal bool loop;

    [HideInInspector]
    internal AudioSource source;
}
