using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine.XR;

public class StateMachineBase<T>
{
    readonly StateBase<T> owner;
    readonly List<StateTransition<T>> transitions = new();
    readonly HashSet<StateBase<T>> registered = new();

    StateBase<T> pending;
    bool hasPending, busy, running;
    
    public StateBase<T> currentState { get; private set; }
    public StateBase<T> defaultState { get; private set; }
    public StateBase<T> lastState { get; private set; }
    public bool isRunning => running;
    
    public event Action<StateBase<T>, StateBase<T>> stateChanged;

    public StateMachineBase(StateBase<T> owner = null) { this.owner = owner; }
    
    
    // ========== Setup ==========

    public StateMachineBase<T> SetDefault(StateBase<T> state)
    {
        RegisterState(state);
        defaultState = state;
        return this;
    }
    
    public StateMachineBase<T> Add(StateBase<T> from, StateBase<T> to, Func<bool> condition, int priority = 0)
        => AddTransition(new StateTransition<T> { PreviousState = from, NextState = to, Condition = condition, Priority = priority });
    
    public StateMachineBase<T> Any(StateBase<T> to, Func<bool> condition, int priority = 0)
        => AddTransition(new StateTransition<T> { PreviousState = null, NextState = to, Condition = condition, Priority = priority });
    
    public StateMachineBase<T> On(GameEventsEnum evt, StateBase<T> from, StateBase<T> to, Func<bool> guard = null)
        => AddTransition(new StateTransition<T> { PreviousState = from, NextState = to, TransitionTrigger = evt, Condition = guard });
    
    public StateMachineBase<T> OnAny(GameEventsEnum evt, StateBase<T> to, Func<bool> guard = null)
        => AddTransition(new StateTransition<T> { PreviousState = null, NextState = to, TransitionTrigger = evt, Condition = guard });



    StateMachineBase<T> AddTransition(StateTransition<T> transition)
    {
        if (transition.PreviousState != null) RegisterState(transition.PreviousState);
        RegisterState(transition.NextState);

        if (transition.TransitionTrigger.HasValue)
        {
            transition.Handler = () => OnTrigger(transition);
            if (running) GameEvents.On(transition.TransitionTrigger.Value,  transition.Handler);
        }
        
        int i = transitions.Count;
        while (i > 0 && transitions[i - 1].Priority < transition.Priority) { i--; }

        transitions.Insert(i, transition);
        return this;
    }
    
    void RegisterState(StateBase<T> state)
    {
        if (registered.Add(state)) state.Parent = owner;
    }
    
    #region ========== State Lifecycle ========== 
    // ========== State Lifecycle ==========

    public void Start()
    {
        if (running) return;
        running = true;

        foreach (var transition in transitions)
        {
            if (transition.TransitionTrigger.HasValue)
            {
                GameEvents.On(transition.TransitionTrigger.Value, transition.Handler);
            }
        }
        
        bool resume = owner != null && owner.keepHistory && lastState != null;
        ChangeState(resume ? lastState : defaultState);
    }

    public void Stop()
    {
        if (!running) return;
        ChangeState(null);
        running = false;
        foreach (var transition in transitions)
        {
            if (transition.TransitionTrigger.HasValue)
            {
                GameEvents.Off(transition.TransitionTrigger.Value, transition.Handler);
            }
        }
    }

    public void Update()
    {
        if (!running) Start();
        EvaluatePolled();
        if (currentState == null) return;

        currentState.Update();
        if (currentState.hasSubStateMachine) currentState.SubStateMachine.Update();
    }
    
    public void FixedUpdate()
    {
        if (currentState == null) return;
        currentState.FixedUpdate();
        if (currentState.hasSubStateMachine) currentState.SubStateMachine.FixedUpdate();
    }
    
    // =====================================
    #endregion

    #region ========= State Transitions =========
    // ========== State Transitions ==========
    public void ChangeState(StateBase<T> nextState)
    {
        if (busy) { pending = nextState; hasPending = true; return; }
        if (nextState == currentState) return;

        busy = true;
        var prev = currentState;

        if (prev != null)
        {
            lastState = prev;
            if (prev.hasSubStateMachine) prev.SubStateMachine.Stop();
            prev.Exit();
        }

        currentState = nextState;

        if (nextState != null)
        {
            nextState.Enter();
            if (nextState.hasSubStateMachine) nextState.SubStateMachine.Start();
        }

        busy = false;
        stateChanged?.Invoke(prev, nextState);

        if (hasPending)
        {
            hasPending = false;
            ChangeState(pending);
        }
    }

    void EvaluatePolled()
    {
        foreach (var transition in transitions)
        {
            if (transition.TransitionTrigger.HasValue) continue;
            if (transition.PreviousState != null && transition.PreviousState != currentState) continue;
            if (transition.NextState == currentState) continue;
            if (transition.Condition != null && !transition.Condition()) continue;
            
            ChangeState(transition.NextState);
            return;
        }
    }

    void OnTrigger(StateTransition<T> transition)
    {
        if (!running) { return; }
        if (transition.PreviousState != null && transition.PreviousState != currentState) { return; }
        if (transition.NextState == currentState) { return; }
        if (transition.Condition != null && !transition.Condition()) { return; }

        ChangeState(transition.NextState);
    }
    
    // =====================================
    #endregion
}