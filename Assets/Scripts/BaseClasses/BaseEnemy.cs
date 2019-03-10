using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseEnemy : Unit
{
    public int damageModifier = 1;
    public int knockbackModifier = 1;
    public int framesBetweenUpdates = 1;
    protected GameObject player;

    protected AudioSource damageAudioSource;

    protected virtual void Start()
    {
        player = GameObject.FindWithTag("Player");

        damageAudioSource = gameObject.AddComponent<AudioSource>();
        damageAudioSource.spatialBlend = 1;
    }
    
    // Update is called once per frame
    void Update()
    {
        if (Time.frameCount % framesBetweenUpdates != 0)
            return;
        //transform.LookAt(player.transform, Vector3.up);
        movement.Move(player.transform.position);
    }

    public override void GetHit(int damage, Vector3 hitDirection, float knockbackModifier)
    {
        if (health.health - damage <= 0)
        {
            //Currently the gameobject doesn't last long enough for this to play
            damageAudioSource.clip = AudioManager.instance.insectDeathAudio;
        }
        else
        {
            damageAudioSource.clip = AudioManager.instance.insectHurtAudio;
        }
        damageAudioSource.Play();

        base.GetHit(damage, hitDirection, knockbackModifier);
    }
}
