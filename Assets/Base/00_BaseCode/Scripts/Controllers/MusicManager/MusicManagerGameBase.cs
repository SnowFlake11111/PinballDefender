using UnityEngine;
using UnityEngine.Audio;
using DG.Tweening;
using UnityEngine.Events;
using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine.SceneManagement;
using System.Linq;
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

    [BoxGroup("Home music", centerLabel: true)]
    [SerializeField] private Dictionary<int, AudioClip> home_musicListBeginning = new Dictionary<int, AudioClip>();
    [BoxGroup("Home music")]
    [SerializeField] private Dictionary<int, AudioClip> home_musicListLooping = new Dictionary<int, AudioClip>();

    [BoxGroup("Campaign music", centerLabel: true)]
    [SerializeField] private Dictionary<int, AudioClip> campaign_musicListBeginning = new Dictionary<int, AudioClip>();
    [BoxGroup("Campaign music")]
    [SerializeField] private Dictionary<int, AudioClip> campaign_musicListLooping = new Dictionary<int, AudioClip>();

    [BoxGroup("ScoreBattle music", centerLabel: true)]
    [SerializeField] private Dictionary<int, AudioClip> score_musicListBeginning = new Dictionary<int, AudioClip>();
    [BoxGroup("ScoreBattle music")]
    [SerializeField] private Dictionary<int, AudioClip> score_musicListLooping = new Dictionary<int, AudioClip>();

    [BoxGroup("DefenderBattle music", centerLabel: true)]
    [SerializeField] private Dictionary<int, AudioClip> defender_musicListBeginning = new Dictionary<int, AudioClip>();
    [BoxGroup("DefenderBattle music")]
    [SerializeField] private Dictionary<int, AudioClip> defender_musicListLooping = new Dictionary<int, AudioClip>();

    [BoxGroup("Sound Effects")]
    [SerializeField] private Dictionary<int, AudioClip> soundEffects = new Dictionary<int, AudioClip>();

    [Space]
    [Header("-----SOUND EFFECTS-----")]
    [SerializeField] private AudioClip clickSound;

    Coroutine loopTransition;
    Coroutine musicTransition;
    private AudioClip _currentMusic;
    int previousModeId = 0;
    int currentlyPlayingMusicId = 0;
    int soundEffectPlayedCount = 0;

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
        if (volume <= 0)
        {
            musicSource.Stop();
        }
        else
        {
            ChangeMusic(previousModeId, currentlyPlayingMusicId);
        }

        //loopChecker = StartCoroutine(CheckMusicForLooping());
        //PlayerPrefs.SetFloat(MUSIC_KEY, volume);
    }
    public void SetSoundVolume(float volume)
    {
        effectSource.volume = volume;
        // PlayerPrefs.SetFloat(SOUND_KEY, volume);
    }

    #region Custom Functions
    public void PlaySoundEffect(int id)
    {
        if (SoundVolume == 0) return;

        if (id == 2)
        {
            if (soundEffectPlayedCount < 10)
            {
                soundEffectPlayedCount++;
                StartCoroutine(HitEffectDonePlaying(soundEffects[2].length));
            }
            else
            {
                return;
            }
        }       

        effectSource.PlayOneShot(soundEffects[id]);
    }

    public void ChangeMusic(int modeId, int specifiedSongId = 0)
    {
        if (MusicVolume == 0)
        {
            previousModeId = modeId;
            currentlyPlayingMusicId = specifiedSongId;
            return;
        }

        if (musicTransition != null)
        {
            StopCoroutine(musicTransition);
        }

        int randomId = 0;

        switch (modeId)
        {
            case 0:
                //Home
                if (specifiedSongId > 0)
                {
                    if (modeId == previousModeId && currentlyPlayingMusicId == specifiedSongId)
                    {
                        return;
                    }

                    currentlyPlayingMusicId = specifiedSongId;
                    musicTransition = StartCoroutine(MusicTransition(modeId, specifiedSongId));
                }
                else
                {
                    List<int> availableCampaignSongs = new List<int>(home_musicListBeginning.Keys.ToList());
                    randomId = UnityEngine.Random.Range(0, availableCampaignSongs.Count);

                    if (modeId == previousModeId && currentlyPlayingMusicId == randomId)
                    {
                        return;
                    }

                    currentlyPlayingMusicId = specifiedSongId;
                    musicTransition = StartCoroutine(MusicTransition(modeId, randomId));
                }
                break;
            case 1:
                //Campaign
                if (specifiedSongId > 0)
                {
                    if (modeId == previousModeId && currentlyPlayingMusicId == specifiedSongId)
                    {
                        return;
                    }

                    currentlyPlayingMusicId = specifiedSongId;
                    musicTransition = StartCoroutine(MusicTransition(modeId, specifiedSongId));
                }
                else
                {
                    List<int> availableCampaignSongs = new List<int>(campaign_musicListBeginning.Keys.ToList());
                    randomId = UnityEngine.Random.Range(0, availableCampaignSongs.Count);

                    if (modeId == previousModeId && currentlyPlayingMusicId == randomId)
                    {
                        return;
                    }

                    currentlyPlayingMusicId = specifiedSongId;
                    musicTransition = StartCoroutine(MusicTransition(modeId, randomId));
                }
                break;
            case 2:
                //ScoreBattle
                if (specifiedSongId > 0)
                {
                    if (modeId == previousModeId && currentlyPlayingMusicId == specifiedSongId)
                    {
                        return;
                    }

                    currentlyPlayingMusicId = specifiedSongId;
                    musicTransition = StartCoroutine(MusicTransition(modeId, specifiedSongId));
                }
                else
                {
                    List<int> availableScoreSongs = new List<int>(score_musicListBeginning.Keys.ToList());
                    randomId = UnityEngine.Random.Range(0, availableScoreSongs.Count);

                    if (modeId == previousModeId && currentlyPlayingMusicId == availableScoreSongs[randomId])
                    {
                        return;
                    }

                    currentlyPlayingMusicId = specifiedSongId;
                    musicTransition = StartCoroutine(MusicTransition(modeId, availableScoreSongs[randomId]));
                }
                break;
            case 3:
                //DefenderBattle
                if (specifiedSongId > 0)
                {
                    if (modeId == previousModeId && currentlyPlayingMusicId == specifiedSongId)
                    {
                        return;
                    }

                    currentlyPlayingMusicId = specifiedSongId;
                    musicTransition = StartCoroutine(MusicTransition(modeId, specifiedSongId));
                }
                else
                {
                    List<int> availableCampaignSongs = new List<int>(defender_musicListBeginning.Keys.ToList());
                    randomId = UnityEngine.Random.Range(0, availableCampaignSongs.Count);

                    if (modeId == previousModeId && currentlyPlayingMusicId == availableCampaignSongs[randomId])
                    {
                        return;
                    }

                    currentlyPlayingMusicId = specifiedSongId;
                    musicTransition = StartCoroutine(MusicTransition(modeId, availableCampaignSongs[randomId]));
                }
                break;
        }

        previousModeId = modeId;
    }

    IEnumerator MusicTransition(int modeId, int songId)
    {
        if (musicSource.isPlaying)
        {
            if (loopTransition != null)
            {
                StopCoroutine(loopTransition);
            }
            var temp = musicSource.DOFade(0, 0.25f).SetEase(Ease.Linear).WaitForCompletion();
            yield return temp;
        }

        switch (modeId)
        {
            case 0:
                musicSource.clip = home_musicListBeginning[songId];
                musicSource.DOFade(0.7f, 0.1f).SetEase(Ease.Linear).OnStart(delegate { musicSource.Play(); });

                if (home_musicListLooping.ContainsKey(songId))
                {
                    loopTransition = StartCoroutine(SetupAutoLoop(musicSource.clip.length, modeId, songId));
                }
                break;
            case 1:
                musicSource.clip = campaign_musicListBeginning[songId];
                musicSource.DOFade(0.7f, 0.1f).SetEase(Ease.Linear).OnStart(delegate { musicSource.Play(); });

                if (campaign_musicListLooping.ContainsKey(songId))
                {
                    loopTransition = StartCoroutine(SetupAutoLoop(musicSource.clip.length, modeId, songId));
                }
                break;
            case 2:
                musicSource.clip = score_musicListBeginning[songId];
                musicSource.DOFade(0.7f, 0.1f).SetEase(Ease.Linear).OnStart(delegate { musicSource.Play(); });

                if (score_musicListLooping.ContainsKey(songId))
                {
                    loopTransition = StartCoroutine(SetupAutoLoop(musicSource.clip.length, modeId, songId));
                }
                break;
            case 3:
                musicSource.clip = defender_musicListBeginning[songId];
                musicSource.DOFade(0.7f, 0.1f).SetEase(Ease.Linear).OnStart(delegate { musicSource.Play(); });

                if (defender_musicListLooping.ContainsKey(songId))
                {
                    loopTransition = StartCoroutine(SetupAutoLoop(musicSource.clip.length, modeId, songId));
                }
                break;
        }
    }

    IEnumerator SetupAutoLoop(float waitTime, int modeId, int songId)
    {
        yield return new WaitForSeconds(waitTime);
        switch (modeId)
        {
            case 0:
                //Home
                musicSource.clip = home_musicListLooping[songId];
                musicSource.Play();
                break;
            case 1:
                //Campaign
                musicSource.clip = campaign_musicListLooping[songId];
                musicSource.Play();
                break;
            case 2:
                //ScoreBattle
                musicSource.clip = score_musicListLooping[songId];
                musicSource.Play();
                break;
            case 3:
                //DefenderBattle
                musicSource.clip = defender_musicListLooping[songId];
                musicSource.Play();
                break;
        }

        loopTransition = null;
    }

    IEnumerator HitEffectDonePlaying(float time)
    {
        yield return new WaitForSeconds(time);
        soundEffectPlayedCount--;
    }
    #endregion

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
