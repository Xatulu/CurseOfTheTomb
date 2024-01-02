using Godot;
using System;

public partial class player_idle : State
{
    private Player player;

    public override void Enter(Object previous_state, Object host)
    {
        player = (Player)host;

        if ((Player.States)previous_state == Player.States.FALL)
        {
            player.footstep_sfx.PlayAtRandomPitch();
        }

        player.animPlayer.Play("idle");
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

        if (!Mathf.IsZeroApprox(player.Velocity.X))
        {
            return Player.States.RUN;
        }

        return null;
    }

}

