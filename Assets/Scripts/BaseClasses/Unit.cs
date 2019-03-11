using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour
{
    // Used to determine the ratio of horizontal to vertical knockback applied
    // i.e. you can have enemies bounce higher than others, 
    // or enemies that are resistant to knockback (Vector2(0,0))
    public Vector2 knockbackBase;

    public float hitImmunityCoolDown = .5f;

    [HideInInspector] public Health health;

    protected bool canBeHit = true;

    protected BaseMovement movement;

    protected virtual void Awake()
    {
        health = GetComponent<Health>();

        movement = GetComponent<BaseMovement>();
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

        Vector2 horizontalKnockback = (new Vector2(hitDirection.x, hitDirection.z)).normalized * knockbackBase.x;

        Vector3 fullKnockback = new Vector3(horizontalKnockback.x, knockbackBase.y * knockbackModifier, horizontalKnockback.y);

        movement.Push(fullKnockback);

        StartCoroutine(hitCooldown());      

		health.Damage(damage);  
    }
}
