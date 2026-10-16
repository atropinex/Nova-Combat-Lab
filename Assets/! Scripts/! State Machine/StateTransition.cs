using System;
using UnityEngine;

public sealed class StateTransition<T>
{
    public StateBase<T> PreviousState;
    public StateBase<T> NextState;
    public Func<bool> Condition;
    public GameEventsEnum? TransitionTrigger;
    public int Priority;

    internal Action Handler;
}