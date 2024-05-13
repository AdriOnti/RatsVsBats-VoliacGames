using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Audios
{

}

public class SoundManager : MonoBehaviour
{
    private AudioSource soundEffectsManager;
    private AudioSource extraSoundsManager;
    private AudioSource musicManager;

    public static SoundManager Instance;
    public AudiosContainer audiosDatabase;

    private readonly Dictionary<Audios, AudioClip> soundsDatabase = new();

    // Start is called before the first frame update
    void Awake()
    {
        if (Instance != null && Instance != this) Destroy(Instance);
        else Instance = this;
        soundEffectsManager = GetComponent<AudioSource>();
        extraSoundsManager = transform.GetChild(0).GetComponentInChildren<AudioSource>();
        musicManager = transform.GetChild(1).GetComponentInChildren<AudioSource>();
    }

    private void Start()
    {
        bool sucess;
        foreach (AudioClip clip in audiosDatabase.soundsDB)
        {
            if (sucess = Enum.TryParse(clip.name, out Audios value))
                soundsDatabase.Add(value, clip);
        }
    }
    public void PlayEnvironment(Audios clip)
    {
        if (soundsDatabase.TryGetValue(clip, out AudioClip value))
        {
            try
            {
                soundEffectsManager.PlayOneShot(value);
            }
            catch (ArgumentNullException)
            {

                //NO IDEA
            }

        }

    }
    public void PlayEffect(Audios clip)
    {
        if (soundsDatabase.TryGetValue(clip, out AudioClip value))
        {
            //if (clip == Audios.AbilityStarMario) musicManager.mute = true;
            extraSoundsManager.PlayOneShot(value);
        }
    }
}
