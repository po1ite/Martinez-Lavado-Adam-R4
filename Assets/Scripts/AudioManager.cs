using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;


[RequireComponent(typeof(AudioSource))]
public class AudioManager : MonoBehaviour
{

    public AudioClip[] audioList;
    private int currentAudio;
    private AudioSource source;

    public Text audioTitle;

    private void Start()
    {
        source = GetComponent<AudioSource>();
        //playMusic();
    }

    public void playMusic()
    {
        if (source.isPlaying) 
        {
            return;
        }

        currentAudio--;

        if (currentAudio < 0)
        {
            currentAudio = audioList.Length - 1;
        }

        StartCoroutine("EndOfAudio");
    }

    IEnumerator EndOfAudio()
    {
        while (source.isPlaying)
        {
            yield return null;
        }
        NextAudio();
    }

    public void NextAudio()
    {
        source.Pause();
        currentAudio++;

        if (currentAudio > audioList.Length - 1)
        {
            currentAudio = 0;
        }

        source.clip = audioList[currentAudio];
        source.Play();

        StartCoroutine("EndOfAudio");

        ShowTitle();
    }

    public void PreviousAudio()
    {
        source.Pause();
        currentAudio--;

        if (currentAudio < 0)
        {
            currentAudio = audioList.Length - 1;
        }

        source.clip = audioList[currentAudio];
        source.Play();

        StartCoroutine("EndOfAudio");

        ShowTitle();
    }

    public void PlaySpecificAudio(int index)
    {
        currentAudio = index;

        source.clip = audioList[currentAudio];
        source.Play();

        StartCoroutine("EndOfAudio");

        ShowTitle();
    }

    public void StopAudio()
    {
        StopCoroutine("EndOfAudio");
        source.Pause();
    }

    void ShowTitle()
    {
        audioTitle.text = source.clip.name;

    }

}
