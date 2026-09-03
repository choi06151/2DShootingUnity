using UnityEngine;

public abstract class CommandParent : ICommand
{
    protected GameObject _executedObject;
    protected float _executedTime;

    public CommandParent(GameObject executedObject)
    {
        _executedTime = Time.time;
        _executedObject = executedObject;
    }


    public float GetExecutionTime()
    {
        return _executedTime;
    }

    public abstract void Execute();
}