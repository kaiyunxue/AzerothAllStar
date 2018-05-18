using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioController : MonoBehaviour {
    static float volumeMultiplier = 1;
    [SerializeField]
    protected AudioSource audioPlayer;
    private void OnEnable()
    {
        audioPlayer = GetComponent<AudioSource>();
    }

    public void PlaySound(AudioClip sound, float prob = 1)
    {
        float rdmNum = Random.Range(0f, 1f);

        if(rdmNum <= prob)
        {
            if (!audioPlayer.isPlaying)
            {
                audioPlayer.clip = sound;
                audioPlayer.volume *= volumeMultiplier;
                audioPlayer.Play();
            }
        }
    }
    public void ForcePlaySound(AudioClip sound)
    {
        audioPlayer.clip = sound;
        audioPlayer.volume *= volumeMultiplier;
        audioPlayer.Play();
    }
}
