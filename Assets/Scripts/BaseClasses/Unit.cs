using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour
{
    // Used to determine the ratio of horizontal to vertical knockback applied
    // i.e. you can have enemies bounce higher than others, 
    // or enemies that are resistant to knockback (Vector2(0,0))
    public Vector2 knockbackBase;

    public float hitImmunityCoolDown = 1.5f;

    public GameObject moveTargetPrefab;

    [HideInInspector] public Health health;

    protected GameObject moveTarget;

    protected bool canBeHit = true;

    protected virtual void Awake()
    {
        health = GetComponent<Health>();

        moveTarget = GameObject.Instantiate(moveTargetPrefab);
        moveTarget.GetComponent<BaseMovement>().animatingCharacter = gameObject;

        GetComponent<AnimatedMover>().trackedObj = moveTarget;
    }

    protected virtual IEnumerator hitCooldown()
    {
        canBeHit = false;
        yield return new WaitForSeconds(hitImmunityCoolDown);
        canBeHit = true;
    }

    public virtual void GetHit(int damage, Vector3 hitDirection, float knockbackModifier)
    {
        if (!canBeHit)
        {
            return;
        }

        health.Damage(damage);

        Vector2 horizontalKnockback = (new Vector2(hitDirection.x, hitDirection.z)).normalized * knockbackBase.x;

        Vector3 fullKnockback = new Vector3(horizontalKnockback.x, knockbackBase.y * knockbackModifier, horizontalKnockback.y);

        moveTarget.GetComponent<BaseMovement>().Push(fullKnockback);

        StartCoroutine(hitCooldown());        
    }
}
