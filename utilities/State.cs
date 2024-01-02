using Godot;
using System;

public partial class State : Node
{
    public StateMachine fsm;

    public virtual void Enter(Object previous_state, Object host) { }
    public virtual void Exit(Object new_state, Object host) { }

    public virtual void Execute(double delta, Object host) { }

    public virtual Object GetNextState(Object host)
    {
        return null;
    }

}
