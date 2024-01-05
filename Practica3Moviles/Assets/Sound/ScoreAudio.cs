using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreAudio : MonoBehaviour
{
    [SerializeField] EventManager eventManager;
    AudioSource m_AudioSource;

    // Start is called before the first frame update
    void Start()
    {
        AudioSource audioSource = GetComponent<AudioSource>();
        eventManager.OnScore += PlayAudio;
    }

    public void PlayAudio() 
    {
        m_AudioSource.Play();
    }
}
