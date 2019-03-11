using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HurtBox : MonoBehaviour
{
    public int damageModifier = 1;
    public int knockbackModifier = 1;

    private AudioSource audioSource;

    void Start()
    {
        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.spatialBlend = 1;
    }

    protected void OnTriggerStay(Collider col)
    {
        if (col.gameObject.CompareTag("Player"))
        {
            //Debug.Log("Hey!");
            Unit unit = col.transform.parent.gameObject.GetComponent<Unit>();
            if (unit != null)
            {
                int damage = damageModifier;
                float knockback = knockbackModifier;
                Vector3 direction = transform.forward.normalized;

                if (unit.canBeHit)
                {
                    audioSource.clip = AudioManager.instance.insectAttackAudio;
                    audioSource.Play();
                }

                unit.GetHit(damage, direction, knockback);
            }
        }
    }
}
