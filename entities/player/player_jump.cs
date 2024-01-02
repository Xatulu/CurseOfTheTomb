using Godot;
using System;

public partial class player_jump : State
{
    private Player player;

    public override void Enter(Object previous_state, Object host)
    {
        player = (Player)host;
        player.animPlayer.Play("jump");
    }

    public override void Exit(Object new_state, Object host)
    {

    }

    public override void Execute(double delta, Object host)
    {

    }

    public override Object GetNextState(Object host)
    {
        if (player.IsFalling())
        {
            return Player.States.FALL;
        }
        return null;
    }

}

