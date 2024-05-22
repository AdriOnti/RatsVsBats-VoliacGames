using System;
using System.Collections.Generic;
using UnityEngine;

public enum Audios
{
    ambientLava,
    effectDoorOpen,
    effectKeyGet,
    effectMonsterAttack,
    effectMonsterIdle,
    effectMouseSounds,
    effectPlayerDie,
    effectPlayerHurt,
    effectPlayerRun,
    effectPlayerSteps
}

public class SoundManager : MonoBehaviour
{
    private AudioSource soundEffectsManager;
    private AudioSource ambientSoundsManager;
    private AudioSource musicManager;
    private AudioSource playerSoundsManager;

    public static SoundManager Instance;
    public AudiosContainer audiosDatabase;

    private readonly Dictionary<Audios, AudioClip> soundsDatabase = new();

    // Start is called before the first frame update
    void Awake()
    {
        if (Instance != null && Instance != this) Destroy(Instance);
        else Instance = this;
        soundEffectsManager = GetComponent<AudioSource>();
        ambientSoundsManager = transform.GetChild(0).GetComponent<AudioSource>();
        musicManager = transform.GetChild(1).GetComponent<AudioSource>();
        playerSoundsManager = transform.GetChild(2).GetComponent<AudioSource>();
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
    public void PlayEffect(Audios clip)
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

    public void StopAmbient()
    {
        ambientSoundsManager.Stop();
    }

    public void StopPlayer()
    {
        playerSoundsManager.Stop();
    }

    public void PlayAmbient(Audios clip)
    {
        if (soundsDatabase.TryGetValue(clip, out AudioClip value))
        {
            try
            {
                ambientSoundsManager.PlayOneShot(value);
            }
            catch (ArgumentNullException)
            {

                //NO IDEA
            }
        }
    }

    public void PlayMusic(Audios clip)
    {
        if (soundsDatabase.TryGetValue(clip, out AudioClip value))
        {
            try
            {
                musicManager.PlayOneShot(value);
            }
            catch (ArgumentNullException)
            {

                //NO IDEA
            }
        }
    }

    public void PlayPlayer(Audios clip)
    {
        if (soundsDatabase.TryGetValue(clip, out AudioClip value))
        {
            try
            {
                playerSoundsManager.PlayOneShot(value);
            }
            catch (ArgumentNullException)
            {

                //NO IDEA
            }
        }
    }
}
