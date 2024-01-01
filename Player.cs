using Godot;
using System;

public partial class Player : CharacterBody2D
{
    [Export]
    private float speed = 600.0f;

    public override void _PhysicsProcess(double delta)
    {
        float direction = Input.GetAxis("move_left", "move_right");

        this.Velocity = new Vector2(direction * speed, 0);
        MoveAndSlide();
    }
}
