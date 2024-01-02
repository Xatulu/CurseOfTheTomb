using Godot;
using System;

public partial class Player : CharacterBody2D
{

    public enum States
    {
        IDLE,
        RUN,
        JUMP,
        FALL
    }

    //Define Standard Variables
    [Export] private float speed = 1000.0f;
    [Export] private float gravity = (float)ProjectSettings.GetSetting("physics/2d/default_gravity");
    [Export] private float jump_velocity = -1500.0f;

    [Export] private float jump_hang_time = 0.15f;
    private float hang_time_remaining = 0.0f;

    [Export] private float jump_input_buffer = 0.15f;
    private float input_buffer_remaining = 0.0f;

    //Variables for OnReady Nodes (Set in _Ready)
    public Node2D pivot;
    public AnimationPlayer animPlayer;
    public StateMachine stateMachine;
    public ExtendedAudioStreamPlayer footstep_sfx;

    public override void _Ready()
    {
        pivot = GetNode<Node2D>("Pivot");
        animPlayer = GetNode<AnimationPlayer>("AnimationPlayer");
        stateMachine = GetNode<StateMachine>("StateMachine");
        footstep_sfx = GetNode<ExtendedAudioStreamPlayer>("Sounds/Footstep");

        // Add States to Player Instantiated Version of StateMachine inlcuding Node Path
        stateMachine.AddState(States.IDLE, GetNode<State>("StateMachine/Idle"));
        stateMachine.AddState(States.RUN, GetNode<State>("StateMachine/Run"));
        stateMachine.AddState(States.JUMP, GetNode<State>("StateMachine/Jump"));
        stateMachine.AddState(States.FALL, GetNode<State>("StateMachine/Fall"));
        //Set Initiale State to IDLE
        stateMachine.Initialize(this, States.IDLE);
    }

    public override void _PhysicsProcess(double delta)
    {
        float direction = Input.GetAxis("move_left", "move_right");

        // Flip Player via Pivot Node when moving Left
        if (direction > 0)
        {
            pivot.Scale = pivot.Scale with { X = 1 };
        }
        else if (direction < 0)
        {
            pivot.Scale = pivot.Scale with { X = -1 };
        }

        // Update Velocity with With Constructor
        Velocity = Velocity with { X = direction * speed };

        // Handle Hang Time / Jumping
        if (!IsOnFloor())
        {
            if (hang_time_remaining > 0)
            {
                Velocity = Velocity with { Y = jump_velocity };
                hang_time_remaining -= (float)delta;
            }
            else
            {
                Velocity = Velocity + Velocity with { X = 0, Y = gravity };
            }

        }

        if (input_buffer_remaining > 0.0)
        {
            input_buffer_remaining -= (float)delta;
        }

        MoveAndSlide();
    }

    // Check if Player can jump and jump 
    public bool Jump()
    {
        if (input_buffer_remaining > 0.0 && IsOnFloor())
        {
            Velocity = Velocity with { Y = jump_velocity };
            hang_time_remaining = jump_hang_time;
            return true;
        }
        return false;
    }

    // Is Player Falling (Speed downwards > Gravity)
    public bool IsFalling()
    {
        return Velocity.Y > 250;
    }

    public override void _UnhandledKeyInput(InputEvent @event)
    {
        if (@event.IsActionPressed("jump"))
        {
            input_buffer_remaining = jump_input_buffer;
        }
    }
}

