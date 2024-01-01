using Godot;
using System;

public partial class Player : CharacterBody2D
{
    [Export]
    private float speed = 600.0f;
    [Export]
    private float gravity = (float)ProjectSettings.GetSetting("physics/2d/default_gravity");
    [Export]
    private float jump_velocity = -1500.0f;
    [Export]
    private float jump_hang_time = 0.15f;

    private Node2D pivot;
    private float hang_time_remaining = 0.0f;

    public override void _Ready()
    {
        pivot = GetNode<Node2D>("Pivot");
    }
    public override void _PhysicsProcess(double delta)
    {
        float direction = Input.GetAxis("move_left", "move_right");

        if (direction > 0)
        {
            pivot.Scale = pivot.Scale with { X = 1 };
        }
        else if (direction < 0)
        {
            pivot.Scale = pivot.Scale with { X = -1 };
        }

        Vector2 newVelocity = new Vector2(direction * speed, Velocity.Y);
        if (!IsOnFloor())
        {
            if (hang_time_remaining > 0)
            {
                newVelocity.Y = jump_velocity;
                hang_time_remaining -= (float)delta;
            }
            else
            {
                newVelocity.Y += gravity;
            }

        }

        if (IsOnFloor() && Input.IsActionJustPressed("jump"))
        {
            newVelocity.Y = jump_velocity;
            hang_time_remaining = jump_hang_time;
        }

        Velocity = newVelocity;
        MoveAndSlide();
    }
}
