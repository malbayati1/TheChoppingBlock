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

    [SerializeField]
    private AudioType _buzzAudio;

    public AudioClip buzzAudio {
        get
        {
            return _buzzAudio.GetClip();
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
