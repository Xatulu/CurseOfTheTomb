using Godot;
using System;

public partial class ExtendedAudioStreamPlayer : AudioStreamPlayer
{

    [Export]
    private float pitch_jitter = 0.1f;

    private float original_pitch;

    public override void _Ready()
    {
        original_pitch = PitchScale;
    }

    public void PlayAtRandomPitch()
    {
        float new_pitch = original_pitch + new Godot.Collections.Array<float> { -pitch_jitter, 0, pitch_jitter }.PickRandom();
        PitchScale = new_pitch;
        Play();
    }
}