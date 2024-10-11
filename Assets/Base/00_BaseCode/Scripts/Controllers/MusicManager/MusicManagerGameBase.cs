using UnityEngine;
using UnityEngine.Audio;
using DG.Tweening;
using UnityEngine.Events;
using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine.SceneManagement;
public enum NameMusic
{
    None = 0,
    Fly = 1,
    FlyOut = 2,
    Idle = 3,
    Beep = 4
}
public class MusicData
{
    public NameMusic name;
    public float spaceDelayTime;
    [HideInInspector] public float timmer;
    [HideInInspector] public bool isActiving;
    public AudioClip audioClip;
}

//  ----------------------------------------------
//  Author:     CuongCT <caothecuong91@gmail.com> 
//  Copyright (c) 2016 OneSoft JSC
// ----------------------------------------------
public class MusicManagerGameBase : SerializedMonoBehaviour
{
    public Dictionary<NameMusic, MusicData> clips;

    public enum SourceAudio { Music, Effect, UI };
    public AudioMixer mixer;
    public AudioSource effectSource;
    public AudioSource musicSource;
    public AudioSource soundUISource;
    public AudioSource conveyorSource;
    public float lowPitchRange = 0.95f;              //The lowest a sound effect will be randomly pitched.
    public float highPitchRange = 1.05f;            //The highest a sound effect will be randomly pitched.
    public float delayTime = 0.5f;
    public const string MASTER_KEY = "MASTER_KEY";
    public const string MUSIC_KEY = "MUSIC_KEY";
    public const string SOUND_KEY = "SOUND_KEY";
    public const float MIN_VALUE = -80f;
    public const float MAX_VALUE = 0f;

    [Space]
    [Header("-----MUSIC-----")]
    [SerializeField] private AudioClip winMusic;
    [SerializeField] private AudioClip BGMusic;
    [SerializeField] private AudioClip BGMusicGamePlay;

    [Space]
    [Header("-----SOUND EFFECTS-----")]
    [SerializeField] private AudioClip clickSound;
    [SerializeField] private AudioClip makeAPurchase;
    [SerializeField] private AudioClip purchaseSuccessful;
    [SerializeField] private AudioClip pickingCake;
    [SerializeField] private AudioClip orderUp;
    [SerializeField] private AudioClip orderFinished;
    [SerializeField] private AudioClip conveyor;
    [SerializeField] private AudioClip victory;
    [SerializeField] private AudioClip defeat;
    [SerializeField] private AudioClip booster_1;
    [SerializeField] private AudioClip booster_2;
    [SerializeField] private AudioClip booster_3;

    [Space]
    [Header("-----MUSIC LOOP CUE-----")]
    [SerializeField] private float loopTimingStart;
    [SerializeField] private float loopTimingEnd;

    Coroutine loopChecker;
    private AudioClip _currentMusic;

    public float MasterVolume
    {
        get
        {
            return PlayerPrefs.GetFloat(MASTER_KEY, 0f);
        }
        set
        {
            SetMasterVolume(value);
        }
    }
    public float MusicVolume
    {
        get
        {
            return GameController.Instance.useProfile.OnMusic ? 1 : 0;
        }
    }
    public float SoundVolume
    {
        get
        {
            return GameController.Instance.useProfile.OnSound ? 1 : 0;
        }
    }

    public void Init()
    {
        musicSource.volume = GameController.Instance.useProfile.OnMusic ? 0.7f : 0;
        effectSource.volume = GameController.Instance.useProfile.OnSound ? 1 : 0;
        //MusicTransition();
        //loopChecker = StartCoroutine(CheckMusicForLooping());
    }


    //Used to play single sound clips.
    public void PlaySingle(AudioClip clip, SourceAudio source = SourceAudio.Effect)
    {
        if (clip == null)
            return;
        switch (source)
        {
            case SourceAudio.Music:
                if (MusicVolume == 0) return;
                musicSource.clip = clip;
                musicSource.Play();
                break;
            case SourceAudio.Effect:
                if (SoundVolume == 0) return;

                effectSource.clip = clip;
                effectSource.Play();
                break;
            case SourceAudio.UI:
                if (SoundVolume == 0) return;
                soundUISource.clip = clip;
                soundUISource.Play();
                break;
        }

    }
    public void PlaySound(AudioClip paramWin)
    {
        if (!GameController.Instance.useProfile.OnMusic)
            return;
        // musicSource.clip = winMusic;
        //musicSource.Play();
        PlaySingle(paramWin, SourceAudio.Effect);
    }
    public void PauseBGMusic()
    {
        musicSource.Pause();
    }

    public void ResumeBGMusic()
    {
        musicSource.UnPause();
    }

