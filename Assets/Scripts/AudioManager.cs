using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [HideInInspector] public static AudioManager Instance;
    [SerializeField] private AudioClip[] sounds;
    [SerializeField] private AudioSource audioPlayer;
    void Awake()
    {
        if (Instance == null)
            Instance = this;
    }

    public void PlayClickSound()
    {
        audioPlayer.PlayOneShot(sounds[0], 0.7f);
    }  
    
    public void PlayWinSound()
    {
        audioPlayer.PlayOneShot(sounds[1], 0.7f);
    } 
    
    public void PlayLoseSound()
    {
        audioPlayer.PlayOneShot(sounds[2], 0.7f);
    }
    public void PlayVictorySound()
    {
        audioPlayer.PlayOneShot(sounds[3], 0.7f);
    } 
    
    public void PlayGameOverSound()
    {
        audioPlayer.PlayOneShot(sounds[4], 0.7f);
    }
    
    public void PlayEqualSound()
    {
        audioPlayer.PlayOneShot(sounds[5], 0.7f);
    }
    
    
}
