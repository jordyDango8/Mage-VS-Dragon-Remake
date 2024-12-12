using UnityEngine.Audio;
using System;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [SerializeField]
    Sound[] sounds;

    internal static AudioManager audioManager;

    float pitchChange = 0.3f;

    void Awake()
    {
        if (audioManager == null)
        {
            audioManager = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        DontDestroyOnLoad(gameObject);

        foreach (Sound sound in sounds)
        {
            sound.source = gameObject.AddComponent<AudioSource>();
            sound.source.clip = sound.clip;            
            sound.name = sound.clip.name;            

            sound.source.volume = sound.volume;
            sound.source.pitch = sound.pitch;
            sound.source.loop = sound.loop;
        }
    }    

    internal void Play (EnumManager.Sounds _name)
    {
        Sound sound = Search(_name);
        if (sound != null)
        {  
            sound.source.Play();
            //Debug.Log("play " + _name);
        }
    }

    internal void Stop(EnumManager.Sounds _name)
    {
        Sound sound = Search(_name);
        if (sound != null)
        {  
            sound.source.Stop();
        }
    }    

    internal void Faster(EnumManager.Sounds _name)
    {
        Sound sound = Search(_name);
        if (sound != null)
        {  
            sound.source.pitch = sound.pitch + pitchChange;
        }
    }

    internal void Slow(EnumManager.Sounds _name)
    {       
        Sound sound = Search(_name);
        if (sound != null)
        {            
            sound.source.pitch = sound.pitch = 1f;
        }
    }

    Sound Search(EnumManager.Sounds _name)
    {
        Sound sound = Array.Find(sounds, sound => sound.name.Equals(_name.ToString()));
        if (sound == null)
        {
            //Debug.LogWarning("sound: " + name + " not found");
            Debug.LogError("sound: " + name + " not found");
            return null;
        }
        else
        {
            return sound;
        }
    }
}
