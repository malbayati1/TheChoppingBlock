using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IHoldable
{
    bool Use(GameObject user);
    void Drop(GameObject droppedBy);
}
