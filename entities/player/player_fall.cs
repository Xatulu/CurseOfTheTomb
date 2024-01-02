using Godot;
using System;

public partial class player_fall : State
{
    private Player player;

    public override void Enter(Object previous_state, Object host)
    {
        player = (Player)host;
        player.animPlayer.Play("fall");
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

        if (player.IsOnFloor())
        {
            return Player.States.IDLE;
        }

        return null;
    }
}
