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

    [SerializeField]
    private AudioType _cookAudio;

    public AudioClip cookAudio {
        get
        {
            return _cookAudio.GetClip();
        }
    }

    [SerializeField]
    private AudioType _addIngredientAudio;

    public AudioClip addIngredientAudio {
        get
        {
            return _addIngredientAudio.GetClip();
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
