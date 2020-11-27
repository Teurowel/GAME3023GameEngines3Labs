using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

//00 - Overworld
//01 - Battle

//Responsible for playing music, modifying voluem, transitioning music
public class MusicManager : MonoBehaviour
{
    [SerializeField] AudioSource musicSource;

    [SerializeField] AudioClip[] trackList;

    [SerializeField] AudioMixer musicMixer;

    [SerializeField] float volumeMin_dB = -80.0f;
    [SerializeField] float volumeMax_dB = 0.0f;

    public enum Track
    {
        Overworld,
        Battle
    }

    // Start is called before the first frame update
    void Start()
    {
        BattleManager.instance.onEnterBattle += OnEnterBattleCallback;
        BattleManager.instance.onExitBattle += OnExitBattleCallback;
    }

    //// Update is called once per frame
    //void Update()
    //{
        
    //}

    public void OnEnterBattleCallback()
    {
        //PlayTrack(Track.Battle);
        StartCoroutine(FadeOutAndInTrackOverSecondsCoroutine(Track.Battle, 1.5f));
    }

    public void OnExitBattleCallback()
    {
        //PlayTrack(Track.Overworld);
        //FadeInTrackOverSeconds(Track.Overworld, 5.0f);
        StartCoroutine(FadeOutAndInTrackOverSecondsCoroutine(Track.Overworld, 1.5f));
    }

    public void PlayTrack(MusicManager.Track trackID)
    {
        musicSource.clip = trackList[(int)trackID];
        musicSource.Play();
    }



    public void FadeInTrackOverSeconds(MusicManager.Track trackID, float duration)
    {
        musicSource.volume = 0.0f;
        PlayTrack(trackID);
        StartCoroutine(FadeInTrackOverSecondsCoroutine(duration));
    }

    IEnumerator FadeInTrackOverSecondsCoroutine(float duration)
    {
        float timer = 0.0f;

        while(timer < duration)
        {
            timer += Time.deltaTime;

            float normalizedTime = timer / duration;

            musicSource.volume = Mathf.SmoothStep(0, 1, normalizedTime);

            yield return new WaitForEndOfFrame();
        }
    }

    IEnumerator FadeOutAndInTrackOverSecondsCoroutine(MusicManager.Track fadeInTrackID, float duration)
    {
        float timer = duration;

        //Fade out
        while (timer >= 0.0f)
        {
            //Debug.Log("Fade out");
            timer -= Time.deltaTime;

            float normalizedTime = timer / duration;

            musicSource.volume = normalizedTime;

            yield return new WaitForEndOfFrame();
        }

        //Fade in new track
        FadeInTrackOverSeconds(fadeInTrackID, duration);
    }



    public void SetMusicVolume(float volumeNormalized)
    {
        musicMixer.SetFloat("MusicVolume", Mathf.Lerp(volumeMin_dB, volumeMax_dB, volumeNormalized));
    }
}

