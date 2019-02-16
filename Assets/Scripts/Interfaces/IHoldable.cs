using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IHoldable
{
	//WE ALWAYS ASSUME THAT THIS CONSUMES THE ITEM
	//they should be bools if not
    void Use(GameObject user);
    void Drop(GameObject droppedBy);
}
