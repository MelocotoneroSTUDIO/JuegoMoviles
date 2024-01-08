using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreAudio : MonoBehaviour
{
    [SerializeField] EventManager eventManager;
    [SerializeField] AudioClip scoreSound;
    [SerializeField] AudioClip failSound;
    AudioSource m_AudioSource;

    // Start is called before the first frame update
    void Start()
    {
        AudioSource audioSource = GetComponent<AudioSource>();
        eventManager.OnScore += PlayScoreAudio;
        eventManager.OnDeath += PlayFailAudio;
    }

    public void PlayScoreAudio() 
    {
        m_AudioSource.clip = scoreSound;
        m_AudioSource.Play();
    }

    public void PlayFailAudio()
    {
        m_AudioSource.clip = failSound;
        m_AudioSource.Play();
    }
}