    //Used to play single sound clips.
    public void PlayOneShot(AudioClip clip, SourceAudio source = SourceAudio.Effect)
    {
        if (clip == null)
            return;
        switch (source)
        {
            case SourceAudio.Music:
                if (MusicVolume == 0) return;
                musicSource.PlayOneShot(clip);
                break;
            case SourceAudio.Effect:
                if (SoundVolume == 0) return;
                effectSource.PlayOneShot(clip);
                break;
        }
    }

    //RandomizeSfx chooses randomly between various audio clips and slightly changes their pitch.
    public void RandomizeSfx(params AudioClip[] clips)
    {
        //Generate a random number between 0 and the length of our array of clips passed in.
        int randomIndex = Random.Range(0, clips.Length);

        //Choose a random pitch to play back our clip at between our high and low pitch ranges.
        float randomPitch = Random.Range(lowPitchRange, highPitchRange);

        //Set the pitch of the audio source to the randomly chosen pitch.
        effectSource.pitch = randomPitch;

        //Set the clip to the clip at our randomly chosen index.
        effectSource.clip = clips[randomIndex];

        //Play the clip.
        effectSource.Play();
    }

    public void PlayMusic(AudioClip clip)
    {
        if (clip == null || _currentMusic == clip)
            return;
        SetMasterVolume(MasterVolume);
        SetMusicVolume(MusicVolume);
        SetSoundVolume(SoundVolume);
        _currentMusic = clip;
        StopMusic();
        musicSource.clip = clip;
        musicSource.PlayDelayed(delayTime);
    }
    public void RandomizeMusic(params AudioClip[] clips)
    {
        //Generate a random number between 0 and the length of our array of clips passed in.
        int randomIndex = Random.Range(0, clips.Length);
        var clip = clips[randomIndex];
        if (clip == null || _currentMusic == clip)
            return;
        _currentMusic = clip;
        StopMusic();
        //Set the clip to the clip at our randomly chosen index.
        musicSource.clip = clips[randomIndex];

        //Play the clip.
        musicSource.PlayDelayed(delayTime);
    }

    public void PauseMusic()
    {
        //Play the clip.
        musicSource.Pause();
    }
    public void UnPauseMusic()
    {
        //Play the clip.
        musicSource.UnPause();
    }

    public void StopMusic()
    {
        musicSource.Stop();
    }
    public bool IsMusicPlaying()
    {
        return musicSource.isPlaying;
    }


    private void SetMasterVolume(float volume)
    {
        // mixer.SetFloat("MasterVolume", volume);
        // PlayerPrefs.SetFloat(MASTER_KEY, volume);
    }
    public void SetMusicVolume(float volume)
    {
        musicSource.volume = volume;
        if(volume <= 0)
        {
            musicSource.time = 0;
        }

        loopChecker = StartCoroutine(CheckMusicForLooping());
        //PlayerPrefs.SetFloat(MUSIC_KEY, volume);
    }
    public void SetSoundVolume(float volume)
    {
        effectSource.volume = volume;
        // PlayerPrefs.SetFloat(SOUND_KEY, volume);
    }

    #region === Play Sound ===
    public void PlayWinSound()
    {
        if (!GameController.Instance.useProfile.OnMusic)
            return;
        // musicSource.clip = winMusic;
        //musicSource.Play();
        PlaySingle(winMusic, SourceAudio.Effect);
    }

    public void PlayBGMusic()
    {
        //if (SceneManager.GetActiveScene().name == "GamePlay")
        //{
        //    musicSource.clip = BGMusicGamePlay;
        //}
        //else
        //{
        //    musicSource.clip = BGMusic;
        //}
        
        musicSource.Play();
    }

    IEnumerator CheckMusicForLooping()
    {
        while (musicSource.volume > 0)
        {
            MusicLoop();
            //yield return new WaitForSeconds(0.001f);
            yield return null;
        }
    }

    public void MusicLoop()
    {
        if (musicSource.clip != null && musicSource.isPlaying)
        {
            if (musicSource.time >= loopTimingEnd)
            {
                musicSource.time = loopTimingStart;
            }
        }
    }

    public void MusicTransition()
    {
        if (CheckCurrentMusic())
        {
            if (loopChecker != null)
            {
                StopCoroutine(loopChecker);
            }

            musicSource.DOFade(0, 0.25f).SetEase(Ease.Linear)
                        .OnComplete(delegate
                        {
                            SwapMusic();
                            musicSource.DOFade(0.7f, 0.1f).SetEase(Ease.Linear)
                                .OnComplete(delegate
                                {
                                    musicSource.Play();
                                    loopChecker = StartCoroutine(CheckMusicForLooping());
                                });
                        });
        }
        

        bool CheckCurrentMusic()
        {
            if (SceneManager.GetActiveScene().name == "HomeScene")
            {
                if (musicSource.clip != BGMusic)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }

            if (SceneManager.GetActiveScene().name == "GamePlay")
            {
                if (musicSource.clip != BGMusicGamePlay)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }

            return false;
        }

        void SwapMusic(bool autoPlay = false)
        {
            float currentTime = 0;

            if (musicSource.clip != null)
            {
                if (musicSource.time > 0)
                {
                    currentTime = musicSource.time;
                }
            }

            if (SceneManager.GetActiveScene().name == "GamePlay")
            {
                musicSource.clip = BGMusicGamePlay;
                musicSource.time = currentTime;
            }
            else
            {
                musicSource.clip = BGMusic;
                musicSource.time = currentTime;
            }

            if (autoPlay)
            {
                musicSource.Play();
            }
        }
    }

