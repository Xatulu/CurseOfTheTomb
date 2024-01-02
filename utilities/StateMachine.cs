using Godot;
using System;
using System.Collections.Generic;

public partial class StateMachine : Node
{

    private Dictionary<Object, State> _states = new Dictionary<Object, State> { };
    private Object current_state;
    private Object previous_state;
    private Node host;


    // Initialize State Machine
    public void Initialize(Node host, Object InititalState)
    {
        this.host = host;

        if (InititalState != null)
        {
            SetState(InititalState);
        }
    }

    // Add State received to Instantiated Dictionary
    public void AddState(Object key, State node)
    {
        _states[key] = node;
    }

    // Set current state, store previous state and trigger Exit/Enter Functions of corresponding States
    public void SetState(Object new_state)
    {
        if (!_states.ContainsKey(new_state))
        {
            return;
        }

        previous_state = current_state;
        current_state = new_state;

        if (previous_state != null)
        {
            _states[previous_state].Exit(new_state, host);
        }

        if (current_state != null)
        {
            _states[current_state].Enter(previous_state, host);
        }

    }

    // Check if we should switch state and do Calculcations in Steps (not implemented yet)
    public override void _PhysicsProcess(double delta)
    {
        if (current_state == null)
        {
            return;
        }

        _states[current_state].Execute(delta, host);

        var next_state = _states[current_state].GetNextState(host);
        if (next_state != null)
        {
            SetState(next_state);
        }
    }

}
