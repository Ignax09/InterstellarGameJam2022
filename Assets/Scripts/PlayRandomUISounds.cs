using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayRandomUISounds : MonoBehaviour
{
    public AudioSource audioFiles;
    public AudioClip[] audioClipArray;
    public AudioClip[] timeLineScrubSndArray;

    void Awake()
    {
        audioFiles = GetComponent<AudioSource>();
    }

    public void playSound()
    {
        audioFiles.clip = audioClipArray[Random.Range(0, audioClipArray.Length)];
        audioFiles.PlayOneShot(audioFiles.clip);
    }

    public void playTimeLineScrubSnd()
    {
        audioFiles.clip = timeLineScrubSndArray[Random.Range(0, timeLineScrubSndArray.Length)];
        audioFiles.PlayOneShot(audioFiles.clip);
        //
    }

    void Start()
    {


    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