    public void PlaySound(bool cakePicking = false, bool orderFinished = false, bool victory = false, bool defeat = false, bool booster_1 = false, bool booster_2 = false, bool booster_3 = false, bool makeAPurchase = false, bool purchaseSuccessful = false, bool orderUp = false)
    {
        if (SoundVolume == 0) return;

        if (cakePicking)
        {
            effectSource.PlayOneShot(pickingCake);
            //effectSource.Play();
            return;
        }

        if (orderFinished)
        {
            effectSource.PlayOneShot(this.orderFinished);
            //effectSource.Play();
            return;
        }

        if (victory)
        {
            effectSource.PlayOneShot(this.victory);
            //effectSource.Play();
            return;
        }

        if (defeat)
        {
            effectSource.PlayOneShot(this.defeat);
            //effectSource.Play();
            return;
        }

        if (booster_1)
        {
            effectSource.PlayOneShot(this.booster_1);
            //effectSource.Play();
            return;
        }

        if (booster_2)
        {
            effectSource.PlayOneShot(this.booster_2);
            //effectSource.Play();
            return;
        }

        if (booster_3)
        {
            effectSource.PlayOneShot(this.booster_3);
            //effectSource.Play();
            return;
        }

        if (makeAPurchase)
        {
            effectSource.PlayOneShot(this.makeAPurchase);
            //effectSource.Play();
            return;
        }

        if (purchaseSuccessful)
        {
            effectSource.PlayOneShot(this.purchaseSuccessful);
            //effectSource.Play();
            return;
        }

        if (orderUp)
        {
            effectSource.PlayOneShot(this.orderUp);
            //effectSource.Play();
            return;
        }
    }

    public void PlayClickSound()
    {
        //PlaySingle(clickSound, SourceAudio.UI);
        if (SoundVolume == 0) return;

        if (soundUISource.clip != clickSound)
        {
            soundUISource.clip = clickSound;
        }

        soundUISource.Play();
    }

    public void PlayConveyorSound()
    {
        if (SoundVolume == 0) return;

        if (conveyorSource.clip == null)
        {
            conveyorSource.clip = conveyor;
        }

        conveyorSource.time = 0;
        conveyorSource.Play();
    }

    public void StopConveyorSound()
    {
        if (conveyorSource.clip == null || SoundVolume == 0) return;

        conveyorSource.Stop();
    }
    #endregion

    public void PlayOneShot(NameMusic name)
    {
        if (SoundVolume == 0) return;
        if (clips.ContainsKey(name))
        {
            if (clips[name].timmer <= 0)
            {
                clips[name].timmer = clips[name].spaceDelayTime;
                clips[name].isActiving = true;
                effectSource.clip = clips[name].audioClip;
                effectSource.Play();
            }
        }
    }

    public void PlayOneShot(NameMusic name, AudioClip clip)
    {
        if (SoundVolume == 0) return;
        if (clips.ContainsKey(name))
        {
            if (clips[name].timmer <= 0)
            {
                clips[name].timmer = clips[name].spaceDelayTime;
                clips[name].isActiving = true;
                effectSource.clip = clip;
                effectSource.Play();
            }
        }
    }

    public void PlayOneShot(NameMusic name, AudioClip clip, AudioSource source)
    {
        if (SoundVolume == 0) return;
        if (clips.ContainsKey(name))
        {
            if (clips[name].timmer <= 0)
            {
                clips[name].timmer = clips[name].spaceDelayTime;
                clips[name].isActiving = true;
                source.volume = 1;
                source.clip = clip;
                source.Play();
            }
        }
    }

    public void PlayOneShot(AudioClip clip, AudioSource source, float volume = 1)
    {
        if (SoundVolume == 0) return;

        source.volume = volume;
        source.clip = clip;
        source.Play();
    }

    private void Update()
    {
        ClipDelayHandle();
    }

    private void ClipDelayHandle()
    {
        if (clips == null)
            return;

        foreach (var clip in clips)
        {
            if (clip.Value.isActiving)
            {
                clip.Value.timmer -= Time.deltaTime;
                if (clip.Value.timmer < 0)
                {
                    clip.Value.timmer = 0;
                    clip.Value.isActiving = false;
                }
            }
        }
    }
}
