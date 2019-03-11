using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using UnityEngine.Random;

public class FlyingAnimatedMover : AnimatedMover
{
    protected override void Animate(float height, float time)
    { 
        iTween.MoveTo(gameObject, iTween.Hash("position",  Vector3.up * height/2, "isLocal", true, "easeType", "easeInCirc", "time", time * 1/4));
        iTween.MoveTo(gameObject, iTween.Hash("position",  Vector3.up * height, "isLocal", true, "easeType", "easeOutCirc", "time", time * 1/4, "delay", time * 1/4));
        iTween.MoveTo(gameObject, iTween.Hash("position",  Vector3.up * height/2, "isLocal", true, "easeType", "easeInCirc", "time", time * 1/4, "delay", time * 2/4));
        iTween.MoveTo(gameObject, iTween.Hash("position",  Vector3.zero, "isLocal", true, "easeType", "easeOutCirc", "time", time * 1/4, "delay", time * 3/4));
    }

    protected override void PlayAudio()
    {
        if (Random.value > .75)
        {
            audioSource.clip = AudioManager.instance.buzzAudio;

            audioSource.Play();
        }
        
    }
}
