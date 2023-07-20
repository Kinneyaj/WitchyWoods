using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IState
{
    void Enter();
    void Execute(StateMachine stateMacine);
    void Exit();
}

public abstract class BaseState : IState {
    protected GameObject owner;
    public BaseState(GameObject owner) { 
        this.owner = owner;
    }

    public virtual void Enter() { }

    public virtual void Execute(StateMachine stateMachine) { }

    public virtual void Exit() { }
}

public class StateMachine {
    private IState currentState;
    private IState previousState;

    public StateMachine(IState initialState) {
        this.currentState = initialState;
        currentState.Enter();
    }

    public void ChangeState(IState newState) {
        currentState.Exit();
        previousState = currentState;
        currentState = newState;
        currentState.Enter();
    }

    public void Update() {
        currentState.Execute(this);
    }
}