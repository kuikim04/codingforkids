using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager instance;

    [Header("Dubbing voice")]

    public AudioClip D1;
    public AudioClip D2;
    public AudioClip D3;
    public AudioClip D4;
    public AudioClip D5;
    public AudioClip D6;
    public AudioClip D7;
    public AudioClip D8;
    public AudioClip D9;
    public AudioClip D10;
    public AudioClip D11;
    public AudioClip D12;


    [Header("SFX")]
    public AudioClip clickSound;
    public AudioClip winSound;
    public AudioClip wrongSound;
    public AudioClip dragSound;
    public AudioClip placeSound;

    [Header("Music")]
    public AudioClip menuMusic;
    public AudioClip gameplayMusic;

    private AudioSource voiceSource;
    private AudioSource sfxSource;
    private AudioSource musicSource;

    [Range(0f, 1f)]
    public float musicVolume = 0.3f;

    private Tween bgmFadeTween; 

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
            return;
        }
        instance = this;
        DontDestroyOnLoad(gameObject);

        sfxSource = gameObject.AddComponent<AudioSource>();

        musicSource = gameObject.AddComponent<AudioSource>();
        musicSource.loop = true;
        musicSource.volume = musicVolume;

        voiceSource = gameObject.AddComponent<AudioSource>();
        voiceSource.playOnAwake = false;
        voiceSource.loop = false;



    }

    public void PlayClickSound()
    {
        sfxSource.PlayOneShot(clickSound);
    }
    public void PlayDragSound()
    {
        sfxSource.PlayOneShot(dragSound);
    }
    public void PlayPlacedSound()
    {
        sfxSource.PlayOneShot(placeSound);
    }
    public void PlayWrongSound()
    {
        sfxSource.PlayOneShot(wrongSound);
    }

    public void PlayWinSound()
    {
        sfxSource.PlayOneShot(winSound);
    }

    public void PlayMenuMusic()
    {
        if (musicSource.clip == menuMusic) return;
        musicSource.clip = menuMusic;
        musicSource.Play();
    }

    public void PlayGameplayMusic()
    {
        if (musicSource.clip == gameplayMusic) return;
        musicSource.clip = gameplayMusic;
        musicSource.Play();
    }

    public void StopMusic()
    {
        musicSource.Stop();
    }

    public void SetMusicVolume(float volume)
    {
        musicVolume = Mathf.Clamp01(volume);
        musicSource.volume = musicVolume;
    }
    public void PlayVoiceOver(AudioClip voiceClip)
    {
        StartCoroutine(PlayVoiceRoutine(voiceClip));
    }

    public void FadeMusicVolume(float targetVolume, float duration = 0.5f)
    {
        if (bgmFadeTween != null && bgmFadeTween.IsActive())
            bgmFadeTween.Kill();

        bgmFadeTween = musicSource.DOFade(targetVolume, duration);
    }
    private IEnumerator PlayVoiceRoutine(AudioClip clip)
    {
        if (clip == null) yield break;

        float originalVolume = musicVolume;

        FadeMusicVolume(0.001f, 0.5f);

        voiceSource.volume = 2f; 
        voiceSource.clip = clip;
        voiceSource.Play();

        yield return new WaitForSeconds(clip.length);

        FadeMusicVolume(originalVolume, 0.5f);
    }

    public void PlayVoiceForScene(int sceneIndex)
    {
        AudioClip clipToPlay = null;

        switch (sceneIndex)
        {
            case 1: clipToPlay = D1; break;
            case 2: clipToPlay = D2; break;
            case 3: clipToPlay = D3; break;
            case 4: clipToPlay = D4; break;
            case 5: clipToPlay = D5; break;
            case 6: clipToPlay = D6; break;
            case 7: clipToPlay = D7; break;
            case 8: clipToPlay = D8; break;
            case 9: clipToPlay = D9; break;
            case 10: clipToPlay = D10; break;
            case 11: clipToPlay = D11; break;
            case 12: clipToPlay = D12; break;
        }

        if (clipToPlay != null)
        {
            PlayVoiceOver(clipToPlay);
        }
    }
}
