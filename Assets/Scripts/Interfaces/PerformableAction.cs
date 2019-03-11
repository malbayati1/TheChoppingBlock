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
