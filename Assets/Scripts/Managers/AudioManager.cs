using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : Singleton<AudioManager>
{
    [SerializeField]
    private AudioType _stepAudio;

    public AudioClip stepAudio {
        get
        {
            return _stepAudio.GetClip();
        }
    }


    void Start()
    {
    }

    void OnEnable()
    {
    }

    void OnDisable()
    {
    }
}
