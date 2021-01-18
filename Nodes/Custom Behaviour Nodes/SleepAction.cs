using UnityEngine;

class SleepAction : TimeDependentActionNode
{
    Sleeper ai;

    public SleepAction(Sleeper ai)
    {
        this.ai = ai;
    }

    public SleepAction(Sleeper ai, float cycleTime)
    {
        this.ai = ai;
        this.cycleTime = cycleTime;
    }

    protected override NodeStatus DoAction()
    {
        Debug.Log("CAT SLEPT");
        ai.Sleep();

        if (ai.IsRested())
        {
            ai.Awake();
            return NodeStatus.SUCCESS;
        }

        return NodeStatus.RUNNING;
    }
}
