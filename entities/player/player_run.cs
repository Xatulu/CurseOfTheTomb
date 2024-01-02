using Godot;
using System;

public partial class player_run : State
{
    private Player player;

    public override void Enter(Object previous_state, Object host)
    {
        player = (Player)host;
        player.animPlayer.Play("run");
    }

    public override void Exit(Object new_state, Object host)
    {

    }

    public override void Execute(double delta, Object host)
    {

    }

    public override Object GetNextState(Object host)
    {
        player = (Player)host;

        if (player.IsFalling())
        {
            return Player.States.FALL;
        }

        if (player.Jump())
        {
            return Player.States.JUMP;
        }

        if (Mathf.IsZeroApprox(player.Velocity.X))
        {
            return Player.States.IDLE;
        }

        return null;
    }

}

