using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public delegate bool BoolDelegate();
public delegate void ActionDelegate();

public class PerformableAction
{
	public BoolDelegate IsPerformable;	
	public int priority;
	public ActionDelegate Action;
	public PerformableAction(BoolDelegate bd, int p, ActionDelegate ad)
	{
		IsPerformable = bd;
		priority = p;
		Action = ad;
	}
}

public class PerformableActionComparator : IComparer<PerformableAction>
{
	public int Compare(PerformableAction pa1, PerformableAction pa2)
	{
		return pa1.priority - pa2.priority;
	}
}
