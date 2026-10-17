using UnityEngine;
using System;

public abstract class StateBase<T>
{
    public T Owner { get; }
    public StateBase<T> Parent { get; internal set; }
    
    public bool keepHistory { get; protected set; }
    
    StateMachineBase<T> subStateMachine;
    public StateMachineBase<T> SubStateMachine => subStateMachine ??= new StateMachineBase<T>();
    public bool hasSubStateMachine => subStateMachine != null;
    
    protected StateBase(T owner) { Owner = owner; }

    public virtual void Enter()
    {
        OnEnter();
        subStateMachine?.Start();
    }

    public virtual void Exit()
    {
        subStateMachine?.Stop(); 
        OnExit();
    }

    public virtual void Update()
    {
        OnUpdate();
        subStateMachine?.Update();
    }

    public virtual void FixedUpdate()
    {
        OnFixedUpdate();
        subStateMachine?.FixedUpdate();
    }
    
    protected virtual void OnEnter() { }
    protected virtual void OnUpdate() { }
    protected virtual void OnFixedUpdate() { }
    protected virtual void OnExit() { }
}