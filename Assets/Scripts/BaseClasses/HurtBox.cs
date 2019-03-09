using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HurtBox : MonoBehaviour
{
    public int damageModifier = 1;
    public int knockbackModifier = 1;

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

				unit.GetHit(damage, direction, knockback);
			}
		}
	}
}
