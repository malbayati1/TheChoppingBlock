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
    private AudioType _munchAudio;

    public AudioClip munchAudio {
        get
        {
            return _munchAudio.GetClip();
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

    [SerializeField]
    private AudioType _pickUpIngredientAudio;

    public AudioClip pickUpIngredientAudio {
        get
        {
            return _pickUpIngredientAudio.GetClip();
        }
    }

    [SerializeField]
    private AudioType _dropIngredientAudio;

    public AudioClip dropIngredientAudio {
        get
        {
            return _dropIngredientAudio.GetClip();
        }
    }

        [SerializeField]
    private AudioType _pickUpKnifeAudio;

    public AudioClip pickUpKnifeAudio {
        get
        {
            return _pickUpKnifeAudio.GetClip();
        }
    }

    [SerializeField]
    private AudioType _dropKnifeAudio;

    public AudioClip dropKnifeAudio {
        get
        {
            return _dropKnifeAudio.GetClip();
        }
    }

    [SerializeField]
    private AudioType _swingAudio;

    public AudioClip swingAudio {
        get
        {
            return _swingAudio.GetClip();
        }
    }

    [SerializeField]
    private AudioType _dullImpactAudio;

    public AudioClip dullImpactAudio {
        get
        {
            return _dullImpactAudio.GetClip();
        }
    }

    [SerializeField]
    private AudioType _juicyImpactAudio;

    public AudioClip juicyImpactAudio {
        get
        {
            return _juicyImpactAudio.GetClip();
        }
    }

    [SerializeField]
    private AudioType _insectAttackAudio;

    public AudioClip insectAttackAudio {
        get
        {
            return _insectAttackAudio.GetClip();
        }
    }

    [SerializeField]
    private AudioType _insectHurtAudio;

    public AudioClip insectHurtAudio {
        get
        {
            return _insectHurtAudio.GetClip();
        }
    }

    [SerializeField]
    private AudioType _insectDeathAudio;

    public AudioClip insectDeathAudio {
        get
        {
            return _insectDeathAudio.GetClip();
        }
    }

    [SerializeField]
    private AudioType _springStartAudio;

    public AudioClip springStartAudio {
        get
        {
            return _springStartAudio.GetClip();
        }
    }

    [SerializeField]
    private AudioType _summerStartAudio;

    public AudioClip summerStartAudio {
        get
        {
            return _summerStartAudio.GetClip();
        }
    }

    [SerializeField]
    private AudioType _fallStartAudio;

    public AudioClip fallStartAudio {
        get
        {
            return _fallStartAudio.GetClip();
        }
    }

    [SerializeField]
    private AudioType _winterStartAudio;

    public AudioClip winterStartAudio {
        get
        {
            return _winterStartAudio.GetClip();
        }
    }

    [SerializeField]
    private AudioType _teleportStartAudio;

    public AudioClip teleportStartAudio {
        get
        {
            return _teleportStartAudio.GetClip();
        }
    }

    [SerializeField]
    private AudioType _teleportMidAudio;

    public AudioClip teleportMidAudio {
        get
        {
            return _teleportMidAudio.GetClip();
        }
    }

    [SerializeField]
    private AudioType _teleportEndAudio;

    public AudioClip teleportEndAudio {
        get
        {
            return _teleportEndAudio.GetClip();
        }
    }

    private AudioSource seasonAudioSource;

    private AudioSource bgMusicSource;

    private float bgMusicVolume;

    void Start()
    {
        bgMusicSource = GetComponent<AudioSource>();

        seasonAudioSource = gameObject.AddComponent<AudioSource>();

        bgMusicVolume = bgMusicSource.volume;
    }

    void OnEnable()
    {
		if(SeasonManager.instance)
		{
			SeasonManager.instance.seasonChangeEvent += PlaySeasonStinger;
		}
    }

    void OnDisable()
    {
		if(SeasonManager.instance)
		{
			SeasonManager.instance.seasonChangeEvent -= PlaySeasonStinger;
		}
    }


    private void PlaySeasonStinger(Season s)
    {
        if (s == Season.Spring)
        {
            seasonAudioSource.clip = AudioManager.instance.springStartAudio;
        }
        else if (s == Season.Summer)
        {
            seasonAudioSource.clip = AudioManager.instance.summerStartAudio;
        }
        else if (s == Season.Winter)
        {
            seasonAudioSource.clip = AudioManager.instance.winterStartAudio;
        }
        else
        {
            seasonAudioSource.clip = AudioManager.instance.fallStartAudio;
        }

        seasonAudioSource.Play();

        StartCoroutine(DisableMusicWhileSeasonStingerPlays());
    }

    private IEnumerator DisableMusicWhileSeasonStingerPlays()
    {
        bgMusicSource.volume = 0;

        while (seasonAudioSource.isPlaying)
        {
            yield return new WaitForEndOfFrame();
        }

        bgMusicSource.volume = bgMusicVolume;
    }
}
