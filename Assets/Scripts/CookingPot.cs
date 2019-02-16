using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CookingPot : MonoBehaviour
{
    public Mixture currentMixture;

    public void Cook()
    {

    }

    public void Add(GameObject i)
    {
        i.transform.position = Vector2.one * 9999;
        InGameIngredient ingredient = i.GetComponent<InGameIngredient>();
        if(i != null && currentMixture.AddIngredient(ingredient))
        {
            //nothing?
        }
        else
        {
            DropItem(i);
        }
    }

    public void DropItem(GameObject i)
    {
        //should drop the item in such a way as it flies away from the pot and doesn't get readded
    }

    public void Empty()
    {
        foreach(InGameIngredient i in currentMixture.ingredients)
        {
            DropItem(i.gameObject);
        }
        currentMixture.ingredients = new List<InGameIngredient>();
    }
}
